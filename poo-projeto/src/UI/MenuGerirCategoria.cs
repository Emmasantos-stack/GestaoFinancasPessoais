using System;
using SistemaFinanceiro.Services;

namespace SistemaFinanceiro.UI
{
    /// <summary>
    /// Menu de consola responsável pela gestão de categorias.
    /// Permite listar, criar, editar e remover categorias.
    /// </summary>
    public class MenuGerirCategoria
    {
        private readonly GerirCategoria _gerirCategoria;

        public MenuGerirCategoria(GerirCategoria gerirCategoria)
        {
            _gerirCategoria = gerirCategoria;
        }

        // ---------------- MENU PRINCIPAL ----------------

        public void Abrir()
        {
            int opcao;

            do
            {
                LimparConsola();

                Console.WriteLine("===== GERIR CATEGORIAS =====");
                Console.WriteLine("1 - Listar categorias");
                Console.WriteLine("2 - Criar categoria");
                Console.WriteLine("3 - Editar categoria");
                Console.WriteLine("4 - Remover categoria");
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

        // ---------------- LISTAR ----------------

        private void Listar()
        {
            LimparConsola();

            var categorias = _gerirCategoria.ObterTodas();

            if (categorias.Count == 0)
            {
                Console.WriteLine("Nenhuma categoria registada.");
            }
            else
            {
                Console.WriteLine("Categorias existentes:\n");
                foreach (var c in categorias)
                {
                    Console.WriteLine($"{c.Id} - {c.Nome}");
                }
            }

            Pausa();
        }

        // ---------------- CRIAR ----------------

        private void Criar()
        {
            LimparConsola();

            Console.Write("Nome da categoria: ");
            string? nome = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(nome))
            {
                Console.WriteLine("Nome inválido.");
                Pausa();
                return;
            }

            try
            {
                _gerirCategoria.Criar(nome);
                Console.WriteLine("Categoria criada com sucesso!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao criar categoria: {ex.Message}");
            }

            Pausa();
        }

        // ---------------- EDITAR ----------------

        private void Editar()
        {
            LimparConsola();

            Console.Write("ID da categoria: ");
            if (!int.TryParse(Console.ReadLine(), out int id))
            {
                Console.WriteLine("ID inválido.");
                Pausa();
                return;
            }

            Console.Write("Novo nome: ");
            string? nome = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(nome))
            {
                Console.WriteLine("Nome inválido.");
            }
            else
            {
                bool ok = _gerirCategoria.Editar(id, nome);
                Console.WriteLine(ok
                    ? "Categoria alterada com sucesso!"
                    : "Categoria não encontrada!");
            }

            Pausa();
        }

        // ---------------- REMOVER ----------------

        private void Remover()
        {
            LimparConsola();

            Console.Write("ID da categoria: ");
            if (!int.TryParse(Console.ReadLine(), out int id))
            {
                Console.WriteLine("ID inválido.");
                Pausa();
                return;
            }

            bool ok = _gerirCategoria.Remover(id);
            Console.WriteLine(ok
                ? "Categoria removida com sucesso!"
                : "Categoria não encontrada!");

            Pausa();
        }

        // ---------------- UTILITÁRIOS ----------------

        private void LimparConsola()
        {
            try { Console.Clear(); } catch { }
        }

        private void Pausa()
        {
            Console.WriteLine("\nPressione qualquer tecla para continuar...");
            Console.ReadKey();
        }
    }
}
