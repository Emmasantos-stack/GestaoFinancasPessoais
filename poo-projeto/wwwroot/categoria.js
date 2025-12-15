// ======================================================
// CATEGORIAS.JS
// Responsável pela gestão de categorias através da API
// ======================================================

// Endereço base da API para categorias
// (Pode ser alterado quando o backend estiver completo)
const API_URL = "/api/categorias";

/**
 * Executado automaticamente quando a página é carregada.
 * - Carrega as categorias existentes
 * - Associa o evento submit ao formulário
 */
document.addEventListener("DOMContentLoaded", () => {
    carregarCategorias();

    const form = document.getElementById("formCategoria");
    form.addEventListener("submit", criarCategoria);
});


/**
 * Obtém a lista de categorias a partir do servidor
 * e envia os dados para serem apresentados na tabela.
 */
async function carregarCategorias() {
    try {
        const response = await fetch(API_URL);
        const categorias = await response.json();

        renderTabela(categorias);
    } catch (error) {
        console.error("Erro ao carregar categorias:", error);
    }
}


/**
 * Cria uma nova categoria com base nos dados introduzidos
 * no formulário e envia para o backend.
 */
async function criarCategoria(event) {
    event.preventDefault(); // impede o reload da página

    const nomeInput = document.getElementById("nomeCategoria");
    const nome = nomeInput.value.trim();

    // Validação simples
    if (nome === "") {
        mostrarMensagem("O nome não pode estar vazio.", true);
        return;
    }

    try {
        const response = await fetch(API_URL, {
            method: "POST",
            headers: { "Content-Type": "application/json" },
            body: JSON.stringify({ nome })
        });

        if (!response.ok) {
            mostrarMensagem("Erro ao criar categoria.", true);
            return;
        }

        // Limpa o campo após sucesso
        nomeInput.value = "";
        mostrarMensagem("Categoria criada com sucesso!");

        // Atualiza a tabela
        carregarCategorias();
    } catch (error) {
        console.error(error);
        mostrarMensagem("Erro ao comunicar com o servidor.", true);
    }
}


/**
 * Remove uma categoria com base no seu identificador (ID).
 */
async function eliminarCategoria(id) {
    const confirmar = confirm(
        "Tem a certeza que deseja remover esta categoria?"
    );
    if (!confirmar) return;

    try {
        const response = await fetch(`${API_URL}/${id}`, {
            method: "DELETE"
        });

        if (!response.ok) {
            mostrarMensagem("Erro ao eliminar categoria.", true);
            return;
        }

        mostrarMensagem("Categoria removida com sucesso.");
        carregarCategorias();
    } catch (error) {
        console.error(error);
    }
}


/**
 * Mostra mensagens de sucesso ou erro ao utilizador.
 */
function mostrarMensagem(texto, erro = false) {
    const mensagem = document.getElementById("categoriaMensagem");
    mensagem.textContent = texto;
    mensagem.style.color = erro ? "red" : "green";

    // Remove a mensagem após alguns segundos
    setTimeout(() => {
        mensagem.textContent = "";
    }, 3000);
}


/**
 * Constrói dinamicamente a tabela HTML com as categorias.
 */
function renderTabela(lista) {
    const tabela = document.getElementById("tabelaCategorias");
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
 * Funcionalidade de edição ainda não implementada.
 * Fica como trabalho futuro.
 */
function editarCategoria(id) {
    alert("Funcionalidade de edição ainda não implementada. (TODO)");
}
