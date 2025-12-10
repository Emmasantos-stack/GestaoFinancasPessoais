using System;

namespace poo_projeto.Services
{
    public class MenuGerirProdutos
    {
        private readonly GerirProdutos _gerirProdutos;

        public MenuGerirProdutos(GerirProdutos gerirProdutos)
        {
            _gerirProdutos = gerirProdutos;
        }

        public void MostrarMenu()
        {
            int opcao;

            do
            {
                Console.WriteLine("===== MENU PRODUTOS =====");
                Console.WriteLine("1 - Adicionar Produto");
                Console.WriteLine("2 - Listar Produtos");
                Console.WriteLine("3 - Editar Produto");
                Console.WriteLine("4 - Remover Produto");
                Console.WriteLine("0 - Voltar");
                Console.Write("Opção: ");

                int.TryParse(Console.ReadLine(), out opcao);
                Console.WriteLine();

                switch (opcao)
                {
                    case 1: _gerirProdutos.AdicionarProduto(); break;
                    case 2: _gerirProdutos.ListarProdutos(); break;
                    case 3: _gerirProdutos.EditarProduto(); break;
                    case 4: _gerirProdutos.RemoverProduto(); break;
                    case 0: break;
                    default: Console.WriteLine("Opção inválida!\n"); break;
                }
            }
            while (opcao != 0);
        }
    }
}
