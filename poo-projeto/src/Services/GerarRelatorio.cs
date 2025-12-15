using System;
using System.Collections.Generic;
using System.Linq;
using SistemaFinanceiro.Models;

namespace SistemaFinanceiro.Services
{

// Classe responsável pela geração de relatórios financeiros. Esta classe permite calcular totais de receitas, despesas e o saldo final com base numa lista de transações.
    
    public class GerarRelatorio
    {
        private readonly List<Transacao> _transacoes;

        public GerarRelatorio(List<Transacao> transacoes)
        {
        //Construtor da classe GerarRelatorio. Lista de transações utilizada para gerar o relatório.
            _transacoes = transacoes;
        }

/// Calcula o total das receitas. Soma de todas as transações do tipo Receita.       

        public decimal TotalReceitas()
        {
            return _transacoes
                .Where(t => t.Tipo == TipoTransacao.Entrada)
                .Sum(t => t.Valor);
        }

// Calcula o total das despesas. Soma de todas as transações do tipo Despesa.
        public decimal TotalDespesas()
        {
            return _transacoes
                .Where(t => t.Tipo == TipoTransacao.Saida)
                .Sum(t => t.Valor);
        }
    // Calcula o saldo final. Diferença entre o total de receitas e o total de despesas.

        public decimal CalcularSaldo()
        {
            return TotalReceitas() - TotalDespesas();
        }

        

        public IEnumerable<Transacao> TransacoesPorCategoria(int categoriaId)
        {
            return _transacoes
                .Where(t => t.Categoria.Id == categoriaId);
        }

        // Apresenta o relatório financeiro no terminal.
        public void MostrarRelatorio()
        {
            Console.WriteLine("===== RELATÓRIO FINANCEIRO =====\n");

            foreach (var t in _transacoes)
            {
                Console.WriteLine(
                    $"{t.Data:dd/MM/yyyy} | {t.Tipo} | {t.Valor}€ | {t.Categoria.Nome}"
                );
            }

            Console.WriteLine("\n-------------------------------");
            Console.WriteLine($"Total Receitas: {TotalReceitas()}€");
            Console.WriteLine($"Total Despesas: {TotalDespesas()}€");
            Console.WriteLine($"Saldo Atual: {CalcularSaldo()}€");
        }
    }
}
