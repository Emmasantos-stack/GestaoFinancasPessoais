using System;
using System.Linq;
using SistemaFinanceiro.Models;

namespace SistemaFinanceiro.Services
{
    public class GerirCategorias
    {
        private readonly Sistema _sistema; // Armazena referência do objeto Sistema para acessar listas e métodos globais

        public GerirCategorias(Sistema sistema)
        {
            _sistema = sistema; // Recebe o Sistema por injeção e permite manipular dados centralizados
        }

        //Listar Categorias
        public void ListarCategorias()
        {
            Console.WriteLine("\n====== LISTA DE CATEGORIAS ======");

                    // Caso não existam categorias cadastradas
            if (_sistema.Categorias.Count == 0)
            {
                Console.WriteLine("Nenhuma categoria registada.");
                return; // Sai do método
            }

                 // Percorre e imprime todas as categorias existentes
            foreach (var c in _sistema.Categorias)
            {
                Console.WriteLine($"ID: {c.Id} | Nome: {c.Nome}");
            }

            Console.WriteLine("================================\n");
        }

            //Criar Categoria
        public void CriarCategoria()
        {
            Console.Write("\nIntroduza o nome da nova categoria: ");
            string nome = Console.ReadLine(); // Recebe o nome informado pelo utilizador

                 // Validar campo vazio ou apenas espaços
            if (string.IsNullOrWhiteSpace(nome))
            {
                Console.WriteLine("Nome inválido!");
                return;
            }

                // Cria nova categoria no sistema
            var nova = _sistema.CriarCategoria(nome);

            Console.WriteLine($"Categoria criada com sucesso! (ID: {nova.Id})\n");
        }

            //EDITAR CATEGORIA
        public void EditarCategoria()
        {
            ListarCategorias(); // Mostra lista antes de editar

            // Validação do ID inserido
            Console.Write("Introduza o ID da categoria a editar: ");
            if (!int.TryParse(Console.ReadLine(), out int id))
            {  
                 // Validação: ID tem de ser um número
                Console.WriteLine("ID inválido!");
                return;
            }

              // Localiza categoria com base no ID
            var categoria = _sistema.Categorias.FirstOrDefault(c => c.Id == id);
            if (categoria == null)
            {
                Console.WriteLine("Categoria não encontrada!");
                return;
            }

            // Solicita novo nome
            Console.Write($"Novo nome para a categoria '{categoria.Nome}': ");
            string novoNome = Console.ReadLine();

            // Verifica se o nome é válido
            if (string.IsNullOrWhiteSpace(novoNome))
            {
                Console.WriteLine("Nome inválido!");
                return;
            }

            categoria.Nome = novoNome; // Atualiza o nome
            _sistema.GravarDados(); // Guarda alteração no sistema

            Console.WriteLine("Categoria atualizada com sucesso!\n");
        }

             //REMOVER CATEGORIA
        public void RemoverCategoria()
        {
            ListarCategorias();

            Console.Write("Introduza o ID da categoria a remover: ");
            if (!int.TryParse(Console.ReadLine(), out int id))
            {
                Console.WriteLine("ID inválido!");
                return;
            }
                 // Procura categoria com o ID
            var categoria = _sistema.Categorias.FirstOrDefault(c => c.Id == id);
            if (categoria == null)
            {
                Console.WriteLine("Categoria não encontrada!");
                return;
            }

                 // Verifica se existem transações associadas à categoria
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
                // O menu funciona até o utilizador escolher a opção 0  
            while (opcao != 0)
            {
                Console.WriteLine("===== GESTÃO DE CATEGORIAS =====");
                Console.WriteLine("1 - Listar categorias");
                Console.WriteLine("2 - Criar categoria");
                Console.WriteLine("3 - Editar categoria");
                Console.WriteLine("4 - Remover categoria");
                Console.WriteLine("0 - Voltar");
                Console.Write("Opção: ");

                    // Tenta converter entrada para número
                int.TryParse(Console.ReadLine(), out opcao);
                Console.WriteLine();
                     // Seleção da opção
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