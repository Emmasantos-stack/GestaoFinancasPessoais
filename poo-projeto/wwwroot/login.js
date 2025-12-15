// ---------------------------------------------------------
// Responsável por tratar o login do utilizador no sistema. Comunica com a API para validar email e password. Guarda os dados da sessão após login bem-sucedido.
// ---------------------------------------------------------

// Aguarda que o HTML esteja totalmente carregado
document.addEventListener("DOMContentLoaded", () => {

  // Obtém o formulário de login
  const form = document.getElementById("formLogin");

  // Elemento onde serão mostradas mensagens ao utilizador
  const msg = document.getElementById("loginMsg");

  // Evento acionado quando o utilizador submete o formulário
  form.addEventListener("submit", async (e) => {
    e.preventDefault(); // Evita o comportamento padrão do formulário
    msg.textContent = ""; // Limpa mensagens anteriores

    // Obtém os valores introduzidos pelo utilizador
    const email = document.getElementById("email").value.trim();
    const password = document.getElementById("password").value;

    // Validação básica dos campos
    if (!email || !password) {
      msg.textContent = "Preencha email e password.";
      msg.style.color = "red";
      return;
    }

    try {
      // Pedido à API para autenticação
      const res = await fetch("/api/login", {
        method: "POST",
        headers: {
          "Content-Type": "application/json"
        },
        body: JSON.stringify({ email, password })
      });

      // Caso as credenciais estejam erradas
      if (res.status === 401) {
        msg.textContent = "Credenciais inválidas.";
        msg.style.color = "red";
        return;
      }

      // Outros erros do servidor
      if (!res.ok) {
        msg.textContent = "Erro no servidor.";
        msg.style.color = "red";
        return;
      }

      // Conversão da resposta para JSON
      const data = await res.json();

      // Estrutura esperada:
      // {
      //   token: "...",
      //   user: { id, nome, email, perfil }
      // }

      // Guarda o token e os dados do utilizador na sessão
      window.Session.setToken(data.token);
      window.Session.setUser(data.user);

      // Mensagem de sucesso
      msg.textContent = "Login bem-sucedido. A redirecionar...";
      msg.style.color = "green";

      // Redireciona para a página principal após pequeno atraso
      setTimeout(() => {
        window.location.href = "transacoes.html";
      }, 700);

    } catch (err) {
      // Erro de ligação à API (ex: servidor desligado)
      console.error(err);
      msg.textContent = "Erro de rede.";
      msg.style.color = "red";
    }
  });
});
