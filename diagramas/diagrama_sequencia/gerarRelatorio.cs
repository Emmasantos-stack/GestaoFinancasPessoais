using System;
using System.Collections.Generic;

namespace SistemaFinanceiro
{
    
    public class Transacao
    {
        private string descricao;
        private double valor;
        private string tipo; 

        public Transacao(string descricao, double valor, string tipo)
        {
            this.descricao = descricao;
            this.valor = valor;
            this.tipo = tipo;
        }

        public string GetDescricao() => descricao;
        public double GetValor() => valor;
        public string GetTipo() => tipo;
    }

    
    public class Persistencia
    {
        private List<Transacao> transacoes;

        public Persistencia()
        {
            transacoes = new List<Transacao>();

            // Transações exemplo
            transacoes.Add(new Transacao("Venda produto", 200, "Receita"));
            transacoes.Add(new Transacao("Compra material", 50, "Despesa"));
            transacoes.Add(new Transacao("Serviço prestado", 150, "Receita"));
            transacoes.Add(new Transacao("Conta de luz", 75, "Despesa"));
        }

        
        public List<Transacao> ObterTransacoes(string periodo)
        {
            
            return transacoes;
        }
    }

    
    public class Relatorio
    {
        private List<Transacao> transacoes;

        public Relatorio(List<Transacao> transacoes)
        {
            this.transacoes = transacoes;
        }

        public double CalcularTotalReceitas()
        {
            double total = 0;
            foreach (var t in transacoes)
            {
                if (t.GetTipo() == "Receita")
                    total += t.GetValor();
            }
            return total;
        }

        public double CalcularTotalDespesas()
        {
            double total = 0;
            foreach (var t in transacoes)
            {
                if (t.GetTipo() == "Despesa")
                    total += t.GetValor();
            }
            return total;
        }

        public double CalcularSaldo()
        {
            return CalcularTotalReceitas() - CalcularTotalDespesas();
        }

        public void GerarRelatorio()
        {
            Console.WriteLine("=== Relatório Financeiro ===");
            Console.WriteLine("Transações:");
            foreach (var t in transacoes)
            {
                Console.WriteLine($"{t.GetTipo()}: {t.GetDescricao()} | Valor: {t.GetValor()}");
            }
            Console.WriteLine("----------------------------");
            Console.WriteLine($"Total Receitas: {CalcularTotalReceitas()}");
            Console.WriteLine($"Total Despesas: {CalcularTotalDespesas()}");
            Console.WriteLine($"Saldo: {CalcularSaldo()}");
        }
    }

    
    public class Programa
    {
        public static void Main(string[] args)
        {
            Persistencia persistencia = new Persistencia();

            Console.WriteLine("=== Gerar Relatório Financeiro ===");
            Console.Write("Período (ex: Janeiro): ");
            string periodo = Console.ReadLine();

            
            List<Transacao> transacoesPeriodo = persistencia.ObterTransacoes(periodo);

            
            Relatorio relatorio = new Relatorio(transacoesPeriodo);

            
            relatorio.GerarRelatorio();

            Console.ReadLine();
        }
    }
}