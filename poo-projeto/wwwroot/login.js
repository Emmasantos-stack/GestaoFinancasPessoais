// ---------------------------------------------------------
// login.js
// Responsável por tratar o login do utilizador
// ---------------------------------------------------------

document.addEventListener("DOMContentLoaded", () => {

  const form = document.getElementById("formLogin");
  const msg = document.getElementById("loginMsg");

  if (!form || !msg) {
    console.error("Elementos do login não encontrados");
    return;
  }

  // Se já estiver autenticado, vai direto para o index
  if (window.Session && Session.getToken()) {
    window.location.href = "index.html";
    return;
  }

  form.addEventListener("submit", async (e) => {
    e.preventDefault();
    msg.textContent = "";

    const emailInput = document.getElementById("email");
    const passwordInput = document.getElementById("password");

    const email = emailInput?.value.trim();
    const password = passwordInput?.value;

    if (!email || !password) {
      msg.textContent = "Preencha email e password.";
      msg.style.color = "red";
      return;
    }

    try {
      const res = await fetch("/api/login", {
        method: "POST",
        headers: {
          "Content-Type": "application/json"
        },
        body: JSON.stringify({ email, password })
      });

      if (res.status === 401) {
        msg.textContent = "Credenciais inválidas.";
        msg.style.color = "red";
        return;
      }

      if (!res.ok) {
        const text = await res.text();
        console.error("Erro servidor:", text);
        msg.textContent = "Erro no servidor.";
        msg.style.color = "red";
        return;
      }

      const data = await res.json();

      // Guarda sessão
      Session.setToken(data.token);
      Session.setUser(data.user);

      msg.textContent = "Login bem-sucedido. A redirecionar...";
      msg.style.color = "green";

      setTimeout(() => {
        window.location.href = "index.html";
      }, 500);

    } catch (err) {
      console.error("Erro fetch:", err);
      msg.textContent = "Erro de rede.";
      msg.style.color = "red";
    }
  });
});
