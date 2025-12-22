// =============================
// UTILIZADOR.JS
// Gestão de utilizadores via API REST (autenticada)
// =============================

// Obtém o utilizador autenticado
const user = Session.getUser();

// Sem sessão → login
if (!user) {
    window.location.href = "login.html";
}

// Apenas Administradores podem aceder
if (user.perfil !== "Administrador") {
    alert("Acesso restrito a administradores.");
    window.location.href = "index.html";
}

// Endpoint base
const API_UTILIZADOR = "/api/utilizador";

// =============================
// INIT
// =============================
document.addEventListener("DOMContentLoaded", () => {

    carregarUtilizador();

    document
        .getElementById("formUtilizador")
        .addEventListener("submit", criarUtilizador);
});


// =============================
// CRIAR UTILIZADOR
// =============================
async function criarUtilizador(event) {
    event.preventDefault();

    const nome = document.getElementById("nome").value.trim();
    const email = document.getElementById("email").value.trim();
    const password = document.getElementById("password").value;
    const perfil = document.getElementById("perfil").value;

    if (!nome || !email || !password) {
        mostrarMensagem("Preencha todos os campos.", true);
        return;
    }

    const novoUser = { nome, email, password, perfil };

    try {
        const response = await Session.authFetch(API_UTILIZADOR, {
            method: "POST",
            headers: { "Content-Type": "application/json" },
            body: JSON.stringify(novoUser)
        });

        if (!response.ok) {
            const erro = await response.text();
            mostrarMensagem(erro || "Erro ao criar utilizador.", true);
            return;
        }

        mostrarMensagem("Utilizador criado com sucesso!");
        document.getElementById("formUtilizador").reset();
        carregarUtilizador();

    } catch (err) {
        console.error(err);
        mostrarMensagem("Erro de comunicação com servidor.", true);
    }
}


// =============================
// CARREGAR UTILIZADORES
// =============================
async function carregarUtilizador() {
    try {
        const response = await Session.authFetch(API_UTILIZADOR);
        const utilizador = await response.json();
        renderUtilizador(utilizador);

    } catch (err) {
        console.error("Erro ao carregar utilizadores:", err);
    }
}


// =============================
// ELIMINAR UTILIZADOR
// =============================
async function eliminarUtilizador(id) {
    if (!confirm("Tem a certeza que deseja eliminar este utilizador?"))
        return;

    try {
        const response = await Session.authFetch(`${API_UTILIZADOR}/${id}`, {
            method: "DELETE"
        });

        if (!response.ok) {
            mostrarMensagem("Erro ao eliminar utilizador.", true);
            return;
        }

        mostrarMensagem("Utilizador removido.");
        carregarUtilizador();

    } catch (err) {
        console.error(err);
    }
}


// =============================
// EDITAR UTILIZADOR ✅
// =============================
async function editarUtilizador(id) {

    const nome = prompt("Novo nome:");
    if (!nome) return;

    const email = prompt("Novo email:");
    if (!email) return;

    const password = prompt("Nova password (mín. 4 caracteres):");
    if (!password || password.length < 4) {
        alert("Password inválida.");
        return;
    }

    const perfil = prompt("Perfil (Administrador ou Utilizador):");
    if (perfil !== "Administrador" && perfil !== "Utilizador") {
        alert("Perfil inválido.");
        return;
    }

    const dto = {
        nome: nome.trim(),
        email: email.trim(),
        password,
        perfil
    };

    try {
        const response = await Session.authFetch(`${API_UTILIZADOR}/${id}`, {
            method: "PUT",
            headers: { "Content-Type": "application/json" },
            body: JSON.stringify(dto)
        });

        if (!response.ok) {
            const erro = await response.text();
            mostrarMensagem(erro || "Erro ao editar utilizador.", true);
            return;
        }

        mostrarMensagem("Utilizador alterado com sucesso!");
        carregarUtilizador();

    } catch (err) {
        console.error(err);
        mostrarMensagem("Erro de comunicação com servidor.", true);
    }
}


// =============================
// RENDER TABELA
// =============================
function renderUtilizador(lista) {
    const tabela = document.getElementById("tabelaUtilizador");
    tabela.innerHTML = "";

    lista.forEach(u => {
        const tr = document.createElement("tr");
        tr.classList.add("table__row");

        tr.innerHTML = `
            <td class="table__cell">${u.id}</td>
            <td class="table__cell">${u.nome}</td>
            <td class="table__cell">${u.email}</td>
            <td class="table__cell">${u.perfil}</td>
            <td class="table__cell table__cell--acoes">
                <button class="button button--small"
                        onclick="editarUtilizador(${u.id})">
                    Editar
                </button>
                <button class="button button--danger"
                        onclick="eliminarUtilizador(${u.id})">
                    Eliminar
                </button>
            </td>
        `;

        tabela.appendChild(tr);
    });
}


// =============================
// MENSAGENS
// =============================
function mostrarMensagem(texto, erro = false) {
    const msg = document.getElementById("utilizadorMensagem");
    msg.textContent = texto;
    msg.style.color = erro ? "red" : "green";
    setTimeout(() => msg.textContent = "", 3000);
}
