from fastapi import FastAPI, HTTPException, Depends, status
from fastapi.security import HTTPBasic, HTTPBasicCredentials
from typing import List, Dict, Optional
import sqlite3
import hashlib
from datetime import datetime
from pydantic import BaseModel, constr
from fastapi.middleware.cors import CORSMiddleware
import requests
from fastapi.openapi.docs import get_swagger_ui_html

OPENROUTER_API_KEY = "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"
OPENROUTER_URL = "https://openrouter.ai/api/v1/chat/completions"

def get_llm_response(chat_history: List[Dict]) -> str:
    headers = {
        "Content-Type": "application/json",
        "Authorization": f"Bearer {OPENROUTER_API_KEY}"
    }

    # Format chat history into messages format
    messages = [
        {
            "role": "user",
            "content": msg["message"]
        }
        for msg in chat_history
    ]

    data = {
        "model": "meta-llama/llama-3.3-70b-instruct",
        "messages": messages
    }

    try:
        response = requests.post(OPENROUTER_URL, headers=headers, json=data)
        response.raise_for_status()  # Raise exception for bad status codes
        return response.json()['choices'][0]['message']['content']
    except Exception as e:
        print(f"LLM API Error: {e}")
        return "Sorry, I couldn't process that request."

app = FastAPI()
security = HTTPBasic()

app.add_middleware(
    CORSMiddleware,
    allow_origins=["*"],
    allow_credentials=True,
    allow_methods=["*"],
    allow_headers=["*"],
)


def check_account_exists(credentials: HTTPBasicCredentials, db) -> bool:
    cursor = db.cursor()
    password_hash = hashlib.sha256(credentials.password.encode()).hexdigest()

    cursor.execute(
        "SELECT 1 FROM students WHERE username = ? AND password_hash = ?",
        (credentials.username, password_hash)
    )
    exists = cursor.fetchone() is not None
    return exists

def get_db():
    conn = sqlite3.connect('study_buddy.db')
    conn.row_factory = sqlite3.Row
    return conn

class StudentCreate(BaseModel):
    username: constr(max_length=50)
    password: constr(max_length=100)
    major: constr(max_length=100)
    description: Optional[constr(max_length=200)] = None

class Message(BaseModel):
    message: str
    is_llm: bool = False

import json

@app.post("/login/")
def login(credentials: HTTPBasicCredentials):
    tmp = authenticate_user(credentials)
    print(tmp)
    return {'user_id': tmp}

def authenticate_user(credentials: HTTPBasicCredentials):
    db = get_db()
    cursor = db.cursor()

    # First check if account exists
    if not check_account_exists(credentials, db):
        db.close()
        raise HTTPException(
            status_code=status.HTTP_404_NOT_FOUND,
            detail="Account not found"
        )

    password_hash = hashlib.sha256(credentials.password.encode()).hexdigest()

    cursor.execute(
        "SELECT student_id, username FROM students WHERE username = ? AND password_hash = ?",
        (credentials.username, password_hash)
    )
    user = cursor.fetchone()

    if not user:
        raise HTTPException(
            status_code=status.HTTP_401_UNAUTHORIZED,
            detail="Invalid credentials"
        )

    db.close()
    return user['student_id']

@app.post("/register/")
async def register(student: StudentCreate):
    db = get_db()
    cursor = db.cursor()

    password_hash = hashlib.sha256(student.password.encode()).hexdigest()

    try:
        cursor.execute("""
            INSERT INTO students (username, password_hash, major, description, created_at)
            VALUES (?, ?, ?, ?, ?)
        """, (student.username, password_hash, student.major, student.description, datetime.now()))
        db.commit()
    except sqlite3.IntegrityError:
        db.close()
        raise HTTPException(status_code=400, detail="Username already exists")

    new_id = cursor.lastrowid
    db.close()
    return {"student_id": new_id}

@app.get("/compatible-students/")
async def get_compatible_students(
    credentials: HTTPBasicCredentials = Depends(security),
    limit: int = 10,
    offset: int = 0
):
    current_user_id = authenticate_user(credentials)
    db = get_db()
    cursor = db.cursor()

    # Get current user's major
    cursor.execute("""
        SELECT major FROM students WHERE student_id = ?
    """, (current_user_id,))
    user_major = dict(cursor.fetchone())['major']

    # Find compatible students (same major, free, not current user)
    cursor.execute("""
        SELECT student_id, username, major, description
        FROM students
        WHERE major = ?
        AND is_free = TRUE
        AND student_id != ?
        LIMIT ? OFFSET ?
    """, (user_major, current_user_id, limit, offset))

    compatible_students = [dict(row) for row in cursor.fetchall()]

    # Get total count
    cursor.execute("""
        SELECT COUNT(*) as total
        FROM students
        WHERE major = ?
        AND is_free = TRUE
        AND student_id != ?
    """, (user_major, current_user_id))

    total = dict(cursor.fetchone())['total']
    db.close()

    return {
        "students": compatible_students,
        "total": total,
        "limit": limit,
        "offset": offset
    }


@app.post("/student/chats")
async def get_student_chat_history(
        credentials: HTTPBasicCredentials = Depends(security),
        limit: int = 10,
        offset: int = 0
):
    current_user_id = authenticate_user(credentials)
    db = get_db()
    cursor = db.cursor()

    # Get all chats where the student is involved
    cursor.execute("""
        SELECT 
            m.match_id,
            m.created_at as match_created_at,
            m.status,
            CASE 
                WHEN m.student1_id = ? THEN m.student2_id
                ELSE m.student1_id
            END as other_student_id,
            s.username as other_student_name,
            s.major as other_student_major,
            (
                SELECT message 
                FROM chat_messages 
                WHERE match_id = m.match_id 
                ORDER BY created_at DESC 
                LIMIT 1
            ) as last_message,
            (
                SELECT created_at 
                FROM chat_messages 
                WHERE match_id = m.match_id 
                ORDER BY created_at DESC 
                LIMIT 1
            ) as last_message_time
        FROM matches m
        JOIN students s ON (
            CASE 
                WHEN m.student1_id = ? THEN m.student2_id
                ELSE m.student1_id
            END = s.student_id
        )
        WHERE (m.student1_id = ? OR m.student2_id = ?)
        AND m.status = 'active'
        ORDER BY last_message_time DESC NULLS LAST
        LIMIT ? OFFSET ?
    """, (current_user_id, current_user_id, current_user_id, current_user_id, limit, offset))

    chats = [dict(row) for row in cursor.fetchall()]

    # Get total count for pagination
    cursor.execute("""
        SELECT COUNT(*) as total 
        FROM matches 
        WHERE (student1_id = ? OR student2_id = ?)
        AND status = 'active'
    """, (current_user_id, current_user_id))

    total = dict(cursor.fetchone())['total']
    print(chats)
    db.close()
    return {
        "chats": chats,
        "total": total,
        "limit": limit,
        "offset": offset
    }


@app.post("/match/{matched_student_id}")
async def create_match(matched_student_id: int, credentials: HTTPBasicCredentials = Depends(security)):
    current_user_id = authenticate_user(credentials)
    db = get_db()
    cursor = db.cursor()

    try:
        # Start transaction
        cursor.execute("BEGIN TRANSACTION")

        # Check if users are free
        cursor.execute("""
            SELECT is_free FROM students WHERE student_id IN (?, ?)
        """, (current_user_id, matched_student_id))
        users_status = cursor.fetchall()
        for user_status in users_status:
            if not dict(user_status)['is_free']:
                cursor.execute("ROLLBACK")
                db.close()
                raise HTTPException(status_code=400, detail="One or both users are already in a match")

        # Create match
        cursor.execute("""
            INSERT INTO matches (student1_id, student2_id, created_at, status)
            VALUES (?, ?, ?, ?)
        """, (current_user_id, matched_student_id, datetime.now(), "active"))

        # Update both students' status
        cursor.execute("""
            UPDATE students SET is_free = FALSE
            WHERE student_id IN (?, ?)
        """, (current_user_id, matched_student_id))

        # Get match ID
        match_id = cursor.lastrowid

        # Get matched username
        cursor.execute("""
            SELECT username FROM students WHERE student_id = ?
        """, (matched_student_id,))
        matched_username_row = cursor.fetchone()
        matched_username = dict(matched_username_row)['username'] if matched_username_row else "Unknown"


        # Commit transaction
        cursor.execute("COMMIT")

        db.close()
        return {"match_id": match_id, "matched_with": matched_student_id, "matched_username": matched_username}

    except Exception as e:
        cursor.execute("ROLLBACK")
        db.close()
        raise HTTPException(status_code=500, detail=str(e))

@app.get("/chat/{match_id}")
async def get_chat_history(
    match_id: int,
    limit: int = 10,
    offset: int = 0,
    credentials: HTTPBasicCredentials = Depends(security)
):
    current_user_id = authenticate_user(credentials)
    db = get_db()
    cursor = db.cursor()

    # Verify user is part of the match
    cursor.execute("""
        SELECT 1 FROM matches
        WHERE match_id = ? AND (student1_id = ? OR student2_id = ?)
    """, (match_id, current_user_id, current_user_id))

    if not cursor.fetchone():
        db.close()
        raise HTTPException(status_code=403, detail="Not authorized to view this chat")

    cursor.execute("""
        SELECT m.message_id, m.sender_id, s.username as sender_username,
               m.message, m.is_llm, m.created_at
        FROM chat_messages m
        JOIN students s ON m.sender_id = s.student_id
        WHERE m.match_id = ?
        ORDER BY m.created_at ASC
        LIMIT ? OFFSET ?
    """, (match_id, limit, offset))

    messages = [dict(row) for row in cursor.fetchall()]

    # Get total count for pagination info
    cursor.execute("SELECT COUNT(*) as total FROM chat_messages WHERE match_id = ?", (match_id,))
    total = dict(cursor.fetchone())['total']

    db.close()
    return {
        "messages": messages,
        "total": total,
        "limit": limit,
        "offset": offset
    }

@app.post("/chat/{match_id}")
async def send_message(
    match_id: int,
    message: Message,
    credentials: HTTPBasicCredentials = Depends(security)
):
    current_user_id = authenticate_user(credentials)
    db = get_db()
    cursor = db.cursor()

    # Verify user is part of the match
    cursor.execute("""
        SELECT 1 FROM matches
        WHERE match_id = ? AND (student1_id = ? OR student2_id = ?)
    """, (match_id, current_user_id, current_user_id))

    if not cursor.fetchone():
        db.close()
        raise HTTPException(status_code=403, detail="Not authorized to send messages in this chat")

    try:
        # Insert user message
        cursor.execute("""
            INSERT INTO chat_messages (match_id, sender_id, message, is_llm, created_at)
            VALUES (?, ?, ?, ?, ?)
        """, (match_id, current_user_id, message.message, message.is_llm, datetime.now()))
        db.commit()

        if message.message.startswith('/llm'):
            # Get chat history for context
            cursor.execute("""
                SELECT message
                FROM chat_messages
                WHERE match_id = ?
                ORDER BY created_at ASC
            """, (match_id,))

            chat_history = [dict(row) for row in cursor.fetchall()]

            # Get LLM response
            llm_response = get_llm_response(chat_history)

            # Insert LLM response
            cursor.execute("""
                INSERT INTO chat_messages (match_id, sender_id, message, is_llm, created_at)
                VALUES (?, ?, ?, ?, ?)
            """, (match_id, current_user_id, llm_response, True, datetime.now()))
            db.commit()

    except sqlite3.Error:
        db.close()
        raise HTTPException(status_code=500, detail="Failed to send message")

    db.close()
    return {"message": "Message sent successfully"}

# Search endpoints
@app.get("/students/search")
async def search_students(
    username: Optional[str] = None,
    major: Optional[str] = None,
    limit: int = 10,
    offset: int = 0,
    credentials: HTTPBasicCredentials = Depends(security)
):
    authenticate_user(credentials)
    db = get_db()
    cursor = db.cursor()

    query = "SELECT student_id, username, major, description FROM students WHERE 1=1"
    params = []

    if username:
        query += " AND username LIKE ?"
        params.append(f"%{username}%")

    if major:
        query += " AND major = ?"
        params.append(major)

    query += " LIMIT ? OFFSET ?"
    params.extend([limit, offset])

    cursor.execute(query, params)
    students = [dict(row) for row in cursor.fetchall()]
    db.close()
    return students

@app.delete("/account/")
async def delete_account(credentials: HTTPBasicCredentials = Depends(security)):
    current_user_id = authenticate_user(credentials)
    db = get_db()
    cursor = db.cursor()

    try:
        # Start transaction
        cursor.execute("BEGIN TRANSACTION")

        # First, set is_free to TRUE for any matched students
        cursor.execute("""
            UPDATE students
            SET is_free = TRUE
            WHERE student_id IN (
                SELECT student2_id FROM matches WHERE student1_id = ?
                UNION
                SELECT student1_id FROM matches WHERE student2_id = ?
            )
        """, (current_user_id, current_user_id))

        # Update matches to mark them as inactive
        cursor.execute("""
            UPDATE matches
            SET status = 'inactive'
            WHERE student1_id = ? OR student2_id = ?
        """, (current_user_id, current_user_id))

        # Delete the student
        cursor.execute("""
            DELETE FROM students
            WHERE student_id = ?
        """, (current_user_id,))

        # Commit transaction
        cursor.execute("COMMIT")
        db.close()
        return {"message": "Account deleted successfully"}

    except sqlite3.Error as e:
        cursor.execute("ROLLBACK")
        db.close()
        raise HTTPException(
            status_code=500,
            detail=f"Failed to delete account: {str(e)}"
        )

@app.delete("/chat/{match_id}")
async def delete_chat(
    match_id: int,
    credentials: HTTPBasicCredentials = Depends(security)
):
    current_user_id = authenticate_user(credentials)
    db = get_db()
    cursor = db.cursor()

    # Verify user is part of the match
    cursor.execute("""
        SELECT student1_id, student2_id
        FROM matches
        WHERE match_id = ? AND (student1_id = ? OR student2_id = ?)
    """, (match_id, current_user_id, current_user_id))

    match = cursor.fetchone()
    if not match:
        db.close()
        raise HTTPException(
            status_code=403,
            detail="Not authorized to delete this chat"
        )

    try:
        # Start transaction
        cursor.execute("BEGIN TRANSACTION")

        # Set both students as free
        cursor.execute("""
            UPDATE students
            SET is_free = TRUE
            WHERE student_id IN (?, ?)
        """, (match['student1_id'], match['student2_id']))

        # Mark match as inactive
        cursor.execute("""
            UPDATE matches
            SET status = 'inactive'
            WHERE match_id = ?
        """, (match_id,))

        # Commit transaction
        cursor.execute("COMMIT")
        db.close()
        return {"message": "Chat deleted successfully"}

    except sqlite3.Error as e:
        cursor.execute("ROLLBACK")
        db.close()
        raise HTTPException(
            status_code=500,
            detail=f"Failed to delete chat: {str(e)}"
        )

@app.get("/docs", include_in_schema=False)
async def custom_swagger_ui_html():
    return get_swagger_ui_html(
        openapi_url="/openapi.json",
        title="API Docs"
    )

if __name__ == "__main__":
    import uvicorn
    uvicorn.run(app, host="0.0.0.0", port=8000)