// ======================================================
// Categoria.JS
// Responsável pela gestão de Categoria através da API
// ======================================================
const user = Session.getUser();
if (!user) {
    window.location.href = "login.html";
}

// Endereço base da API para Categoria
const API_URL = "/api/Categoria";

/**
 * Executado automaticamente quando a página é carregada.
 */
document.addEventListener("DOMContentLoaded", () => {
    carregarCategoria();

    const form = document.getElementById("formCategoria");
    form.addEventListener("submit", criarCategoria);
});


/**
 * Obtém a lista de Categoria a partir do servidor
 */
async function carregarCategoria() {
    try {
        const response = await Session.authFetch(API_URL);
        const categorias = await response.json();
        renderTabela(categorias);
    } catch (error) {
        console.error("Erro ao carregar Categoria:", error);
    }
}


/**
 * Cria uma nova categoria
 */
async function criarCategoria(event) {
    event.preventDefault();

    const nomeInput = document.getElementById("nomeCategoria");
    const nome = nomeInput.value.trim();

    if (nome === "") {
        mostrarMensagem("O nome não pode estar vazio.", true);
        return;
    }

    try {
        const response = await Session.authFetch(API_URL, {
            method: "POST",
            headers: { "Content-Type": "application/json" },
            body: JSON.stringify({ nome })
        });

        if (!response.ok) {
            mostrarMensagem("Erro ao criar categoria.", true);
            return;
        }

        nomeInput.value = "";
        mostrarMensagem("Categoria criada com sucesso!");
        carregarCategoria();

    } catch (error) {
        console.error(error);
        mostrarMensagem("Erro ao comunicar com o servidor.", true);
    }
}


/**
 * Remove uma categoria
 */
async function eliminarCategoria(id) {
    if (!confirm("Tem a certeza que deseja remover esta categoria?"))
        return;

    try {
        const response = await Session.authFetch(`${API_URL}/${id}`, {
            method: "DELETE"
        });

        if (!response.ok) {
            mostrarMensagem("Erro ao eliminar categoria.", true);
            return;
        }

        mostrarMensagem("Categoria removida com sucesso.");
        carregarCategoria();

    } catch (error) {
        console.error(error);
    }
}


/**
 * Mensagens ao utilizador
 */
function mostrarMensagem(texto, erro = false) {
    const mensagem = document.getElementById("categoriaMensagem");
    mensagem.textContent = texto;
    mensagem.style.color = erro ? "red" : "green";

    setTimeout(() => mensagem.textContent = "", 3000);
}


/**
 * Renderiza a tabela de categorias
 */
function renderTabela(lista) {
    const tabela = document.getElementById("tabelaCategoria");
    tabela.innerHTML = "";

    lista.forEach(cat => {
        const linha = document.createElement("tr");
        linha.classList.add("table__row");

        linha.innerHTML = `
            <td class="table__cell">${cat.id}</td>
            <td class="table__cell">${cat.nome}</td>
            <td class="table__cell table__cell--acoes">
                <button class="button button--small"
                        onclick="editarCategoria(${cat.id})">
                    Editar
                </button>
                <button class="button button--danger"
                        onclick="eliminarCategoria(${cat.id})">
                    Eliminar
                </button>
            </td>
        `;

        tabela.appendChild(linha);
    });
}


/**
 * Placeholder edição
 */
function editarCategoria(id) {
    alert("Funcionalidade de edição ainda não implementada.");
}
