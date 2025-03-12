import React, { useState, useEffect } from 'react'
import './App.css'

function App() {
    let username = "";
    let password = "";
    let matchId = null;
    const [formData, setFormData] = useState({
        name: '',
        description: '',
        price: ''
    });

    function register() {
        let regUsername = document.getElementById("reg-username");
        let regPassword = document.getElementById("reg-password");
        let regMajor = document.getElementById("reg-major");
        let regDescription = document.getElementById("reg-description");
        if (
            regUsername &&
            regPassword &&
            regMajor &&
            regDescription
        ) {
            let user = regUsername.value;
            let pass = regPassword.value;
            let major = regMajor.value;
            let description = regDescription.value;

            console.log("probvam fetch");
            fetch("http://localhost:8000/register/", {
                method: "POST",
                headers: { "Content-Type": "application/json" },
                body: JSON.stringify({ username: user, password: pass, major: major, description: description })
            })
                .then(response => response.json())
                .then(data => alert("registered! student id: " + data.student_id))
                .catch(error => console.error("error:", error));
            setFormData({
                name: '',
                description: '',
                price: ''
            });
        } else {
            alert("Fill in all fields ");
        }
        checkIfLogged();
    }



    function checkIfLogged() {
        if (username != "") {
            //logged
            showLogged();
        } else {
            //not logged
            showNotLogged();
        }
    }

    function showLogged() {
        var x = document.getElementById("RegisterDiv");
        if (x) x.style.display = "none";
        var x = document.getElementById("LoginDiv");
        if (x) x.style.display = "none";
        var x = document.getElementById("MatchDiv");
        if (x) x.style.display = "block";
        var x = document.getElementById("CompatibleDiv");
        if (x) x.style.display = "none";
        var x = document.getElementById("SearchDiv");
        if (x) x.style.display = "block";
        var x = document.getElementById("ChatDiv");
        if (x) x.style.display = "none";
        var x = document.getElementById("DeleteAccDiv");
        if (x) x.style.display = "block";
        var x = document.getElementById("DeleteChatDiv");
        if (x) x.style.display = "block";
    }
    function showNotLogged() {
        var x = document.getElementById("RegisterDiv");
        if (x) x.style.display = "block";
        var x = document.getElementById("LoginDiv");
        if (x) x.style.display = "block";
        var x = document.getElementById("MatchDiv");
        if (x) x.style.display = "none";
        var x = document.getElementById("ChatDiv");
        if (x) x.style.display = "none";
        var x = document.getElementById("CompatibleDiv");
        if (x) x.style.display = "none";
        var x = document.getElementById("SearchDiv");
        if (x) x.style.display = "none";
        var x = document.getElementById("DeleteAccDiv");
        if (x) x.style.display = "none";
    }

    function login() {
        let loginUsername = document.getElementById("login-username");
        let loginPassword = document.getElementById("login-password");
        if (loginUsername && loginPassword) {
            username = loginUsername.value;
            password = loginPassword.value;
            fetch("http://localhost:8000/login/", {
                method: "POST",
                headers: { "Content-Type": "application/json" },
                body: JSON.stringify({ username: username, password: password })
            })
                .then(response => response.json())
                .then(data => alert("Logged In student id: " + data['user_id']))
                .catch(error => console.error("error:", error));

            getChatHistory2();
        } else {
            alert("Fill in all the fields!");
        }
        checkIfLogged();
    }

    function findMatch() {
        fetch("http://localhost:8000/compatible-students/", {
            method: "GET",
            headers: { "Authorization": "Basic " + btoa(username + ":" + password) }
        })
            .then(response => response.json())
            .then(data => {
                let container = document.getElementById("compatible-students");
                container.innerHTML = "<h3>Compatible Students:</h3>";
                if (data.students.length === 0) {
                    container.innerHTML += "<p>No compatible students found at the moment.</p>";
                } else {
                    data.students.forEach(student => {
                        container.innerHTML += `
                            <div class="compatible-student">
                                <p><strong>Username:</strong> ${student.username}</p>
                                <p><strong>Major:</strong> ${student.major}</p>
                                <p><strong>Description:</strong> ${student.description || 'No description provided.'}</p>
                                <button onClick=`+ selectMatch(student.student_id)`>Select</button>
                            </div>
                        `;
                    });
                }
            })
            .catch(error => console.error("error:", error));
        checkIfLogged();// <button onClick={deleteChat}>delete chat</button>
    }

    function selectMatch(studentId) {
        fetch(`http://localhost:8000/match/${studentId}`, {
            method: "POST",
            headers: { "Authorization": "Basic " + btoa(username + ":" + password) }
        })
            .then(response => response.json())
            .then(data => {
                if (data.match_id) {
                    matchId = data.match_id;
                    alert("Matched with " + data.matched_username + "! Match ID: " + matchId);
                    getChatHistory();
                    var x = document.getElementById("ChatDiv");
                    if (x) x.style.display = "block";
                    var x = document.getElementById("MatchDiv");
                    if (x) x.style.display = "none";
                    var x = document.getElementById("CompatibleDiv");
                    if (x) x.style.display = "none";
                } else {
                    alert("Failed to create match.");
                }
            })
            .catch(error => console.error("error:", error));
        checkIfLogged();
    }


    function getChatHistory() {
        if (!matchId) return;
        fetch(`http://localhost:8000/chat/${matchId}?limit=10&offset=0`, {
            method: "GET",
            headers: { "Authorization": "Basic " + btoa(username + ":" + password) }
        })
            .then(response => response.json())
            .then(data => {
                let chatBox = document.getElementById("chat-box");
                chatBox.innerHTML = "";
                data.messages.forEach(msg => {
                    let sender = msg.is_llm ? "LLM" : msg.sender_username;
                    chatBox.innerHTML += `<p><strong>${sender}:</strong> ${msg.message}</p>`;
                });
            })
            .catch(error => console.error("error:", error));
        var x = document.getElementById("SearchDiv");
        if (x) x.style.display = "none";
    }

    function getChatHistory2() {
        let loginUsername = document.getElementById("login-username");
        let loginPassword = document.getElementById("login-password");
        if (loginUsername && loginPassword) {
            username = loginUsername.value;
            password = loginPassword.value;
            fetch("http://localhost:8000/student/chats/", {
                method: "POST",
                headers: { "Content-Type": "application/json" },
                body: JSON.stringify({ username: username, password: password })
            })
                .then(response => response.json())
                .then(data => console.log("Logged In student id: " + data['']))
                .catch(error => console.error("error:", error));
        }
    }

    function sendMessage() {
        if (!matchId) {
            alert("you need to be in a match first");
            return;
        }
        let message = document.getElementById("message-input").value;
        fetch(`http://localhost:8000/chat/${matchId}`, {
            method: "POST",
            headers: {
                "Content-Type": "application/json",
                "Authorization": "Basic " + btoa(username + ":" + password)
            },
            body: JSON.stringify({ message: message })
        })
            .then(() => {
                getChatHistory();
            })
            .catch(error => console.error("error:", error));
    }

    function askLLM() {
        if (!matchId) {
            alert("you need to be in a match first");
            return;
        }
        let message = "/llm " + document.getElementById("message-input").value;
        fetch(`http://localhost:8000/chat/${matchId}`, {
            method: "POST",
            headers: {
                "Content-Type": "application/json",
                "Authorization": "Basic " + btoa(username + ":" + password)
            },
            body: JSON.stringify({ message: message })
        })
            .then(() => {
                setTimeout(getChatHistory, 3000);
            })
            .catch(error => console.error("error:", error));
        var x = document.getElementById("SearchDiv");
        if (x) x.style.display = "none";
    }


    function searchStudents() {
        let searchUsername = document.getElementById("search-username").value;
        fetch(`http://localhost:8000/students/search?username=${searchUsername}`, {
            method: "GET",
            headers: { "Authorization": "Basic " + btoa(username + ":" + password) }
        })
            .then(response => response.json())
            .then(data => {
                let container = document.getElementById("search-students");
                container.innerHTML = "<h3>Search Results:</h3>";
                data.forEach(student => {
                    container.innerHTML += `
                        <p>Username: ${student.username}, Major: ${student.major}, Description: ${student.description || 'No description provided.'}</p>
                    `;
                });
            })
            .catch(error => console.error("error:", error));
        checkIfLogged();
    }

    function deleteAccount() {
        if (!confirm("Are you sure you want to delete your account? This action is irreversible.")) {
            return;
        }
        fetch("http://localhost:8000/account/", {
            method: "DELETE",
            headers: { "Authorization": "Basic " + btoa(username + ":" + password) }
        })
            .then(response => {
                if (response.ok) {
                    alert("Account deleted successfully.");
                    // Redirect to home or login page after deletion
                    window.location.href = "./index.html"; // Or any other appropriate page
                } else {
                    alert("Failed to delete account.");
                }
            })
            .catch(error => console.error("Error deleting account:", error));
        checkIfLogged();
    }

    function deleteChat() {
        if (!matchId) {
            alert("No active chat to delete.");
            return;
        }
        if (!confirm("Are you sure you want to delete this chat? This will also unmatch you with your study buddy.")) {
            return;
        }
        fetch(`http://localhost:8000/chat/${matchId}`, {
            method: "DELETE",
            headers: { "Authorization": "Basic " + btoa(username + ":" + password) }
        })
            .then(response => {
                if (response.ok) {
                    alert("Chat deleted successfully.");
                    matchId = null; // Reset matchId
                    document.getElementById("chat-box").innerHTML = ""; // Clear chat box
                } else {
                    alert("Failed to delete chat.");
                }
            })
            .catch(error => console.error("Error deleting chat:", error));
        checkIfLogged();
    }









    return (
        <div>
            <table width='100%'>
                <tr>
                    <td><h1>StudyBuddy</h1></td><td><div id="DeleteAccDiv" class="hidden">
                        <button onClick={deleteAccount}><h9>delete account</h9></button>
                    </div></td>



                </tr>
            </table>

            {/* Form Section */}
            <div className='container my-5'>
                <div className='row justify-content-center'>
                    <div className='col-md-6'>
                        <div id="RegisterDiv" class="shown">
                            <h2>register</h2>
                            <table>
                                <tr>
                                    <td>
                                        <input type="text" id="reg-username" placeholder="username"></input>
                                    </td><td>

                                        <input type="password" id="reg-password" placeholder="password"></input>
                                    </td></tr><tr><td>
                                        <input type="text" id="reg-major" placeholder="major"></input>
                                    </td><td><input type="text" id="reg-description" placeholder="Short description about yourself (TLDR)"></input>
                                    </td></tr></table><p></p>
                            <button onClick={register}>register</button>
                        </div>
                        <div id="LoginDiv" class="shown">

                            <h2>or login</h2><table><tr><td>
                                <input type="text" id="login-username" placeholder="username"></input>
                            </td><td><input type="password" id="login-password" placeholder="password"></input>
                                </td></tr></table><p></p><button onClick={login}>login</button>
                        </div>
                        <table>
                            <tr>
                                <td>
                                    <div id="MatchDiv" class="hidden">
                                        <h2>match with a study buddy</h2>
                                        <button onClick={findMatch}>find match</button>
                                    </div>
                                </td>
                                <td>
                                    <div id="SearchDiv" class="hidden">
                                        <h2>or search for one</h2>
                                        <input type="text" id="search-username" placeholder="username"></input>
                                        <button onClick={searchStudents}>search</button>
                                        <div id="search-students"></div>
                                    </div>
                                </td>
                            </tr>
                        </table>


                        <div id="ChatDiv" class="hidden">
                            <h2>chat</h2>
                            <div id="chat-box"></div>
                            <input type="text" id="message-input" placeholder="type a message"></input>
                            <button onClick={sendMessage}>send</button>
                            <button onClick={askLLM}>ask llm</button>
                            <button onClick={deleteChat}>delete chat</button>
                        </div>
                        <div id="CompatibleDiv" class="hidden">
                            <div id="compatible-students"></div>
                        </div>
                        <div id="SearchDiv" class="hidden">                            <h2>search students</h2>
                            <input type="text" id="search-username" placeholder="username"></input>
                            <button onClick={searchStudents}>search students</button>
                            <div id="search-students"></div>
                        </div>


                    </div>
                </div>
            </div>

        </div>
    )

}

export default App
