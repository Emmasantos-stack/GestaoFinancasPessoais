using System;
using System.Linq;
using System.Text;
using SistemaFinanceiro.Models;

namespace SistemaFinanceiro.Services
{
    public class GerarRelatorio
    {
        private readonly Sistema _sistema;

        public GerarRelatorio(Sistema sistema)
        {
            _sistema = sistema;
        }

        // Relatório completo (todas as transações)
        public string GerarRelatorioCompleto()
        {
            var transacoes = _sistema.Transacoes;
            if (transacoes.Count == 0)
                return "Nenhuma transação registada.\n";

            var sb = new StringBuilder();
            sb.AppendLine("====== RELATÓRIO COMPLETO ======\n");

            foreach (var t in transacoes)
                EscreverTransacao(sb, t);

            sb.AppendLine($"\nSaldo atual: {_sistema.ObterSaldoAtual():0.00}€");
            sb.AppendLine("================================");

            return sb.ToString();
        }

        // Relatório mensal
        public string GerarRelatorioMensal(int ano, int mes)
        {
            var transacoes = _sistema.Transacoes
                .Where(t => t.Data.Year == ano && t.Data.Month == mes)
                .ToList();

            if (transacoes.Count == 0)
                return $"Nenhuma transação encontrada para {mes}/{ano}.\n";

            var sb = new StringBuilder();
            sb.AppendLine($"====== RELATÓRIO {mes}/{ano} ======\n");

            foreach (var t in transacoes)
                EscreverTransacao(sb, t);

            var totalReceitas = transacoes.Where(t => t.Tipo == TipoTransacao.Receita).Sum(t => t.Valor);
            var totalDespesas = transacoes.Where(t => t.Tipo == TipoTransacao.Despesa).Sum(t => t.Valor);
            var saldo = totalReceitas - totalDespesas;

            sb.AppendLine($"\nTotal Receitas: {totalReceitas:0.00}€");
            sb.AppendLine($"Total Despesas: {totalDespesas:0.00}€");
            sb.AppendLine($"Saldo Mensal: {saldo:0.00}€");
            sb.AppendLine("================================");

            return sb.ToString();
        }

        // Relatório anual
        public string GerarRelatorioAnual(int ano)
        {
            var transacoes = _sistema.Transacoes
                .Where(t => t.Data.Year == ano)
                .ToList();

            if (transacoes.Count == 0)
                return $"Nenhuma transação encontrada para o ano {ano}.\n";

            var sb = new StringBuilder();
            sb.AppendLine($"====== RELATÓRIO ANUAL {ano} ======\n");

            foreach (var t in transacoes)
                EscreverTransacao(sb, t);

            var totalReceitas = transacoes.Where(t => t.Tipo == TipoTransacao.Receita).Sum(t => t.Valor);
            var totalDespesas = transacoes.Where(t => t.Tipo == TipoTransacao.Despesa).Sum(t => t.Valor);
            var saldo = totalReceitas - totalDespesas;

            sb.AppendLine($"\nTotal Receitas: {totalReceitas:0.00}€");
            sb.AppendLine($"Total Despesas: {totalDespesas:0.00}€");
            sb.AppendLine($"Saldo Anual: {saldo:0.00}€");
            sb.AppendLine("================================");

            return sb.ToString();
        }

        // Função auxiliar
        private void EscreverTransacao(StringBuilder sb, Transacao t)
        {
            var categoria = _sistema.Categorias.FirstOrDefault(c => c.Id == t.CategoriaId)?.Nome ?? "Sem categoria";

            sb.AppendLine(
                $"{t.Data:dd/MM/yyyy} | {t.Descricao} | {categoria} | {(t.Tipo == TipoTransacao.Receita ? "+" : "-")}{t.Valor:0.00}€"
            );
        }
    }
}