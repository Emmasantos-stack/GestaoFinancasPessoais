using System;
using System.Collections.Generic;

namespace SistemaFinanceiro
{
    public class Utilizador
    {
        // Atributos
        private int id;
        private string nome;
        private string email;
        private string password;
        private string perfil;

        // Relacionamento: um Utilizador possui várias Transações
        public List<Transacao> Transacoes { get; set; }

        // Construtor
        public Utilizador(int id, string nome, string email, string password, string perfil)
        {
            this.id = id;
            this.nome = nome;
            this.email = email;
            this.password = password;
            this.perfil = perfil;
            Transacoes = new List<Transacao>();
        }

        // Métodos
        public void Login()
        {
            Console.WriteLine($"{nome} fez login com sucesso!");
        }

        public void Logout()
        {
            Console.WriteLine($"{nome} saiu da conta.");
        }
    }

    public class Transacao
    {
        // Atributos
        private int id;
        private string descricao;
        private double valor;
        private DateTime data;
        private string tipo;

        // Relacionamento: cada Transação pertence a uma Categoria
        public Categoria Categoria { get; set; }

        // Construtor
        public Transacao(int id, string descricao, double valor, DateTime data, string tipo, Categoria categoria)
        {
            this.id = id;
            this.descricao = descricao;
            this.valor = valor;
            this.data = data;
            this.tipo = tipo;
            this.Categoria = categoria;
        }

        // Método
        public bool ValidarValor()
        {
            return valor >= 0;
        }
    }

    public class Categoria
    {
        // Atributos
        private int id;
        private string nome;

        // Construtor
        public Categoria(int id, string nome)
        {
            this.id = id;
            this.nome = nome;
        }
    }

    public class Relatorio
    {
        // Métodos
        public double CalcularSaldo(List<Transacao> transacoes)
        {
            double saldo = 0;
            foreach (var t in transacoes)
            {
                if (t.ValidarValor())
                {
                    saldo += t.Tipo == "Receita" ? t.Valor : -t.Valor;
                }
            }
            return saldo;
        }

        public double TotalReceitas(List<Transacao> transacoes)
        {
            double total = 0;
            foreach (var t in transacoes)
            {
                if (t.Tipo == "Receita")
                    total += t.Valor;
            }
            return total;
        }

        public double TotalDespesas(List<Transacao> transacoes)
        {
            double total = 0;
            foreach (var t in transacoes)
            {
                if (t.Tipo == "Despesa")
                    total += t.Valor;
            }
            return total;
        }
    }
}