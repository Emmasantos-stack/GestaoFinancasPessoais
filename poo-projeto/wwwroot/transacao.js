// ======================================================
// TRANSACAO.JS
// Gestão de transações no frontend via API REST
// ======================================================
const user = Session.getUser();
if (!user) {
    window.location.href = "login.html";
}

// ENDPOINTS DA API
const API_TRANSACAO = "/api/transacao";
const API_CATEGORIAS = "/api/categoria";

// Executado quando a página carrega
document.addEventListener("DOMContentLoaded", () => {

    carregarCategorias();
    carregarTransacoes();

    document
        .getElementById("formTransacao")
        .addEventListener("submit", criarTransacao);
});


// ======================================================
// CARREGAR CATEGORIAS
// ======================================================
async function carregarCategorias() {
    try {
        const response = await Session.authFetch(API_CATEGORIAS);
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


// ======================================================
// CRIAR TRANSACAO
// ======================================================
async function criarTransacao(event) {
    event.preventDefault();

    const descricao = document.getElementById("descricao").value.trim();
    const valor = parseFloat(document.getElementById("valor").value);
    const data = document.getElementById("data").value;
    const tipo = document.getElementById("tipo").value;
    const categoriaIdRaw = document.getElementById("categoria").value;
    const categoriaId = categoriaIdRaw ? parseInt(categoriaIdRaw) : null;

    if (!descricao || !valor || !data || !tipo) {
        mostrarMensagem("Preencha todos os campos obrigatórios.", true);
        return;
    }

    const novaTransacao = {
        descricao,
        valor,
        data,
        tipo,
        categoriaId
    };

    try {
        const response = await Session.authFetch(API_TRANSACAO, {
            method: "POST",
            headers: { "Content-Type": "application/json" },
            body: JSON.stringify(novaTransacao)
        });

        if (!response.ok) {
            const erro = await response.text();
            console.error("Erro API:", erro);
            mostrarMensagem(erro || "Erro ao criar transação.", true);
            return;
        }

        mostrarMensagem("Transação criada com sucesso!");
        document.getElementById("formTransacao").reset();
        carregarTransacoes();

    } catch (err) {
        console.error(err);
        mostrarMensagem("Erro ao comunicar com o servidor.", true);
    }
}


// ======================================================
// CARREGAR TRANSACOES
// ======================================================
async function carregarTransacoes() {
    try {
        const response = await Session.authFetch(API_TRANSACAO);
        const transacoes = await response.json();
        renderTabela(transacoes);

    } catch (err) {
        console.error("Erro ao carregar transações:", err);
    }
}


// ======================================================
// ELIMINAR TRANSACAO
// ======================================================
async function eliminarTransacao(id) {
    if (!confirm("Tem a certeza que deseja eliminar?")) return;

    try {
        const response = await Session.authFetch(`${API_TRANSACAO}/${id}`, {
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


// ======================================================
// MENSAGENS AO UTILIZADOR
// ======================================================
function mostrarMensagem(texto, erro = false) {
    const msg = document.getElementById("transacaoMensagem");
    msg.textContent = texto;
    msg.style.color = erro ? "red" : "green";

    setTimeout(() => msg.textContent = "", 3000);
}


// ======================================================
// RENDERIZAR TABELA
// ======================================================
function renderTabela(lista) {
    const tabela = document.getElementById("tabelaTransacao");
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
                <button class="button button--small"
                        onclick="editarTransacao(${tx.id})">
                    Editar
                </button>
                <button class="button button--danger"
                        onclick="eliminarTransacao(${tx.id})">
                    Eliminar
                </button>
            </td>
        `;

        tabela.appendChild(tr);
    });
}

// ======================================================
// EDITAR TRANSAÇÃO
// ======================================================
async function editarTransacao(id) {

    const descricao = prompt("Nova descrição:");
    if (!descricao) return;

    const valor = prompt("Novo valor:");
    if (!valor || isNaN(valor) || Number(valor) <= 0) return;

    const data = prompt("Nova data (YYYY-MM-DD):");
    if (!data) return;

    const tipo = prompt("Tipo (Receita ou Despesa):");
    if (tipo !== "Receita" && tipo !== "Despesa") {
        alert("Tipo inválido.");
        return;
    }

    const categoriaIdTxt = prompt("ID da categoria (ou vazio):");

    const dto = {
        descricao: descricao.trim(),
        valor: Number(valor),
        data,
        tipo,
        categoriaId: categoriaIdTxt ? Number(categoriaIdTxt) : null
    };

    try {
        const response = await Session.authFetch(`/api/transacao/${id}`, {
            method: "PUT",
            headers: { "Content-Type": "application/json" },
            body: JSON.stringify(dto)
        });

        if (!response.ok) {
            const erro = await response.text();
            mostrarMensagem(erro || "Erro ao editar transação.", true);
            return;
        }

        mostrarMensagem("Transação alterada com sucesso!");
        carregarTransacoes();

    } catch (err) {
        console.error(err);
        mostrarMensagem("Erro de comunicação com servidor.", true);
    }
}


