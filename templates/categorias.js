// =========================
// CATEGORIAS.JS
// Gestão de categorias via API REST
// =========================

// ENDPOINTS (podes ajustar quando tiveres backend real)
const API_URL = "/api/categorias";

/**
 * Carrega todas as categorias ao iniciar a página.
 */
document.addEventListener("DOMContentLoaded", () => {
    carregarCategorias();

    const form = document.getElementById("formCategoria");
    form.addEventListener("submit", criarCategoria);
});


/**
 * Vai buscar as categorias ao servidor (mock por agora).
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
 * Cria uma nova categoria e envia para o backend.
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
        const response = await fetch(API_URL, {
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

        carregarCategorias(); // recarrega tabela
    } catch (error) {
        console.error(error);
        mostrarMensagem("Erro ao comunicar com o servidor.", true);
    }
}


/**
 * Remove uma categoria pelo ID.
 */
async function eliminarCategoria(id) {
    const confirmar = confirm("Tem a certeza que deseja remover esta categoria?");
    if (!confirmar) return;

    try {
        const response = await fetch(`${API_URL}/${id}`, { method: "DELETE" });

        if (!response.ok) {
            mostrarMensagem("Erro ao eliminar categoria.", true);
            return;
        }

        mostrarMensagem("Categoria removida.");
        carregarCategorias();
    } catch (error) {
        console.error(error);
    }
}


/**
 * Mostra mensagem de feedback ao utilizador.
 */
function mostrarMensagem(texto, erro = false) {
    const mensagem = document.getElementById("categoriaMensagem");
    mensagem.textContent = texto;
    mensagem.style.color = erro ? "red" : "green";

    setTimeout(() => (mensagem.textContent = ""), 3000);
}


/**
 * Renderiza a tabela com todas as categorias.
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
                <button class="button button--small" onclick="editarCategoria(${cat.id})">Editar</button>
                <button class="button button--danger" onclick="eliminarCategoria(${cat.id})">Eliminar</button>
            </td>
        `;

        tabela.appendChild(linha);
    });
}


/**
 * Função placeholder — para ser feita depois.
 */
function editarCategoria(id) {
    alert("Funcionalidade de edição ainda não implementada. (TODO)");
}
