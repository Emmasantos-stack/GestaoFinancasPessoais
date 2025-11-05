using System;
using System.Collections.Generic;

namespace SistemaFinanceiro
{
    // Classe responsável por representar uma categoria de transação
    public class Categoria
    {
        private string nome;

        public Categoria(string nome)
        {
            this.nome = nome;
        }

        public string GetNome() => nome;

        public bool Validar()
        {

            if (string.IsNullOrWhiteSpace(nome))
            {
                Console.WriteLine(" Categoria inválida!");
                return false;
            }
            return true;
        }
    }

    // Classe responsável por representar uma transação financeira
    public class Transacao
    {
        private string descricao;
        private double valor;
        private string tipo;
        private Categoria categoria;

        public void SetDescricao(string descricao) => this.descricao = descricao;
        public void SetValor(double valor) => this.valor = valor;
        public void SetTipo(string tipo) => this.tipo = tipo;
        public void SetCategoria(Categoria categoria) => this.categoria = categoria;

        public bool Validar()
        {
            if (string.IsNullOrWhiteSpace(descricao))
            {
                Console.WriteLine(" Descrição inválida!");
                return false;
            }
            if (valor <= 0)
            {
                Console.WriteLine(" Valor inválido!");
                return false;
            }
            if (tipo != "Receita" && tipo != "Despesa")
            {
                Console.WriteLine(" Tipo inválido! Use 'Receita' ou 'Despesa'.");
                return false;
            }
            if (!categoria.Validar())
            {
                return false;
            }
            return true;
        }

        public string GetResumo()
        {
            return $"{tipo}: {descricao} | Valor: {valor} | Categoria: {categoria.GetNome()}";
        }
    }

    // Classe responsável pela persistência das transações
    public class Persistencia
    {
        private List<Transacao> transacoes = new List<Transacao>();

        public void GuardarTransacao(Transacao t)
        {
            transacoes.Add(t);
            Console.WriteLine(" Transação guardada com sucesso!");
        }

        public void ListarTransacoes()
        {
            Console.WriteLine("=== Todas as Transações ===");
            foreach (var t in transacoes)
            {
                Console.WriteLine(t.GetResumo());
            }
        }
    }


    public class Programa
    {
        public static void Main(string[] args)
        {
            Persistencia persistencia = new Persistencia();

            Console.WriteLine("=== Gestor de Transações ===");


            Transacao t = new Transacao();

            Console.Write("Descrição: ");
            t.SetDescricao(Console.ReadLine());

            Console.Write("Valor: ");
            double valor;
            while (!double.TryParse(Console.ReadLine(), out valor))
            {
                Console.Write("Valor inválido. Digite um número: ");
            }
            t.SetValor(valor);

            Console.Write("Tipo (Receita/Despesa): ");
            string tipo = Console.ReadLine();
            t.SetTipo(tipo);

            Console.Write("Categoria: ");
            string nomeCategoria = Console.ReadLine();
            Categoria c = new Categoria(nomeCategoria);
            t.SetCategoria(c);


            if (t.Validar())
            {
                persistencia.GuardarTransacao(t);
            }
            else
            {
                Console.WriteLine(" Transação inválida. Não foi guardada.");
            }


            persistencia.ListarTransacoes();

            Console.ReadLine();
        }
    }
}
