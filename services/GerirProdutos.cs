using System;
using System.Collections.Generic;

namespace poo_projeto.Services
{
    public class GerirProdutos
    {
        private readonly Sistema _sistema;

        public GerirProdutos(Sistema sistema)
        {
            _sistema = sistema;
        }

        public void AdicionarProduto()
        {
            Console.Write("Nome do produto: ");
            string nome = Console.ReadLine();

            Console.Write("Preço: ");
            double preco = double.Parse(Console.ReadLine());

            Console.Write("Stock inicial: ");
            int stock = int.Parse(Console.ReadLine());

            var produto = _sistema.CriarProduto(nome, preco, stock);

            Console.WriteLine($"Produto criado com ID {produto.Id}!\n");
        }

        public void ListarProdutos()
        {
            var produtos = _sistema.Produtos;

            if (produtos.Count == 0)
            {
                Console.WriteLine("Nenhum produto registado.\n");
                return;
            }

            Console.WriteLine("=== LISTA DE PRODUTOS ===");
            foreach (var p in produtos)
            {
                Console.WriteLine($"ID: {p.Id} | Nome: {p.Nome} | Preço: {p.Preco} | Stock: {p.Stock}");
            }
            Console.WriteLine();
        }

        public void RemoverProduto()
        {
            Console.Write("ID do produto a remover: ");
            int id = int.Parse(Console.ReadLine());

            bool removido = _sistema.RemoverProduto(id);

            Console.WriteLine(removido ? "Produto removido!\n" : "Produto não encontrado.\n");
        }

        public void EditarProduto()
        {
            Console.Write("ID do produto a editar: ");
            int id = int.Parse(Console.ReadLine());

            var produto = _sistema.ObterProduto(id);
            if (produto == null)
            {
                Console.WriteLine("Produto não encontrado.\n");
                return;
            }

            Console.Write("Novo nome (enter para manter): ");
            string nome = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(nome)) produto.Nome = nome;

            Console.Write("Novo preço (enter para manter): ");
            string precoStr = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(precoStr)) produto.Preco = double.Parse(precoStr);

            Console.Write("Novo stock (enter para manter): ");
            string stockStr = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(stockStr)) produto.Stock = int.Parse(stockStr);

            _sistema.Gravar();

            Console.WriteLine("Produto atualizado!\n");
        }
    }
}
