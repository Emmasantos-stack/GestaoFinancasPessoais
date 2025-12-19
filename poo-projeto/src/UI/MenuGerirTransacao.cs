using System;
using System.Globalization;
using SistemaFinanceiro.Services;
using SistemaFinanceiro.Models;

namespace SistemaFinanceiro.UI
{
    // Classe responsável pelo menu de gestão de transações
    // Aqui o utilizador pode listar, criar e remover transações
    public class MenuGerirtransacao
    {
        // Serviço que contém a lógica de negócio das transações
        private readonly GerirTransacao _gerirtransacao;

        // Serviço responsável pela gestão das Categoria
        private readonly GerirCategoria _gerirCategoria;


        // Construtor do menu
        // Recebe os serviços por injeção de dependências
        public MenuGerirtransacao(
            GerirTransacao gerirtransacao,
            GerirCategoria gerirCategoria)
        {
            // Guarda o serviço de transações
            _gerirtransacao = gerirtransacao;

            // Guarda o serviço de Categoria
            _gerirCategoria = gerirCategoria;
        }


        // Método que abre o menu de gestão de transações
        public void Abrir()
        {
            int opcao;

            // Ciclo que mantém o menu ativo até o utilizador escolher sair
            do
            {
                // Limpa o ecrã para melhor leitura 
                Console.Clear();

                // Mostra as opções disponíveis
                Console.WriteLine("===== GERIR TRANSAÇÕES =====");
                Console.WriteLine("1 - Listar Transações");
                Console.WriteLine("2 - Criar Transação");
                Console.WriteLine("3 - Remover Transação");
                Console.WriteLine("0 - Voltar");
                Console.Write("Opção: ");

                // Lê a opção escolhida pelo utilizador
                int.TryParse(Console.ReadLine(), out opcao);

                // Executa a ação correspondente à opção escolhida
                switch (opcao)
                {
                    case 1: Listar(); break;
                    case 2: Criar(); break;
                    case 3: Remover(); break;
                }

            } while (opcao != 0); // Sai do menu quando a opção for 0
        }

        // Método responsável por listar todas as transações
        private void Listar()
        {
            // Limpa o ecrã
            Console.Clear();

            // Obtém todas as transações do sistema
            var transacao = _gerirtransacao.ObterTransacao();

            // Obtém todas as Categoria existentes
            var Categoria = _gerirCategoria.ObterTodas();

            // Mostra o cabeçalho da lista
            foreach (var t in transacao)
            {
                // Procura o nome da categoria associada à transação
                // Se não existir, mostra "Sem categoria"
                var cat = Categoria.Find(c => c.Id == t.CategoriaId)?.Nome ?? "Sem categoria";

                // Mostra os dados da transação no ecrã
                Console.WriteLine($"{t.Id} | {t.Data:dd/MM/yyyy} | {t.Tipo} | {t.Descricao} | {t.Valor}€ | {cat}");
            }

            // Aguarda uma tecla para voltar ao menu
            Console.ReadKey();
        }

        // Método responsável por criar uma nova transação
        private void Criar()
{
    Console.Clear();

    // ---------------- DESCRIÇÃO ----------------
    Console.Write("Descrição: ");
    string desc = Console.ReadLine();

    if (string.IsNullOrWhiteSpace(desc))
    {
        Console.WriteLine("Descrição inválida.");
        Console.ReadKey();
        return;
    }

    // ---------------- VALOR ----------------
    double valor;
    Console.Write("Valor: ");
    while (!double.TryParse(Console.ReadLine(), out valor) || valor <= 0)
    {
        Console.Write("Valor inválido. Introduza um número maior que 0: ");
    }

    // ---------------- DATA ----------------
    DateTime data;
    Console.Write("Data (dd/MM/yyyy): ");
    while (!DateTime.TryParseExact(
        Console.ReadLine(),
        "dd/MM/yyyy",
        CultureInfo.InvariantCulture,
        DateTimeStyles.None,
        out data))
    {
        Console.Write("Data inválida. Use o formato dd/MM/yyyy: ");
    }

    // ---------------- TIPO ----------------
    TipoTransacao tipo;
    Console.Write("Tipo (Receita/Despesa): ");
    while (!Enum.TryParse(Console.ReadLine(), true, out tipo))
    {
        Console.Write("Tipo inválido. Escreva Receita ou Despesa: ");
    }

    // ---------------- CATEGORIA ----------------
    Console.Write("ID Categoria (ou vazio): ");
    string catTxt = Console.ReadLine();

    int? catId = null;
    if (!string.IsNullOrWhiteSpace(catTxt))
    {
        if (int.TryParse(catTxt, out int parsedId))
            catId = parsedId;
        else
        {
            Console.WriteLine("ID de categoria inválido.");
            Console.ReadKey();
            return;
        }
    }

    // ---------------- CRIAR TRANSAÇÃO ----------------
    try
    {
        _gerirtransacao.CriarTransacao(desc, valor, data, tipo, catId);
        Console.WriteLine("Transação criada com sucesso!");
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Erro ao criar transação: {ex.Message}");
    }

    Console.ReadKey();
}


        // Método responsável por remover uma transação existente
        private void Remover()
        {
            // Limpa o ecrã
            Console.Clear();

            // Pede o ID da transação a remover
            Console.Write("ID da transação: ");
            int id = int.Parse(Console.ReadLine());

            // Tenta remover a transação 
            bool ok = _gerirtransacao.RemoverTransacao(id);

            //Mostra resuoltado da operação
            Console.WriteLine(ok ? "Removida!" : "Não encontrada!");

            // Aguarda uma tecla antes de voltar ao menu
            Console.ReadKey();
        }
    }
}