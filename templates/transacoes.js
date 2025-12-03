// =============================
// TRANSACOES.JS
// Gestão de transações via API REST
// =============================

// ENDPOINTS (ajustar quando tiver backend real)
const API_TRANSACOES = "/api/transacoes";
const API_CATEGORIAS = "/api/categorias";

document.addEventListener("DOMContentLoaded", () => {
    carregarCategorias();
    carregarTransacoes();

    document.getElementById("formTransacao")
        .addEventListener("submit", criarTransacao);
});

// =============================
// CARREGAR CATEGORIAS NO SELECT
// =============================
async function carregarCategorias() {
    try {
        const response = await fetch(API_CATEGORIAS);
        const categorias = await response.json();

        const select = document.getElementById("categoria");
        select.innerHTML = "";

        categorias.forEach(cat => {
            const option = document.createElement("option");
            option.value = cat.id;
            option.textContent = cat.nome;
            select.appendChild(option);
        });

    } catch (err) {
        console.error("Erro ao carregar categorias:", err);
    }
}


// =============================
// CRIAR NOVA TRANSACAO
// =============================
async function criarTransacao(event) {
    event.preventDefault();

    const descricao = document.getElementById("descricao").value.trim();
    const valor = parseFloat(document.getElementById("valor").value);
    const data = document.getElementById("data").value;
    const categoriaId = document.getElementById("categoria").value;

    if (!descricao || !valor || !data) {
        mostrarMensagem("Preencha todos os campos.", true);
        return;
    }

    const novaTransacao = {
        descricao,
        valor,
        data,
        categoriaId
    };

    try {
        const response = await fetch(API_TRANSACOES, {
            method: "POST",
            headers: { "Content-Type": "application/json" },
            body: JSON.stringify(novaTransacao)
        });

        if (!response.ok) {
            mostrarMensagem("Erro ao criar transação.", true);
            return;
        }

        mostrarMensagem("Transação criada com sucesso!");

        document.getElementById("formTransacao").reset();
        carregarTransacoes();

    } catch (err) {
        console.error(err);
        mostrarMensagem("Erro ao comunicar com servidor.", true);
    }
}


// =============================
// CARREGAR TODAS AS TRANSACOES
// =============================
async function carregarTransacoes() {
    try {
        const response = await fetch(API_TRANSACOES);
        const transacoes = await response.json();

        renderTabela(transacoes);

    } catch (err) {
        console.error("Erro ao carregar transações:", err);
    }
}


// =============================
// ELIMINAR TRANSACAO
// =============================
async function eliminarTransacao(id) {
    if (!confirm("Tem a certeza que deseja eliminar?")) return;

    try {
        const response = await fetch(`${API_TRANSACOES}/${id}`, {
            method: "DELETE"
        });

        if (!response.ok) {
            mostrarMensagem("Erro ao eliminar transação.", true);
            return;
        }

        mostrarMensagem("Transação removida.");
        carregarTransacoes();

    } catch (err) {
        console.error(err);
    }
}


// =============================
// MENSAGEM AO UTILIZADOR
// =============================
function mostrarMensagem(texto, erro = false) {
    const msg = document.getElementById("transacaoMensagem");
    msg.textContent = texto;
    msg.style.color = erro ? "red" : "green";
    setTimeout(() => msg.textContent = "", 3000);
}


// =============================
// RENDERIZAR TABELA
// =============================
function renderTabela(lista) {
    const tabela = document.getElementById("tabelaTransacoes");
    tabela.innerHTML = "";

    lista.forEach(tx => {
        const tr = document.createElement("tr");
        tr.classList.add("table__row");

        tr.innerHTML = `
            <td class="table__cell">${tx.id}</td>
            <td class="table__cell">${tx.descricao}</td>
            <td class="table__cell">${tx.valor.toFixed(2)} €</td>
            <td class="table__cell">${tx.data}</td>
            <td class="table__cell">${tx.categoriaNome || "—"}</td>

            <td class="table__cell table__cell--acoes">
                <button class="button button--small" onclick="editarTransacao(${tx.id})">Editar</button>
                <button class="button button--danger" onclick="eliminarTransacao(${tx.id})">Eliminar</button>
            </td>
        `;

        tabela.appendChild(tr);
    });
}


// =============================
// PLACEHOLDER - EDITAR
// =============================
function editarTransacao(id) {
    alert("TODO: Implementar edição de transações.");
}
