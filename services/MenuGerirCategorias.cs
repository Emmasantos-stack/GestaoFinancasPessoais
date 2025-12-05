using System;

public class MenuGerirCategorias
{
    private readonly CategoriaServico _categoriaServico;

    public MenuGerirCategorias(CategoriaServico categoriaServico)
    {
        _categoriaServico = categoriaServico;
    }

    public void Abrir()
    {
        int opcao;

        do
        {
            Console.Clear();
            Console.WriteLine("=== GESTÃO DE CATEGORIAS ===");
            Console.WriteLine("1. Listar Categorias");
            Console.WriteLine("2. Criar Categoria");
            Console.WriteLine("3. Editar Categoria");
            Console.WriteLine("4. Remover Categoria");
            Console.WriteLine("0. Voltar");
            Console.Write("Opção: ");

            if (!int.TryParse(Console.ReadLine(), out opcao))
                opcao = -1;

            switch (opcao)
            {
                case 1:
                    ListarCategorias();
                    break;
                case 2:
                    CriarCategoria();
                    break;
                case 3:
                    EditarCategoria();
                    break;
                case 4:
                    RemoverCategoria();
                    break;
                case 0:
                    return;
                default:
                    Console.WriteLine("Opção inválida!");
                    Console.ReadKey();
                    break;
            }

        } while (opcao != 0);
    }

    private void ListarCategorias()
    {
        Console.Clear();
        Console.WriteLine("--- Categorias ---");

        var categorias = _categoriaServico.ObterCategorias();

        if (categorias.Count == 0)
        {
            Console.WriteLine("Nenhuma categoria registada.");
        }
        else
        {
            foreach (var c in categorias)
                Console.WriteLine($"{c.Id} - {c.Nome}");
        }

        Console.WriteLine("\nPressione qualquer tecla para continuar...");
        Console.ReadKey();
    }

    private void CriarCategoria()
    {
        Console.Clear();
        Console.Write("Nome da nova categoria: ");
        string nome = Console.ReadLine();

        if (string.IsNullOrWhiteSpace(nome))
        {
            Console.WriteLine("Nome inválido!");
        }
        else
        {
            _categoriaServico.CriarCategoria(nome);
            Console.WriteLine("Categoria criada com sucesso!");
        }

        Console.ReadKey();
    }

    private void EditarCategoria()
    {
        Console.Clear();
        Console.Write("ID da categoria a editar: ");

        if (!int.TryParse(Console.ReadLine(), out int id))
        {
            Console.WriteLine("ID inválido!");
            Console.ReadKey();
            return;
        }

        Console.Write("Novo nome: ");
        string novoNome = Console.ReadLine();

        bool ok = _categoriaServico.EditarCategoria(id, novoNome);

        Console.WriteLine(ok ? "Categoria alterada!" : "Categoria não encontrada.");
        Console.ReadKey();
    }

    private void RemoverCategoria()
    {
        Console.Clear();
        Console.Write("ID da categoria a remover: ");

        if (!int.TryParse(Console.ReadLine(), out int id))
        {
            Console.WriteLine("ID inválido!");
            Console.ReadKey();
            return;
        }

        bool ok = _categoriaServico.RemoverCategoria(id);

        Console.WriteLine(ok ? "Categoria removida!" : "Categoria não encontrada.");
        Console.ReadKey();
    }
}
