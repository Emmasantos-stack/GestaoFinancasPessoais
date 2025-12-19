const user = Session.getUser();
if (!user) {
    window.location.href = "login.html";
}

const API_TRANSACAO = "/api/transacao";

document.addEventListener("DOMContentLoaded", carregarRelatorio);

async function carregarRelatorio() {
    try {
        const response = await Session.authFetch(API_TRANSACAO);
        const transacoes = await response.json();

        renderTabela(transacoes);
        calcularResumo(transacoes);

    } catch (err) {
        console.error("Erro ao carregar relatório:", err);
    }
}

function calcularResumo(lista) {
    let receitas = 0;
    let despesas = 0;

    lista.forEach(t => {
        if (t.tipo === "Receita") receitas += t.valor;
        if (t.tipo === "Despesa") despesas += t.valor;
    });

    const saldo = receitas - despesas;

    document.getElementById("totalReceitas").textContent = receitas.toFixed(2) + " €";
    document.getElementById("totalDespesas").textContent = despesas.toFixed(2) + " €";
    document.getElementById("saldo").textContent = saldo.toFixed(2) + " €";
}

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
