// =============================
// UTILIZADORES.JS
// Gestão de utilizadores via API REST
// =============================

const API_UTILIZADORES = "/api/utilizadores";

document.addEventListener("DOMContentLoaded", () => {
    carregarUtilizadores();

    document.getElementById("formUtilizador")
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
        const response = await fetch(API_UTILIZADORES, {
            method: "POST",
            headers: { "Content-Type": "application/json" },
            body: JSON.stringify(novoUser)
        });

        if (!response.ok) {
            mostrarMensagem("Erro ao criar utilizador.", true);
            return;
        }

        mostrarMensagem("Utilizador criado com sucesso!");

        document.getElementById("formUtilizador").reset();
        carregarUtilizadores();

    } catch (err) {
        console.error(err);
        mostrarMensagem("Erro de comunicação com servidor.", true);
    }
}



// =============================
// CARREGAR UTILIZADORES
// =============================
async function carregarUtilizadores() {
    try {
        const response = await fetch(API_UTILIZADORES);
        const utilizadores = await response.json();

        renderTabela(utilizadores);

    } catch (err) {
        console.error("Erro ao carregar utilizadores:", err);
    }
}


// =============================
// ELIMINAR UTILIZADOR
// =============================
async function eliminarUtilizador(id) {
    if (!confirm("Tem a certeza que deseja eliminar este utilizador?")) return;

    try {
        const response = await fetch(`${API_UTILIZADORES}/${id}`, { method: "DELETE" });

        if (!response.ok) {
            mostrarMensagem("Erro ao eliminar utilizador.", true);
            return;
        }

        mostrarMensagem("Utilizador removido.");
        carregarUtilizadores();

    } catch (err) {
        console.error(err);
    }
}


// =============================
// RENDERIZAR TABELA
// =============================
function renderTabela(lista) {
    const tabela = document.getElementById("tabelaUtilizadores");
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
                <button class="button button--small" onclick="editarUtilizador(${u.id})">Editar</button>
                <button class="button button--danger" onclick="eliminarUtilizador(${u.id})">Eliminar</button>
            </td>
        `;

        tabela.appendChild(tr);
    });
}


// =============================
// MENSAGEM AO UTILIZADOR
// =============================
function mostrarMensagem(texto, erro = false) {
    const msg = document.getElementById("utilizadorMensagem");
    msg.textContent = texto;
    msg.style.color = erro ? "red" : "green";
    setTimeout(() => msg.textContent = "", 3000);
}


// =============================
// PLACEHOLDER - EDITAR
// =============================
function editarUtilizador(id) {
    alert("TODO: Implementar edição de utilizador.");
}
