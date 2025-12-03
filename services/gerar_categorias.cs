using System;
using System.Linq;
using SistemaFinanceiro.Models;

namespace SistemaFinanceiro.Services
{
    public class GerirCategorias
    {
        private readonly Sistema _sistema;

        public GerirCategorias(Sistema sistema)
        {
            _sistema = sistema;
        }

   
        public void ListarCategorias()
        {
            Console.WriteLine("\n====== LISTA DE CATEGORIAS ======");

            if (_sistema.Categorias.Count == 0)
            {
                Console.WriteLine("Nenhuma categoria registada.");
                return;
            }

            foreach (var c in _sistema.Categorias)
            {
                Console.WriteLine($"ID: {c.Id} | Nome: {c.Nome}");
            }

            Console.WriteLine("================================\n");
        }

 
        public void CriarCategoria()
        {
            Console.Write("\nIntroduza o nome da nova categoria: ");
            string nome = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(nome))
            {
                Console.WriteLine("Nome inválido!");
                return;
            }

            var nova = _sistema.CriarCategoria(nome);

            Console.WriteLine($"Categoria criada com sucesso! (ID: {nova.Id})\n");
        }

    
        public void EditarCategoria()
        {
            ListarCategorias();

            Console.Write("Introduza o ID da categoria a editar: ");
            if (!int.TryParse(Console.ReadLine(), out int id))
            {
                Console.WriteLine("ID inválido!");
                return;
            }

            var categoria = _sistema.Categorias.FirstOrDefault(c => c.Id == id);
            if (categoria == null)
            {
                Console.WriteLine("Categoria não encontrada!");
                return;
            }

            Console.Write($"Novo nome para a categoria '{categoria.Nome}': ");
            string novoNome = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(novoNome))
            {
                Console.WriteLine("Nome inválido!");
                return;
            }

            categoria.Nome = novoNome;
            _sistema.GravarDados();

            Console.WriteLine("Categoria atualizada com sucesso!\n");
        }

      
        public void RemoverCategoria()
        {
            ListarCategorias();

            Console.Write("Introduza o ID da categoria a remover: ");
            if (!int.TryParse(Console.ReadLine(), out int id))
            {
                Console.WriteLine("ID inválido!");
                return;
            }

            var categoria = _sistema.Categorias.FirstOrDefault(c => c.Id == id);
            if (categoria == null)
            {
                Console.WriteLine("Categoria não encontrada!");
                return;
            }

          
            bool temTransacoes = _sistema.Transacoes.Any(t => t.CategoriaId == id);
            if (temTransacoes)
            {
                Console.WriteLine("Não é possível remover esta categoria — existem transações associadas!");
                return;
            }

            _sistema.Categorias.Remove(categoria);
            _sistema.GravarDados();

            Console.WriteLine("Categoria removida com sucesso!\n");
        }

       
        public void MenuCategorias()
        {
            int opcao = -1;

            while (opcao != 0)
            {
                Console.WriteLine("===== GESTÃO DE CATEGORIAS =====");
                Console.WriteLine("1 - Listar categorias");
                Console.WriteLine("2 - Criar categoria");
                Console.WriteLine("3 - Editar categoria");
                Console.WriteLine("4 - Remover categoria");
                Console.WriteLine("0 - Voltar");
                Console.Write("Opção: ");

                int.TryParse(Console.ReadLine(), out opcao);
                Console.WriteLine();

                switch (opcao)
                {
                    case 1: ListarCategorias(); break;
                    case 2: CriarCategoria(); break;
                    case 3: EditarCategoria(); break;
                    case 4: RemoverCategoria(); break;
                    case 0: Console.WriteLine("A voltar ao menu principal...\n"); break;
                    default: Console.WriteLine("Opção inválida!\n"); break;
                }
            }
        }
    }
}