using System;
using System.Collections.Generic;
using System.Linq;
using SistemaFinanceiro.Models;

namespace SistemaFinanceiro.Services
{
    // Classe responsável pela geração de relatórios financeiros.
    // Permite calcular receitas, despesas, saldo e filtrar transações por categoria. 
    public class GerarRelatorio
    {
        // Lista de transações utilizada para gerar o relatório
        private readonly List<Transacao> _transacoes;

        // Construtor da classe GerarRelatorio
        // Recebe a lista de transações a analisar
        public GerarRelatorio(List<Transacao> transacoes)
        {
            _transacoes = transacoes;
        }

        // Calcula o total das receitas
        // Soma os valores de todas as transações do tipo Receita
        public double TotalReceitas()
        {
            return _transacoes
                .Where(t => t.Tipo == TipoTransacao.Receita)
                .Sum(t => t.Valor);
        }

        // Calcula o total das despesas
        // Soma todas as transações do tipo Despesa
        public double TotalDespesas()
        {
            return _transacoes
                .Where(t => t.Tipo == TipoTransacao.Despesa)
                .Sum(t => t.Valor);
        }

         // Calcula o saldo atual.
        // O saldo corresponde à diferença entre receitas e despesas.
        public double CalcularSaldo()
        {
            return TotalReceitas() - TotalDespesas();
        }

        // Devolve todas as transações associadas a uma determinada categoria.
        public IEnumerable<Transacao> TransacoesPorCategoria(int categoriaId)
        {
            return _transacoes
                .Where(t => t.CategoriaId == categoriaId);
        }

        // Apresenta o relatório financeiro no terminal
         // Mostra todas as transações, bem como totais e saldo.
        public void MostrarRelatorio()
        {
            Console.WriteLine("===== RELATÓRIO FINANCEIRO =====\n");

            foreach (var t in _transacoes)
            {
                Console.WriteLine(
                    $"{t.Data:dd/MM/yyyy} | {t.Tipo} | {t.Valor}€ | CategoriaId: {t.CategoriaId}"
                );
            }

            Console.WriteLine("\n-------------------------------");
            Console.WriteLine($"Total Receitas: {TotalReceitas()}€");
            Console.WriteLine($"Total Despesas: {TotalDespesas()}€");
            Console.WriteLine($"Saldo Atual: {CalcularSaldo()}€");
        }
    }
}
