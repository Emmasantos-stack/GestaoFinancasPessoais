// =============================
// REGISTO.JS
// Página de criação de conta
// =============================

const API_UTILIZADOR = "/api/utilizador";

document.addEventListener("DOMContentLoaded", () => {
    document
        .getElementById("formRegisto")
        .addEventListener("submit", criarConta);
});

async function criarConta(event) {
    event.preventDefault();

    const nome = document.getElementById("nome").value.trim();
    const email = document.getElementById("email").value.trim();
    const password = document.getElementById("password").value;
    const perfil = "Utilizador";

    if (!nome || !email || !password) {
        mostrarMensagem("Preencha todos os campos.", true);
        return;
    }

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

        setTimeout(() => {
            window.location.href = "login.html";
        }, 1200);

    } catch (err) {
        console.error(err);
        mostrarMensagem("Erro de comunicação com o servidor.", true);
    }
}

function mostrarMensagem(texto, erro = false) {
    const msg = document.getElementById("registoMensagem");
    msg.textContent = texto;
    msg.style.color = erro ? "red" : "green";
}
