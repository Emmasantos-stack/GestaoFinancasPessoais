// ======================================================
// categoria.js
// Responsável pela gestão de categorias no frontend
// Comunicação com a API REST
// ======================================================

// Verifica se o utilizador está autenticado
const user = Session.getUser();
if (!user) {
    window.location.href = "login.html";
}

// Endpoint base da API de categorias
const API_URL = "/api/categoria";

/*
 Executado automaticamente quando a página é carregada
 */
document.addEventListener("DOMContentLoaded", () => {
    carregarCategoria();

    const form = document.getElementById("formCategoria");
    form.addEventListener("submit", criarCategoria);
});


/*
  Obtém todas as categorias a partir da API
 */
async function carregarCategoria() {
    try {
        const response = await Session.authFetch(API_URL);
        const categorias = await response.json();
        renderTabela(categorias);
    } catch (error) {
        console.error("Erro ao carregar categorias:", error);
    }
}


/*
  Cria uma nova categoria
 */
async function criarCategoria(event) {
    event.preventDefault();

    const nomeInput = document.getElementById("nomeCategoria");
    const nome = nomeInput.value.trim();

    // Validação básica
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

        // Limpa campo e atualiza tabela
        nomeInput.value = "";
        mostrarMensagem("Categoria criada com sucesso!");
        carregarCategoria();

    } catch (error) {
        console.error(error);
        mostrarMensagem("Erro ao comunicar com o servidor.", true);
    }
}


/*
  Remove uma categoria existente
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


/*
 Mostra mensagens ao utilizador
 */
function mostrarMensagem(texto, erro = false) {
    const mensagem = document.getElementById("categoriaMensagem");
    mensagem.textContent = texto;
    mensagem.style.color = erro ? "red" : "green";

    setTimeout(() => mensagem.textContent = "", 3000);
}


/*
 Renderiza a tabela de categorias
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


/* Edita o nome de uma categoria existente */
async function editarCategoria(id) {
    const novoNome = prompt("Novo nome da categoria:");

    if (!novoNome || novoNome.trim() === "")
        return;

    try {
        const response = await Session.authFetch(`/api/categoria/${id}`, {
            method: "PUT",
            headers: { "Content-Type": "application/json" },
            body: JSON.stringify({ nome: novoNome })
        });

        if (!response.ok) {
            mostrarMensagem("Erro ao editar categoria.", true);
            return;
        }

        mostrarMensagem("Categoria alterada com sucesso!");
        carregarCategoria();

    } catch (err) {
        console.error(err);
        mostrarMensagem("Erro de comunicação com o servidor.", true);
    }
}
