using System;
using SistemaFinanceiro.Services;

namespace SistemaFinanceiro.UI
{
    public class MenuGerirUtilizador
    {
        private readonly GerirUtilizador _servico;

        public MenuGerirUtilizador(GerirUtilizador servico)
        {
            _servico = servico;
        }

        public void Abrir()
        {
            int opcao;

            do
            {
                Console.Clear();
                Console.WriteLine("=== GERIR Utilizador ===");
                Console.WriteLine("1 - Listar");
                Console.WriteLine("2 - Criar");
                Console.WriteLine("3 - Remover");
                Console.WriteLine("0 - Voltar");
                Console.Write("Opção: ");

                int.TryParse(Console.ReadLine(), out opcao);

                switch (opcao)
                {
                    case 1:
                        Listar();
                        break;
                    case 2:
                        Criar();
                        break;
                    case 3:
                        Remover();
                        break;
                }

            } while (opcao != 0);
        }

        private void Listar()
        {
            Console.Clear();
            var lista = _servico.ObterTodos();

            if (lista.Count == 0)
                Console.WriteLine("Sem Utilizador.");
            else
                foreach (var u in lista)
                    Console.WriteLine($"{u.Id} | {u.Nome} | {u.Email} | {u.Perfil}");

            Console.ReadKey();
        }

        private void Criar()
{
    Console.Clear();

    Console.Write("Nome: ");
    string nome = Console.ReadLine() ?? "";

    Console.Write("Email: ");
    string email = Console.ReadLine() ?? "";

    Console.Write("Password: ");
    string password = Console.ReadLine() ?? "";

    Console.Write("Perfil (admin/user): ");
    string perfil = Console.ReadLine() ?? "user";
    perfil = perfil.ToLower() != "admin" ? "user" : "admin";

    try
    {
        _servico.Criar(nome, email, password, perfil);
        Console.WriteLine("Utilizador criado com sucesso!");
    }
    catch (Exception ex)
    {
        Console.WriteLine(ex.Message);
    }

    Console.ReadKey();
}


        private void Remover()
        {
            Console.Clear();
            Console.Write("ID: ");

            if (int.TryParse(Console.ReadLine(), out int id))
            {
                if (_servico.Remover(id))
                    Console.WriteLine("Removido.");
                else
                    Console.WriteLine("Não encontrado.");
            }

            Console.ReadKey();
        }
    }
}
