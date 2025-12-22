using System;
using System.Globalization;
using SistemaFinanceiro.Services;
using SistemaFinanceiro.Models;

namespace SistemaFinanceiro.UI
{
    // Classe responsável pelo menu de gestão de transações
    public class MenuGerirTransacao
    {
        private readonly GerirTransacao _gerirTransacao;
        private readonly GerirCategoria _gerirCategoria;

        public MenuGerirTransacao(
            GerirTransacao gerirTransacao,
            GerirCategoria gerirCategoria)
        {
            _gerirTransacao = gerirTransacao;
            _gerirCategoria = gerirCategoria;
        }

        public void Abrir()
        {
            int opcao;

            do
            {
                Console.Clear();
                Console.WriteLine("===== GERIR TRANSAÇÕES =====");
                Console.WriteLine("1 - Listar Transações");
                Console.WriteLine("2 - Criar Transação");
                Console.WriteLine("3 - Remover Transação");
                Console.WriteLine("0 - Voltar");
                Console.Write("Opção: ");

                int.TryParse(Console.ReadLine(), out opcao);

                switch (opcao)
                {
                    case 1: Listar(); break;
                    case 2: Criar(); break;
                    case 3: Remover(); break;
                }

            } while (opcao != 0);
        }

        // ================= LISTAR =================
        private void Listar()
        {
            Console.Clear();

            var transacoes = _gerirTransacao.ObterTransacao();
            var categorias = _gerirCategoria.ObterTodas();

            if (transacoes.Count == 0)
            {
                Console.WriteLine("Sem transações registadas.");
            }
            else
            {
                foreach (var t in transacoes)
                {
                    var catNome = categorias
                        .Find(c => c.Id == t.CategoriaId)?.Nome
                        ?? "Sem categoria";

                    Console.WriteLine(
                        $"{t.Id} | {t.Data:dd/MM/yyyy} | {t.Tipo} | {t.Descricao} | {t.Valor}€ | {catNome}"
                    );
                }
            }

            Console.ReadKey();
        }

        // ================= CRIAR =================
        private void Criar()
        {
            Console.Clear();

            Console.Write("Descrição: ");
            string? desc = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(desc))
            {
                Console.WriteLine("Descrição inválida.");
                Console.ReadKey();
                return;
            }

            double valor;
            Console.Write("Valor: ");
            while (!double.TryParse(Console.ReadLine(), out valor) || valor <= 0)
            {
                Console.Write("Valor inválido. Introduza um número maior que 0: ");
            }

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

            TipoTransacao tipo;
            Console.Write("Tipo (Receita/Despesa): ");
            while (!Enum.TryParse(Console.ReadLine(), true, out tipo))
            {
                Console.Write("Tipo inválido. Escreva Receita ou Despesa: ");
            }

            Console.Write("ID Categoria (ou vazio): ");
            string? catTxt = Console.ReadLine();

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

            try
            {
                _gerirTransacao.CriarTransacao(desc, valor, data, tipo, catId);
                Console.WriteLine("Transação criada com sucesso!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao criar transação: {ex.Message}");
            }

            Console.ReadKey();
        }

        // ================= REMOVER =================
        private void Remover()
        {
            Console.Clear();
            Console.Write("ID da transação: ");

            if (!int.TryParse(Console.ReadLine(), out int id))
            {
                Console.WriteLine("ID inválido.");
                Console.ReadKey();
                return;
            }

            bool ok = _gerirTransacao.RemoverTransacao(id);
            Console.WriteLine(ok ? "Removida!" : "Não encontrada!");

            Console.ReadKey();
        }
    }
}
