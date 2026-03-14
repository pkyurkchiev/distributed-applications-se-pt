from fastapi import FastAPI, Depends, HTTPException, status, Query
from fastapi.security import OAuth2PasswordBearer, OAuth2PasswordRequestForm
from sqlalchemy import create_engine, Column, Integer, String, Boolean, DateTime, ForeignKey, Float
from sqlalchemy.orm import sessionmaker, declarative_base, Session
from jose import JWTError, jwt
from passlib.context import CryptContext
from datetime import datetime, timedelta
from pydantic import BaseModel, Field, EmailStr
from typing import List, Optional

DATABASE_URL = "sqlite:///./martial_store.db"
engine = create_engine(DATABASE_URL, connect_args={"check_same_thread": False})
SessionLocal = sessionmaker(autocommit=False, autoflush=False, bind=engine)
Base = declarative_base()

class Category(Base):
    __tablename__ = "categories"
    id = Column(Integer, primary_key=True, index=True)
    name = Column(String(100), nullable=False)
    description = Column(String(255))
    is_active = Column(Boolean, default=True, nullable=False)
    display_order = Column(Integer, nullable=False)
    created_at = Column(DateTime, default=datetime.utcnow)

class Product(Base):
    __tablename__ = "products"
    id = Column(Integer, primary_key=True, index=True)
    name = Column(String(150), nullable=False)
    price = Column(Float, nullable=False)
    stock_quantity = Column(Integer, nullable=False)
    weight = Column(Float)
    category_id = Column(Integer, ForeignKey("categories.id"), nullable=False)
    created_at = Column(DateTime, default=datetime.utcnow)

class Customer(Base):
    __tablename__ = "customers"
    id = Column(Integer, primary_key=True, index=True)
    first_name = Column(String(100), nullable=False)
    last_name = Column(String(100), nullable=False)
    email = Column(String(150), unique=True, nullable=False)
    phone = Column(String(20))
    is_active = Column(Boolean, default=True, nullable=False)
    date_registered = Column(DateTime, default=datetime.utcnow)

class Order(Base):
    __tablename__ = "orders"
    id = Column(Integer, primary_key=True, index=True)
    customer_id = Column(Integer, ForeignKey("customers.id"), nullable=False)
    product_id = Column(Integer, ForeignKey("products.id"), nullable=False)
    quantity = Column(Integer, nullable=False)
    total_amount = Column(Float, nullable=False)
    status = Column(String(50), default="Pending", nullable=False)
    is_paid = Column(Boolean, default=False, nullable=False)
    order_date = Column(DateTime, default=datetime.utcnow)

class User(Base):
    __tablename__ = "users"
    id = Column(Integer, primary_key=True)
    username = Column(String(100), unique=True, nullable=False)
    hashed_password = Column(String(255), nullable=False)

Base.metadata.create_all(bind=engine)

class ProductBase(BaseModel):
    name: str = Field(..., max_length=150)
    price: float = Field(..., gt=0)
    stock_quantity: int = Field(..., ge=0)
    weight: Optional[float] = None
    category_id: int

class ProductCreate(ProductBase): pass

class ProductResponse(ProductBase):
    id: int
    created_at: datetime
    class Config: orm_mode = True

class CustomerBase(BaseModel):
    first_name: str = Field(..., max_length=100)
    last_name: str = Field(..., max_length=100)
    email: EmailStr
    phone: Optional[str] = Field(None, max_length=20)
    is_active: bool = True

class CustomerCreate(CustomerBase): pass

class CustomerResponse(CustomerBase):
    id: int
    date_registered: datetime
    class Config: orm_mode = True

class OrderBase(BaseModel):
    customer_id: int
    product_id: int
    quantity: int = Field(..., gt=0)
    total_amount: float = Field(..., gt=0)
    status: str = Field("Pending", max_length=50)
    is_paid: bool = False

class OrderCreate(OrderBase): pass

class OrderResponse(OrderBase):
    id: int
    order_date: datetime
    class Config: orm_mode = True

SECRET_KEY = "martial_arts_super_secret"
ALGORITHM = "HS256"
pwd_context = CryptContext(schemes=["bcrypt"], deprecated="auto")
oauth2_scheme = OAuth2PasswordBearer(tokenUrl="login")

def get_db():
    db = SessionLocal()
    try: yield db
    finally: db.close()

def get_current_user(token: str = Depends(oauth2_scheme), db: Session = Depends(get_db)):
    try:
        payload = jwt.decode(token, SECRET_KEY, algorithms=[ALGORITHM])
        username = payload.get("sub")
        user = db.query(User).filter(User.username == username).first()
        if not user: raise HTTPException(status_code=401)
        return user
    except JWTError: raise HTTPException(status_code=401)

app = FastAPI(title="Martial Arts Store API")

@app.post("/register", tags=["Auth"])
def register(username: str, password: str, db: Session = Depends(get_db)):
    hashed = pwd_context.hash(password)
    user = User(username=username, hashed_password=hashed)
    db.add(user)
    db.commit()
    return {"message": "Потребителят е създаден"}

@app.post("/login", tags=["Auth"])
def login(form_data: OAuth2PasswordRequestForm = Depends(), db: Session = Depends(get_db)):
    user = db.query(User).filter(User.username == form_data.username).first()
    if not user or not pwd_context.verify(form_data.password, user.hashed_password):
        raise HTTPException(status_code=400, detail="Грешно име или парола")
    token = jwt.encode({"sub": user.username, "exp": datetime.utcnow() + timedelta(hours=2)}, SECRET_KEY, algorithm=ALGORITHM)
    return {"access_token": token, "token_type": "bearer"}

@app.post("/products", response_model=ProductResponse, tags=["Products"])
def create_product(product: ProductCreate, db: Session = Depends(get_db), user=Depends(get_current_user)):
    db_obj = Product(**product.dict())
    db.add(db_obj)
    db.commit()
    db.refresh(db_obj)
    return db_obj

@app.get("/products", response_model=List[ProductResponse], tags=["Products"])
def read_products(skip: int = 0, limit: int = 10, search: Optional[str] = None, db: Session = Depends(get_db), user=Depends(get_current_user)):
    query = db.query(Product)
    if search: query = query.filter(Product.name.contains(search))
    return query.offset(skip).limit(limit).all()

@app.put("/products/{id}", response_model=ProductResponse, tags=["Products"])
def update_product(id: int, data: ProductCreate, db: Session = Depends(get_db), user=Depends(get_current_user)):
    db_obj = db.query(Product).filter(Product.id == id).first()
    if not db_obj: raise HTTPException(status_code=404)
    for k, v in data.dict().items(): setattr(db_obj, k, v)
    db.commit()
    return db_obj

@app.delete("/products/{id}", tags=["Products"])
def delete_product(id: int, db: Session = Depends(get_db), user=Depends(get_current_user)):
    db_obj = db.query(Product).filter(Product.id == id).first()
    if not db_obj: raise HTTPException(status_code=404)
    db.delete(db_obj)
    db.commit()
    return {"message": "Изтрито"}

@app.post("/customers", response_model=CustomerResponse, tags=["Customers"])
def create_customer(customer: CustomerCreate, db: Session = Depends(get_db), user=Depends(get_current_user)):
    db_obj = Customer(**customer.dict())
    db.add(db_obj)
    db.commit()
    db.refresh(db_obj)
    return db_obj

@app.get("/customers", response_model=List[CustomerResponse], tags=["Customers"])
def read_customers(skip: int = 0, limit: int = 10, email: Optional[str] = None, db: Session = Depends(get_db), user=Depends(get_current_user)):
    query = db.query(Customer)
    if email: query = query.filter(Customer.email == email)
    return query.offset(skip).limit(limit).all()

@app.put("/customers/{id}", response_model=CustomerResponse, tags=["Customers"])
def update_customer(id: int, data: CustomerCreate, db: Session = Depends(get_db), user=Depends(get_current_user)):
    db_obj = db.query(Customer).filter(Customer.id == id).first()
    if not db_obj: raise HTTPException(status_code=404)
    for k, v in data.dict().items(): setattr(db_obj, k, v)
    db.commit()
    return db_obj

@app.delete("/customers/{id}", tags=["Customers"])
def delete_customer(id: int, db: Session = Depends(get_db), user=Depends(get_current_user)):
    db_obj = db.query(Customer).filter(Customer.id == id).first()
    if not db_obj: raise HTTPException(status_code=404)
    db.delete(db_obj)
    db.commit()
    return {"message": "Изтрито"}

# --- ORDERS CRUD ---
@app.post("/orders", response_model=OrderResponse, tags=["Orders"])
def create_order(order: OrderCreate, db: Session = Depends(get_db), user=Depends(get_current_user)):
    db_obj = Order(**order.dict())
    db.add(db_obj)
    db.commit()
    db.refresh(db_obj)
    return db_obj

@app.get("/orders", response_model=List[OrderResponse], tags=["Orders"])
def read_orders(skip: int = 0, limit: int = 10, status: Optional[str] = None, db: Session = Depends(get_db), user=Depends(get_current_user)):
    query = db.query(Order)
    if status: query = query.filter(Order.status == status)
    return query.offset(skip).limit(limit).all()

@app.put("/orders/{id}", response_model=OrderResponse, tags=["Orders"])
def update_order(id: int, data: OrderCreate, db: Session = Depends(get_db), user=Depends(get_current_user)):
    db_obj = db.query(Order).filter(Order.id == id).first()
    if not db_obj: raise HTTPException(status_code=404)
    for k, v in data.dict().items(): setattr(db_obj, k, v)
    db.commit()
    return db_obj

@app.delete("/orders/{id}", tags=["Orders"])
def delete_order(id: int, db: Session = Depends(get_db), user=Depends(get_current_user)):
    db_obj = db.query(Order).filter(Order.id == id).first()
    if not db_obj: raise HTTPException(status_code=404)
    db.delete(db_obj)
    db.commit()
    return {"message": "Изтрито"}