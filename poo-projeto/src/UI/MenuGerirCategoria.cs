using System;
using SistemaFinanceiro.Services;

namespace SistemaFinanceiro.UI
{
    /// <summary>
    /// Menu de consola responsável pela gestão de Categoria.
    /// Permite listar, criar, editar e remover Categoria.
    /// </summary>
    public class MenuGerirCategoria
    {
        private readonly GerirCategoria _gerirCategoria;

        public MenuGerirCategoria(GerirCategoria gerirCategoria)
        {
            _gerirCategoria = gerirCategoria;
        }

        /// <summary>
        /// Apresenta o menu principal de Categoria.
        /// </summary>
        public void Abrir()
        {
            int opcao;

            do
            {
                LimparConsola();

                Console.WriteLine("===== GERIR Categoria =====");
                Console.WriteLine("1 - Listar Categoria");
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

        // ----------------- OPÇÕES DO MENU -----------------

        private void Listar()
        {
            LimparConsola();

            var Categoria = _gerirCategoria.ObterCategoria();

            if (Categoria.Count == 0)
            {
                Console.WriteLine("Nenhuma categoria registada.");
            }
            else
            {
                Console.WriteLine("Categoria existentes:");
                foreach (var c in Categoria)
                {
                    Console.WriteLine($"{c.Id} - {c.Nome}");
                }
            }

            Console.WriteLine("\nPressione qualquer tecla para continuar...");
            Console.ReadKey();
        }

        private void Criar()
        {
            LimparConsola();

            Console.Write("Nome da categoria: ");
            string? nome = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(nome))
            {
                Console.WriteLine("Nome inválido.");
            }
            else
            {
                _gerirCategoria.CriarCategoria(nome);
                Console.WriteLine("Categoria criada com sucesso!");
            }

            Console.ReadKey();
        }

        private void Editar()
        {
            LimparConsola();

            Console.Write("ID da categoria: ");
            if (!int.TryParse(Console.ReadLine(), out int id))
                return;

            Console.Write("Novo nome: ");
            string? nome = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(nome))
            {
                Console.WriteLine("Nome inválido.");
            }
            else
            {
                bool ok = _gerirCategoria.EditarCategoria(id, nome);
                Console.WriteLine(ok ? "Categoria alterada!" : "Categoria não encontrada!");
            }

            Console.ReadKey();
        }

        private void Remover()
        {
            LimparConsola();

            Console.Write("ID da categoria: ");
            if (!int.TryParse(Console.ReadLine(), out int id))
                return;

            bool ok = _gerirCategoria.RemoverCategoria(id);
            Console.WriteLine(ok ? "Categoria removida!" : "Categoria não encontrada!");

            Console.ReadKey();
        }

        // ----------------- UTILITÁRIOS -----------------

        /// <summary>
        /// Limpa a consola de forma segura.
        /// Evita exceções quando a aplicação não tem consola associada.
        /// </summary>
        private void LimparConsola()
        {
            try
            {
                Console.Clear();
            }
            catch
            {
                // Ignorar erro de consola
            }
        }
    }
}
