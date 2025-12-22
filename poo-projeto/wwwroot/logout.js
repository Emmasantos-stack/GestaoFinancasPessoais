function logout() {
    Session.clearToken();
    window.location.href = "login.html";
}
