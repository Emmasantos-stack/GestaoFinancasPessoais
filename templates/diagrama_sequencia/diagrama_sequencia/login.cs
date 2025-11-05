using System;
using System.Collections.Generic;

namespace SistemaFinanceiro
{

    public class Utilizador
    {
        private string nome;
        private string email;
        private string password;

        public Utilizador(string nome, string email, string password)
        {
            this.nome = nome;
            this.email = email;
            this.password = password;
        }

        public string GetNome() => nome;
        public string GetEmail() => email;
        public string GetPassword() => password;
    }

    
    public class Persistencia
    {
        private List<Utilizador> baseDeDados;

        public Persistencia()
        {
            baseDeDados = new List<Utilizador>();

            // utilizadores teste
            baseDeDados.Add(new Utilizador("Emma Santos", "emma@example.com", "segredo123"));
            baseDeDados.Add(new Utilizador("João Silva", "joao@example.com", "123456"));
        }

        
        public Utilizador ValidarCredenciais(string email, string password)
        {
            foreach (Utilizador u in baseDeDados)
            {
                if (u.GetEmail() == email && u.GetPassword() == password)
                {
                    return u; 
                }
            }
            return null; 
        }
    }


    public class Programa
    {
        public static void Main(string[] args)
        {
            Persistencia persistencia = new Persistencia();

            Console.WriteLine("=== Login ===");

            
            Console.Write("Email: ");
            string email = Console.ReadLine();

            Console.Write("Password: ");
            string password = Console.ReadLine();

        
            Utilizador u = persistencia.ValidarCredenciais(email, password);

            if (u != null)
            {
                Console.WriteLine($" Acesso concedido! Bem-vindo {u.GetNome()}");
            }
            else
            {
                Console.WriteLine(" Login falhou! Email ou password incorretos.");
            }

            Console.ReadLine();
        }
    }
}