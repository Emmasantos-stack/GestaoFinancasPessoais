using System;
using System.Globalization;
using SistemaFinanceiro.Services;
using SistemaFinanceiro.Models;

namespace SistemaFinanceiro.UI
{
    // Classe responsável pelo menu de gestão de transações
    // Aqui o utilizador pode listar, criar e remover transações
    public class MenuGerirTransacoes
    {
        // Serviço que contém a lógica de negócio das transações
        private readonly GerirTransacoes _gerirTransacoes;

        // Serviço responsável pela gestão das categorias
        private readonly GerirCategorias _gerirCategorias;


        // Construtor do menu
        // Recebe os serviços por injeção de dependências
        public MenuGerirTransacoes(
            GerirTransacoes gerirTransacoes,
            GerirCategorias gerirCategorias)
        {
            // Guarda o serviço de transações
            _gerirTransacoes = gerirTransacoes;

            // Guarda o serviço de categorias
            _gerirCategorias = gerirCategorias;
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
            var transacoes = _gerirTransacoes.ObterTransacoes();

            // Obtém todas as categorias existentes
            var categorias = _gerirCategorias.ObterCategorias();

            // Mostra o cabeçalho da lista
            foreach (var t in transacoes)
            {
                // Procura o nome da categoria associada à transação
                // Se não existir, mostra "Sem categoria"
                var cat = categorias.Find(c => c.Id == t.CategoriaId)?.Nome ?? "Sem categoria";

                // Mostra os dados da transação no ecrã
                Console.WriteLine($"{t.Id} | {t.Data:dd/MM/yyyy} | {t.Tipo} | {t.Descricao} | {t.Valor}€ | {cat}");
            }

            // Aguarda uma tecla para voltar ao menu
            Console.ReadKey();
        }

        // Método responsável por criar uma nova transação
        private void Criar()
        {
            // Limpa o ecrã
            Console.Clear();

            // Pede a descrição da transação
            Console.Write("Descrição: ");
            string desc = Console.ReadLine();

            // Pede o valor da transação
            Console.Write("Valor: ");
            double valor = double.Parse(Console.ReadLine());

            // Pede a data da transação no formato indicado
            Console.Write("Data (dd/MM/yyyy): ");
            DateTime data = DateTime.ParseExact(Console.ReadLine(), "dd/MM/yyyy", CultureInfo.InvariantCulture);

            // Pede o tipo da transação (Receita ou Despesa)
            Console.Write("Tipo (Receita/Despesa): ");
            TipoTransacao tipo = Enum.Parse<TipoTransacao>(Console.ReadLine(), true);

            // Pede o ID da categoria (opcional)
            Console.Write("ID Categoria (ou vazio): ");
            string catTxt = Console.ReadLine();

            // Se o campo estiver vazio, a categoria fica como null
            int? catId = string.IsNullOrWhiteSpace(catTxt) ? null : int.Parse(catTxt);

            // Cria a transação através do serviço
            _gerirTransacoes.CriarTransacao(desc, valor, data, tipo, catId);

            //Mensagem de sucesso
            Console.WriteLine("Transação criada!");

            // Aguarda uma tecla antes de voltar ao menu
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
            bool ok = _gerirTransacoes.RemoverTransacao(id);

            //Mostra resuoltado da operação
            Console.WriteLine(ok ? "Removida!" : "Não encontrada!");

            // Aguarda uma tecla antes de voltar ao menu
            Console.ReadKey();
        }
    }
}