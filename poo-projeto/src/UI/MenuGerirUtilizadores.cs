using System;
using SistemaFinanceiro.Services;

namespace SistemaFinanceiro.UI
{
    public class MenuGerirUtilizadores
    {
        private readonly GerirUtilizadores _servico;

        public MenuGerirUtilizadores(GerirUtilizadores servico)
        {
            _servico = servico;
        }

        public void Abrir()
        {
            int opcao;

            do
            {
                Console.Clear();
                Console.WriteLine("=== GERIR UTILIZADORES ===");
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
                Console.WriteLine("Sem utilizadores.");
            else
                foreach (var u in lista)
                    Console.WriteLine($"{u.Id} | {u.Nome} | {u.Email} | {u.Perfil}");

            Console.ReadKey();
        }

        private void Criar()
        {
            Console.Clear();

            Console.Write("Nome: ");
            string nome = Console.ReadLine();

            Console.Write("Email: ");
            string email = Console.ReadLine();

            Console.Write("Password: ");
            string password = Console.ReadLine();

            Console.Write("Perfil: ");
            string perfil = Console.ReadLine();

            try
            {
                _servico.Criar(nome, email, password, perfil);
                Console.WriteLine("Utilizador criado!");
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
