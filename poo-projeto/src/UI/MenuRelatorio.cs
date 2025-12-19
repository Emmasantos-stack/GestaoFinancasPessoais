using System;
using SistemaFinanceiro.Services;

namespace SistemaFinanceiro.UI
{
    public class MenuRelatorio
    {
        private readonly GerarRelatorio _relatorio;

        public MenuRelatorio(GerarRelatorio relatorio)
        {
            _relatorio = relatorio;
        }

        public void Abrir()
        {
            int opcao;

            do
            {
                try { Console.Clear(); } catch { }

                Console.WriteLine("===== RELATÓRIOS =====");
                Console.WriteLine("1 - Total de Receitas");
                Console.WriteLine("2 - Total de Despesas");
                Console.WriteLine("3 - Saldo Atual");
                Console.WriteLine("4 - Transações por Categoria");
                Console.WriteLine("0 - Voltar");
                Console.Write("Opção: ");

                int.TryParse(Console.ReadLine(), out opcao);

                Console.Clear();

                switch (opcao)
                {
                    case 1:
                        Console.WriteLine($"Total de Receitas: {_relatorio.TotalReceitas()}€");
                        break;

                    case 2:
                        Console.WriteLine($"Total de Despesas: {_relatorio.TotalDespesas()}€");
                        break;

                    case 3:
                        Console.WriteLine($"Saldo Atual: {_relatorio.CalcularSaldo()}€");
                        break;

                    case 4:
                        MostrarPorCategoria();
                        break;
                }

                if (opcao != 0)
                {
                    Console.WriteLine("\nPressione qualquer tecla...");
                    Console.ReadKey();
                }

            } while (opcao != 0);
        }

        private void MostrarPorCategoria()
        {
            Console.Write("ID da Categoria: ");
            if (!int.TryParse(Console.ReadLine(), out int id))
                return;

            var lista = _relatorio.TransacoesPorCategoria(id);

            foreach (var t in lista)
            {
                Console.WriteLine(
                    $"{t.Data:dd/MM/yyyy} | {t.Tipo} | {t.Descricao} | {t.Valor}€"
                );
            }
        }
    }
}
