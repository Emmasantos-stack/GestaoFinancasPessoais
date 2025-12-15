
// TRANSACOES.JS
// Ficheiro responsável por gerir as transações no frontend
// Comunica com a API REST para criar, listar e eliminar transações


// ENDPOINTS DA API
// Estes caminhos representam os endpoints do backend
// (podem ser alterados quando o backend estiver finalizado)
const API_TRANSACOES = "/api/transacoes";
const API_CATEGORIAS = "/api/categorias";

// Este evento é executado quando a página HTML termina de carregar
document.addEventListener("DOMContentLoaded", () => {

    // Carrega as categorias para o select do formulário
    carregarCategorias();

    // Carrega todas as transações e mostra na tabela
    carregarTransacoes();

    // Associa o envio do formulário à função criarTransacao
    document.getElementById("formTransacao")
        .addEventListener("submit", criarTransacao);
});

// CARREGAR CATEGORIAS NO SELECT
async function carregarCategorias() {
    try {
        // Faz um pedido GET à API para obter as categorias
        const response = await fetch(API_CATEGORIAS);

        // Converte a resposta em JSON
        const categorias = await response.json();

        // Obtém o elemento <select> das categorias
        const select = document.getElementById("categoria");

        // Limpa as opções existentes
        select.innerHTML = "";

        // Para cada categoria recebida da API
        categorias.forEach(cat => {

            // Cria um novo elemento <option>
            const option = document.createElement("option");

            // Define o valor do option como o ID da categoria
            option.value = cat.id;

            // Define o texto visível como o nome da categoria
            option.textContent = cat.nome;

            // Adiciona a opção ao select
            select.appendChild(option);
        });

    } catch (err) {
        // Mostra erro no console caso a API falhe
        console.error("Erro ao carregar categorias:", err);
    }
}

// CRIAR NOVA TRANSACAO
async function criarTransacao(event) {

    // Impede o comportamento padrão do formulário (reload da página)
    event.preventDefault();

    // Obtém e trata os valores introduzidos pelo utilizador
    const descricao = document.getElementById("descricao").value.trim();
    const valor = parseFloat(document.getElementById("valor").value);
    const data = document.getElementById("data").value;
    const categoriaId = document.getElementById("categoria").value;

    // Validação simples dos campos obrigatórios
    if (!descricao || !valor || !data) {
        mostrarMensagem("Preencha todos os campos.", true);
        return;
    }

    // Cria o objeto da nova transação a enviar para a API
    const novaTransacao = {
        descricao,
        valor,
        data,
        categoriaId
    };

    // Envia a nova transação para a API usando POST
    try {
        const response = await fetch(API_TRANSACOES, {
            method: "POST",
            headers: { "Content-Type": "application/json" },
            body: JSON.stringify(novaTransacao)
        });

        // Verifica se o servidor respondeu com erro
        if (!response.ok) {
            mostrarMensagem("Erro ao criar transação.", true);
            return;
        }

        // Mostra mensagem de sucesso
        mostrarMensagem("Transação criada com sucesso!");

        // Limpa o formulário
        document.getElementById("formTransacao").reset();

        // Atualiza a lista de transações
        carregarTransacoes();

    } catch (err) {
        // Mostra erro caso não seja possível comunicar com o servidor
        console.error(err);
        mostrarMensagem("Erro ao comunicar com servidor.", true);
    }
}


// CARREGAR TODAS AS TRANSACOES
async function carregarTransacoes() {
    try {
         // Faz um pedido GET para obter todas as transações
        const response = await fetch(API_TRANSACOES);

        // Converte a resposta para JSON
        const transacoes = await response.json();

        // Envia a lista para ser renderizada na tabela
        renderTabela(transacoes);

    } catch (err) {
        // Mostra erro no console
        console.error("Erro ao carregar transações:", err);
    }
}

// ELIMINAR TRANSACAO
async function eliminarTransacao(id) {
    // Confirmação antes de eliminar
    if (!confirm("Tem a certeza que deseja eliminar?")) return;

    try {
        // Envia pedido DELETE para a API com o ID da transação
        const response = await fetch(`${API_TRANSACOES}/${id}`, {
            method: "DELETE"
        });

        // Verifica se ocorreu erro
        if (!response.ok) {
            mostrarMensagem("Erro ao eliminar transação.", true);
            return;
        }

        // Mostra mensagem de sucesso
        mostrarMensagem("Transação removida.");

        // Atualiza a lista de transações
        carregarTransacoes();

    } catch (err) {
        // Mostra erro no console
        console.error(err);
    }
}


// MENSAGEM AO UTILIZADOR
function mostrarMensagem(texto, erro = false) {

    // Obtém o elemento onde a mensagem será mostrada
    const msg = document.getElementById("transacaoMensagem");

    // Define o texto da mensagem
    msg.textContent = texto;

    // Define a cor conforme seja erro ou sucesso
    msg.style.color = erro ? "red" : "green";

    // Remove a mensagem após 3 segundos
    setTimeout(() => msg.textContent = "", 3000);
}


// RENDERIZAR TABELA
function renderTabela(lista) {
    // Obtém o corpo da tabela
    const tabela = document.getElementById("tabelaTransacoes");

    // Limpa o conteúdo atual da tabela
    tabela.innerHTML = "";

    lista.forEach(tx => {
        const tr = document.createElement("tr");
        tr.classList.add("table__row");

        // Preenche a linha com os dados da transação
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
        // Adiciona a linha à tabela
        tabela.appendChild(tr);
    });
}


// PLACEHOLDER - EDITAR
function editarTransacao(id) {
    // Função ainda não implementada
    // Serve apenas como placeholder para futuras melhorias
    alert("TODO: Implementar edição de transações.");
}