using System;
using System.Linq;
using SistemaFinanceiro.Models;

namespace SistemaFinanceiro.Services
{
    public class GerirTransacoes
    {
        private readonly Sistema _sistema;

        public GerirTransacoes(Sistema sistema)
        {
            _sistema = sistema;
        }

        // LISTAR TRANSACOES
        public void ListarTransacoes()
        {
            Console.WriteLine("\n====== LISTA DE TRANSAÇÕES ======");

            if (_sistema.Transacoes.Count == 0)
            {
                Console.WriteLine("Nenhuma transação registada.");
                return;
            }

            foreach (var t in _sistema.Transacoes)
            {
                string categoriaNome = _sistema.Categorias
                    .FirstOrDefault(c => c.Id == t.CategoriaId)?.Nome ?? "Sem categoria";

                Console.WriteLine(
                    $"ID: {t.Id} | {t.Tipo} | {t.Descricao} | {t.Valor}€ | {t.Data:yyyy-MM-dd} | Categoria: {categoriaNome}"
                );
            }

            Console.WriteLine("=================================\n");
        }

        // CRIAR TRANSACAO
        public void CriarTransacao()
        {
            Console.WriteLine("\nCriar nova transação:");

            Console.Write("Descrição: ");
            string descricao = Console.ReadLine();

            Console.Write("Valor (€): ");
            if (!double.TryParse(Console.ReadLine(), out double valor) || valor <= 0)
            {
                Console.WriteLine("Valor inválido!");
                return;
            }

            Console.Write("Data (YYYY-MM-DD) ou Enter para hoje: ");
            string dataStr = Console.ReadLine();
            DateTime data = string.IsNullOrEmpty(dataStr)
                ? DateTime.Today
                : DateTime.Parse(dataStr);

            Console.Write("Tipo (Receita / Despesa): ");
            string tipo = Console.ReadLine();
            if (tipo != "Receita" && tipo != "Despesa")
            {
                Console.WriteLine("Tipo inválido!");
                return;
            }

            Console.WriteLine("\nCategorias disponíveis:");
            foreach (var c in _sistema.Categorias)
                Console.WriteLine($"{c.Id} - {c.Nome}");

            Console.Write("Categoria (ID) ou Enter para nenhuma: ");
            string catStr = Console.ReadLine();
            int? categoriaId = null;

            if (!string.IsNullOrWhiteSpace(catStr) && int.TryParse(catStr, out int catId))
            {
                bool existe = _sistema.Categorias.Any(c => c.Id == catId);
                if (!existe)
                {
                    Console.WriteLine("Categoria inexistente!");
                    return;
                }
                categoriaId = catId;
            }

            var t = _sistema.CriarTransacao(descricao, valor, data, tipo, categoriaId);

            Console.WriteLine($"Transação criada com sucesso! (ID: {t.Id})\n");
        }

        // REMOVER TRANSACAO
        public void RemoverTransacao()
        {
            ListarTransacoes();

            Console.Write("Introduza o ID da transação a remover: ");
            if (!int.TryParse(Console.ReadLine(), out int id))
            {
                Console.WriteLine("ID inválido!");
                return;
            }

            bool removido = _sistema.RemoverTransacao(id);

            if (!removido)
                Console.WriteLine("Transação não encontrada!");
            else
                Console.WriteLine("Transação removida com sucesso!\n");
        }

        // MENU DE TRANSACOES
        public void MenuTransacoes()
        {
            int opcao = -1;

            while (opcao != 0)
            {
                Console.WriteLine("===== GESTÃO DE TRANSAÇÕES =====");
                Console.WriteLine("1 - Listar transações");
                Console.WriteLine("2 - Criar transação");
                Console.WriteLine("3 - Remover transação");
                Console.WriteLine("0 - Voltar");
                Console.Write("Opção: ");

                int.TryParse(Console.ReadLine(), out opcao);
                Console.WriteLine();

                switch (opcao)
                {
                    case 1: ListarTransacoes(); break;
                    case 2: CriarTransacao(); break;
                    case 3: RemoverTransacao(); break;
                    case 0: Console.WriteLine("A voltar ao menu principal...\n"); break;
                    default: Console.WriteLine("Opção inválida!\n"); break;
                }
            }
        }
    }
}