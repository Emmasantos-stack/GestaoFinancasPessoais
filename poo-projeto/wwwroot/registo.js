// =============================
// registo.js
// Responsável pela criação de novas contas de utilizador
// =============================

// Endpoint da API de utilizadores
const API_UTILIZADOR = "/api/utilizador";

// Associa o evento de submissão ao formulário
document.addEventListener("DOMContentLoaded", () => {
    document
        .getElementById("formRegisto")
        .addEventListener("submit", criarConta);
});

/**
 * Cria uma nova conta de utilizador
 */
async function criarConta(event) {
    event.preventDefault();

    // Obtém os valores introduzidos
    const nome = document.getElementById("nome").value.trim();
    const email = document.getElementById("email").value.trim();
    const password = document.getElementById("password").value;
    const perfil = "user";

    // Validação básica
    if (!nome || !email || !password) {
        mostrarMensagem("Preencha todos os campos.", true);
        return;
    }

    // Objeto enviado para a API
    const novoUser = { nome, email, password, perfil };

    try {
        const response = await fetch(API_UTILIZADOR, {
            method: "POST",
            headers: { "Content-Type": "application/json" },
            body: JSON.stringify(novoUser)
        });

        if (!response.ok) {
            mostrarMensagem("Erro ao criar conta. Email pode já existir.", true);
            return;
        }

        mostrarMensagem("Conta criada com sucesso! Redirecionando...");

        // Redireciona para login
        setTimeout(() => {
            window.location.href = "login.html";
        }, 1200);

    } catch (err) {
        console.error(err);
        mostrarMensagem("Erro de comunicação com o servidor.", true);
    }
}

/**
 * Mostra mensagens ao utilizador
 */
function mostrarMensagem(texto, erro = false) {
    const msg = document.getElementById("registoMensagem");
    msg.textContent = texto;
    msg.style.color = erro ? "red" : "green";
}
