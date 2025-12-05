using System;
using System.Globalization;

public class MenuGerirTransacoes
{
    private readonly GerirTransacoes _transacoesServico;
    private readonly GerirCategorias _categoriasServico;

    public MenuGerirTransacoes(GerirTransacoes transacoesServico, GerirCategorias categoriasServico)
    {
        _transacoesServico = transacoesServico;
        _categoriasServico = categoriasServico;
    }

    public void Abrir()
    {
        int opcao;

        do
        {
            Console.Clear();
            Console.WriteLine("=== GESTÃO DE TRANSAÇÕES ===");
            Console.WriteLine("1. Listar Transações");
            Console.WriteLine("2. Criar Transação");
            Console.WriteLine("3. Editar Transação");
            Console.WriteLine("4. Remover Transação");
            Console.WriteLine("5. Ver Saldo Atual");
            Console.WriteLine("0. Voltar");
            Console.Write("Opção: ");

            if (!int.TryParse(Console.ReadLine(), out opcao))
                opcao = -1;

            switch (opcao)
            {
                case 1: ListarTransacoes(); break;
                case 2: CriarTransacao(); break;
                case 3: EditarTransacao(); break;
                case 4: RemoverTransacao(); break;
                case 5: MostrarSaldo(); break;
                case 0: return;
                default:
                    Console.WriteLine("Opção inválida!");
                    Console.ReadKey();
                    break;
            }

        } while (true);
    }

    private void ListarTransacoes()
    {
        Console.Clear();
        Console.WriteLine("--- Transações ---");

        var transacoes = _transacoesServico.ObterTransacoes();

        if (transacoes.Count == 0)
        {
            Console.WriteLine("Nenhuma transação registada.");
        }
        else
        {
            foreach (var t in transacoes)
            {
                var categoria = _categoriasServico.ObterCategorias().Find(c => c.Id == t.CategoriaId);

                Console.WriteLine(
                    $"{t.Id} - {t.Descricao} | {t.Valor}€ | {t.Data.ToShortDateString()} | {t.Tipo} | Categoria: {categoria?.Nome ?? "N/A"}"
                );
            }
        }

        Console.WriteLine("\nPressione qualquer tecla para continuar...");
        Console.ReadKey();
    }

    private void CriarTransacao()
    {
        Console.Clear();
        Console.WriteLine("--- Criar Transação ---");

        Console.Write("Descrição: ");
        string desc = Console.ReadLine();

        Console.Write("Valor (€): ");
        if (!decimal.TryParse(Console.ReadLine(), out decimal valor))
        {
            Console.WriteLine("Valor inválido!");
            Console.ReadKey();
            return;
        }

        Console.Write("Data (dd/mm/yyyy): ");
        if (!DateTime.TryParseExact(Console.ReadLine(), "dd/MM/yyyy", new CultureInfo("pt-PT"), DateTimeStyles.None, out DateTime data))
        {
            Console.WriteLine("Data inválida!");
            Console.ReadKey();
            return;
        }

        Console.Write("Tipo (1=Receita | 2=Despesa): ");
        int tipoNum = int.Parse(Console.ReadLine());
        TipoTransacao tipo = tipoNum == 1 ? TipoTransacao.Receita : TipoTransacao.Despesa;

        Console.Write("ID da Categoria: ");
        int catId = int.Parse(Console.ReadLine());

        _transacoesServico.CriarTransacao(desc, valor, data, tipo, catId);

        Console.WriteLine("Transação criada com sucesso!");
        Console.ReadKey();
    }

    private void EditarTransacao()
    {
        Console.Clear();
        Console.Write("ID da transação a editar: ");
        if (!int.TryParse(Console.ReadLine(), out int id))
        {
            Console.WriteLine("ID inválido!");
            Console.ReadKey();
            return;
        }

        Console.Write("Nova descrição: ");
        string desc = Console.ReadLine();

        Console.Write("Novo valor (€): ");
        if (!decimal.TryParse(Console.ReadLine(), out decimal valor))
        {
            Console.WriteLine("Valor inválido!");
            Console.ReadKey();
            return;
        }

        Console.Write("Nova data (dd/mm/yyyy): ");
        if (!DateTime.TryParseExact(Console.ReadLine(), "dd/MM/yyyy", new CultureInfo("pt-PT"), DateTimeStyles.None, out DateTime data))
        {
            Console.WriteLine("Data inválida!");
            Console.ReadKey();
            return;
        }

        Console.Write("Novo tipo (1=Receita | 2=Despesa): ");
        int tipoNum = int.Parse(Console.ReadLine());
        TipoTransacao tipo = tipoNum == 1 ? TipoTransacao.Receita : TipoTransacao.Despesa;

        Console.Write("Novo ID da categoria: ");
        int catId = int.Parse(Console.ReadLine());

        bool ok = _transacoesServico.EditarTransacao(id, desc, valor, data, tipo, catId);

        Console.WriteLine(ok ? "Transação alterada!" : "Transação não encontrada.");
        Console.ReadKey();
    }

    private void RemoverTransacao()
    {
        Console.Clear();
        Console.Write("ID da transação a remover: ");

        if (!int.TryParse(Console.ReadLine(), out int id))
        {
            Console.WriteLine("ID inválido!");
            Console.ReadKey();
            return;
        }

        bool ok = _transacoesServico.RemoverTransacao(id);

        Console.WriteLine(ok ? "Transação removida!" : "Transação não encontrada.");
        Console.ReadKey();
    }

    private void MostrarSaldo()
    {
        Console.Clear();
        var saldo = _transacoesServico.ObterSaldoAtual();

        Console.WriteLine($"Saldo Atual: {saldo}€");
        Console.WriteLine("\nPressione qualquer tecla para continuar...");
        Console.ReadKey();
    }
}
