// =============================
// UTILIZADOR.JS
// Gestão de utilizadores via API REST (autenticada)
// =============================

// Obtém o utilizador autenticado da sessão
const user = Session.getUser();

// Caso não exista sessão ativa, redireciona para o login
if (!user) {
    window.location.href = "login.html";
}

// Endpoint base da API de utilizadores
const API_UTILIZADOR = "/api/utilizador";

// Executado quando a página termina de carregar
document.addEventListener("DOMContentLoaded", () => {

    // Restrição de acesso (opcional mas recomendada)
    // Apenas utilizadores com perfil Admin podem aceder
    const user = Session.getUser();
    if (user && user.perfil !== "Admin") {
        alert("Acesso restrito a administradores.");
        window.location.href = "index.html";
        return;
    }

    // Carrega a lista de utilizadores existentes
    carregarUtilizador();

    // Associa o evento de submissão do formulário
    document
        .getElementById("formUtilizador")
        .addEventListener("submit", criarUtilizador);
});


// =============================
// CRIAR UTILIZADOR
// =============================
// Envia um pedido POST para criar um novo utilizador
async function criarUtilizador(event) {
    event.preventDefault();

    // Obtém os valores introduzidos no formulário
    const nome = document.getElementById("nome").value.trim();
    const email = document.getElementById("email").value.trim();
    const password = document.getElementById("password").value;
    const perfil = document.getElementById("perfil").value;

    // Validação básica dos campos
    if (!nome || !email || !password) {
        mostrarMensagem("Preencha todos os campos.", true);
        return;
    }

    // Objeto a enviar para a API
    const novoUser = { nome, email, password, perfil };

    try {
        const response = await Session.authFetch(API_UTILIZADOR, {
            method: "POST",
            headers: { "Content-Type": "application/json" },
            body: JSON.stringify(novoUser)
        });

        // Caso a API devolva erro
        if (!response.ok) {
            mostrarMensagem("Erro ao criar utilizador.", true);
            return;
        }

        // Sucesso
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
// Obtém todos os utilizadores da API
async function carregarUtilizador() {
    try {
        const response = await Session.authFetch(API_UTILIZADOR);
        const utilizador = await response.json();
        renderTabela(utilizador);

    } catch (err) {
        console.error("Erro ao carregar utilizador:", err);
    }
}


// =============================
// ELIMINAR UTILIZADOR
// =============================
// Remove um utilizador com base no ID
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
// RENDERIZAR TABELA
// =============================
// Apresenta a lista de utilizadores na tabela HTML
function renderTabela(lista) {
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
// MENSAGENS AO UTILIZADOR
// =============================
// Mostra mensagens de sucesso ou erro
function mostrarMensagem(texto, erro = false) {
    const msg = document.getElementById("utilizadorMensagem");
    msg.textContent = texto;
    msg.style.color = erro ? "red" : "green";
    setTimeout(() => msg.textContent = "", 3000);
}


// =============================
// PLACEHOLDER - EDITAR
// =============================
// Funcionalidade de edição futura
function editarUtilizador(id) {
    alert("TODO: Implementar edição de utilizador.");
}