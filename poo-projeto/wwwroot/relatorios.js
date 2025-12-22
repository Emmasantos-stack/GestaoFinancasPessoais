// =============================
// relatorios.js
// Responsável por carregar e apresentar relatórios financeiros
// =============================

// Verifica se existe utilizador autenticado
const user = Session.getUser();
if (!user) {
    window.location.href = "login.html";
}

// Endpoint da API de transações
const API_TRANSACAO = "/api/transacao";

// Carrega o relatório quando a página abre
document.addEventListener("DOMContentLoaded", carregarRelatorio);

// Carrega os dados das transações e gera o relatório
async function carregarRelatorio() {
    try {
        const response = await Session.authFetch(API_TRANSACAO);
        const transacoes = await response.json();

        // Preenche tabela
        renderTabela(transacoes);

        //Calcula totais
        calcularResumo(transacoes);

    } catch (err) {
        console.error("Erro ao carregar relatório:", err);
    }
}

//Calcula receitas, despesas e saldo
function calcularResumo(lista) {
    let receitas = 0;
    let despesas = 0;

    lista.forEach(t => {
         // Normalização para evitar erros de capitalização
        if (t.tipo === "Receita") receitas += t.valor;

        if (t.tipo === "Despesa") despesas += t.valor;
    });

    const saldo = receitas - despesas;

    // Atualiza valores no HTML
    document.getElementById("totalReceitas").textContent = receitas.toFixed(2) + " €";
    document.getElementById("totalDespesas").textContent = despesas.toFixed(2) + " €";
    document.getElementById("saldo").textContent = saldo.toFixed(2) + " €";
}

// Renderiza a tabela de transações
function renderTabela(lista) {
    const tbody = document.getElementById("tabelaRelatorio");
    tbody.innerHTML = "";

    lista.forEach(t => {
        const tr = document.createElement("tr");

        tr.innerHTML = `
            <td>${new Date(t.data).toLocaleDateString()}</td>
            <td>${t.descricao}</td>
            <td>${t.tipo}</td>
            <td>${t.valor.toFixed(2)} €</td>
            <td>${t.categoriaNome ?? "—"}</td>
        `;

        tbody.appendChild(tr);
    });
}
