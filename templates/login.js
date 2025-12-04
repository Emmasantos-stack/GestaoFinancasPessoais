// login.js (usa window.Session)
document.addEventListener("DOMContentLoaded", () => {
  const form = document.getElementById("formLogin");
  const msg = document.getElementById("loginMsg");

  form.addEventListener("submit", async (e) => {
    e.preventDefault();
    msg.textContent = "";

    const email = document.getElementById("email").value.trim();
    const password = document.getElementById("password").value;

    if (!email || !password) {
      msg.textContent = "Preencha email e password.";
      msg.style.color = "red";
      return;
    }

    try {
      const res = await fetch("/api/login", {
        method: "POST",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify({ email, password })
      });

      if (res.status === 401) {
        msg.textContent = "Credenciais inválidas.";
        msg.style.color = "red";
        return;
      }

      if (!res.ok) {
        msg.textContent = "Erro no servidor.";
        msg.style.color = "red";
        return;
      }

      const data = await res.json();
      // esperado: { token: "...", user: { id, nome, email, perfil } }
      window.Session.setToken(data.token);
      window.Session.setUser(data.user);

      msg.textContent = "Login bem-sucedido. A redirecionar...";
      msg.style.color = "green";

      setTimeout(() => {
        // vai para a página principal / transacoes
        window.location.href = "transacoes.html";
      }, 700);

    } catch (err) {
      console.error(err);
      msg.textContent = "Erro de rede.";
      msg.style.color = "red";
    }
  });
});
