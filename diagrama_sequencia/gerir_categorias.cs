using System;
using System.Collections.Generic;

namespace SistemaFinanceiro
{
    // Classe Categoria para representar categorias de transações
    public class Categoria
    {
        private int id;
        private string nome;

        public Categoria(int id, string nome)
        {
            this.id = id;
            this.nome = nome;
        }

        public int GetId() => id;
        public string GetNome() => nome;

        public void SetNome(string nome) => this.nome = nome;

        public bool Validar()
        {
            if (string.IsNullOrWhiteSpace(nome))
            {
                Console.WriteLine(" Nome da categoria inválido!");
                return false;
            }
            return true;
        }

        public string GetResumo() => $"ID: {id} | Nome: {nome}";
    }

    // Classe Persistencia para gerenciar armazenamento de categorias
    public class Persistencia
    {
        private List<Categoria> categorias;
        private int proximoId = 1;

        public Persistencia()
        {
            categorias = new List<Categoria>();
        }

        
        public Categoria NovaCategoria(string nome)
        {
            Categoria c = new Categoria(proximoId++, nome);
            return c;
        }

        public void GuardarCategoria(Categoria c)
        {
            categorias.Add(c);
            Console.WriteLine($" Categoria '{c.GetNome()}' guardada com sucesso!");
        }

        public void AtualizarCategoria(Categoria c)
        {
            
            Console.WriteLine($" Categoria '{c.GetNome()}' atualizada com sucesso!");
        }

        public void RemoverCategoria(int id)
        {
            Categoria c = categorias.Find(x => x.GetId() == id);
            if (c != null)
            {
                categorias.Remove(c);
                Console.WriteLine($" Categoria '{c.GetNome()}' removida com sucesso!");
            }
            else
            {
                Console.WriteLine(" Categoria não encontrada!");
            }
        }

        public void ListarCategorias()
        {
            Console.WriteLine("=== Lista de Categorias ===");
            foreach (var c in categorias)
            {
                Console.WriteLine(c.GetResumo());
            }
        }

        public Categoria ObterCategoria(int id)
        {
            return categorias.Find(x => x.GetId() == id);
        }
    }

    
    public class Programa
    {
        public static void Main(string[] args)
        {
            Persistencia persistencia = new Persistencia();
            bool continuar = true;

            Console.WriteLine("=== Gestor de Categorias ===");

            while (continuar)
            {
                Console.WriteLine("\nOpções:");
                Console.WriteLine("1 - Listar categorias");
                Console.WriteLine("2 - Criar categoria");
                Console.WriteLine("3 - Editar categoria");
                Console.WriteLine("4 - Remover categoria");
                Console.WriteLine("5 - Sair");
                Console.Write("Escolha uma opção: ");
                string opcao = Console.ReadLine();

                switch (opcao)
                {
                    case "1":
                        persistencia.ListarCategorias();
                        break;

                    case "2":
                        Console.Write("Nome da nova categoria: ");
                        string nomeNova = Console.ReadLine();
                        Categoria nova = persistencia.NovaCategoria(nomeNova);
                        if (nova.Validar())
                            persistencia.GuardarCategoria(nova);
                        break;

                    case "3":
                        Console.Write("ID da categoria a editar: ");
                        int idEditar;
                        if (int.TryParse(Console.ReadLine(), out idEditar))
                        {
                            Categoria cEditar = persistencia.ObterCategoria(idEditar);
                            if (cEditar != null)
                            {
                                Console.Write("Novo nome: ");
                                string novoNome = Console.ReadLine();
                                cEditar.SetNome(novoNome);
                                if (cEditar.Validar())
                                    persistencia.AtualizarCategoria(cEditar);
                            }
                            else
                            {
                                Console.WriteLine(" Categoria não encontrada!");
                            }
                        }
                        break;

                    case "4":
                        Console.Write("ID da categoria a remover: ");
                        int idRemover;
                        if (int.TryParse(Console.ReadLine(), out idRemover))
                        {
                            persistencia.RemoverCategoria(idRemover);
                        }
                        break;

                    case "5":
                        continuar = false;
                        Console.WriteLine("Saindo do gestor de categorias...");
                        break;

                    default:
                        Console.WriteLine("Opção inválida!");
                        break;
                }
            }

            Console.ReadLine();
        }
    }
}