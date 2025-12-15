using System;
using SistemaFinanceiro.Services;

namespace SistemaFinanceiro.UI
{
    public class MenuGerirCategorias
    {
        private readonly GerirCategorias _gerirCategorias;

        public MenuGerirCategorias(GerirCategorias gerirCategorias)
        {
            _gerirCategorias = gerirCategorias;
        }

        public void Abrir()
        {
            int opcao;

            do
            {
                Console.Clear();
                Console.WriteLine("===== GERIR CATEGORIAS =====");
                Console.WriteLine("1 - Listar Categorias");
                Console.WriteLine("2 - Criar Categoria");
                Console.WriteLine("3 - Editar Categoria");
                Console.WriteLine("4 - Remover Categoria");
                Console.WriteLine("0 - Voltar");
                Console.Write("Opção: ");

                int.TryParse(Console.ReadLine(), out opcao);

                switch (opcao)
                {
                    case 1: Listar(); break;
                    case 2: Criar(); break;
                    case 3: Editar(); break;
                    case 4: Remover(); break;
                }

            } while (opcao != 0);
        }

        private void Listar()
        {
            Console.Clear();
            var categorias = _gerirCategorias.ObterCategorias();

            if (categorias.Count == 0)
            {
                Console.WriteLine("Nenhuma categoria registada.");
            }
            else
            {
                foreach (var c in categorias)
                {
                    Console.WriteLine($"{c.Id} - {c.Nome}");
                }
            }

            Console.ReadKey();
        }

        private void Criar()
        {
            Console.Clear();
            Console.Write("Nome da categoria: ");
            string nome = Console.ReadLine();

            _gerirCategorias.CriarCategoria(nome);
            Console.WriteLine("Categoria criada com sucesso!");
            Console.ReadKey();
        }

        private void Editar()
        {
            Console.Clear();

            Console.Write("ID da categoria: ");
            if (!int.TryParse(Console.ReadLine(), out int id))
                return;

            Console.Write("Novo nome: ");
            string nome = Console.ReadLine();

            bool ok = _gerirCategorias.EditarCategoria(id, nome);
            Console.WriteLine(ok ? "Categoria alterada!" : "Categoria não encontrada!");
            Console.ReadKey();
        }

        private void Remover()
        {
            Console.Clear();

            Console.Write("ID da categoria: ");
            if (!int.TryParse(Console.ReadLine(), out int id))
                return;

            bool ok = _gerirCategorias.RemoverCategoria(id);
            Console.WriteLine(ok ? "Categoria removida!" : "Categoria não encontrada!");
            Console.ReadKey();
        }
    }
}
