using System;
using System.Linq;
using System.Text;

public class GerarRelatorio
{
    private readonly GerirTransacoes _transacoesServico;
    private readonly GerirCategorias _categoriasServico;

    public GerarRelatorio(GerirTransacoes transacoesServico, GerirCategorias categoriasServico)
    {
        _transacoesServico = transacoesServico;
        _categoriasServico = categoriasServico;
    }

    // --------------------------------------------------------------------
    // RELATÓRIO COMPLETO DO SISTEMA
    // --------------------------------------------------------------------
    public string GerarRelatorioCompleto()
    {
        var sb = new StringBuilder();

        sb.AppendLine("=========================================");
        sb.AppendLine("        RELATÓRIO FINANCEIRO COMPLETO     ");
        sb.AppendLine("=========================================\n");

        sb.AppendLine(GerarResumoGeral());
        sb.AppendLine(GerarResumoCategorias());
        sb.AppendLine(GerarResumoTransacoes());

        return sb.ToString();
    }

    // --------------------------------------------------------------------
    // RESUMO GERAL DO SISTEMA
    // --------------------------------------------------------------------
    public string GerarResumoGeral()
    {
        decimal saldo = _transacoesServico.ObterSaldoAtual();

        var receitas = _transacoesServico.ObterTransacoes()
            .Where(t => t.Tipo == TipoTransacao.Receita)
            .Sum(t => t.Valor);

        var despesas = _transacoesServico.ObterTransacoes()
            .Where(t => t.Tipo == TipoTransacao.Despesa)
            .Sum(t => t.Valor);

        var sb = new StringBuilder();
        sb.AppendLine("---- RESUMO GERAL ----");
        sb.AppendLine($"Total de Receitas: {receitas}€");
        sb.AppendLine($"Total de Despesas: {despesas}€");
        sb.AppendLine($"Saldo Atual: {saldo}€");
        sb.AppendLine();

        return sb.ToString();
    }

    // --------------------------------------------------------------------
    // RELATÓRIO DE CATEGORIAS
    // --------------------------------------------------------------------
    public string GerarResumoCategorias()
    {
        var categorias = _categoriasServico.ObterCategorias();
        var transacoes = _transacoesServico.ObterTransacoes();

        var sb = new StringBuilder();

        sb.AppendLine("---- RESUMO POR CATEGORIAS ----");

        if (categorias.Count == 0)
        {
            sb.AppendLine("Não existem categorias.\n");
            return sb.ToString();
        }

        foreach (var cat in categorias)
        {
            var total = transacoes
                .Where(t => t.CategoriaId == cat.Id)
                .Sum(t => t.Valor * (t.Tipo == TipoTransacao.Despesa ? -1 : 1));

            sb.AppendLine($"{cat.Nome} → {total}€");
        }

        sb.AppendLine();
        return sb.ToString();
    }

    // --------------------------------------------------------------------
    // LISTAGEM DE TODAS AS TRANSAÇÕES
    // --------------------------------------------------------------------
    public string GerarResumoTransacoes()
    {
        var transacoes = _transacoesServico.ObterTransacoes();
        var categorias = _categoriasServico.ObterCategorias();

        var sb = new StringBuilder();

        sb.AppendLine("---- TODAS AS TRANSAÇÕES ----");

        if (transacoes.Count == 0)
        {
            sb.AppendLine("Nenhuma transação registada.\n");
            return sb.ToString();
        }

        foreach (var t in transacoes)
        {
            var categoria = categorias.FirstOrDefault(c => c.Id == t.CategoriaId)?.Nome ?? "Sem categoria";

            sb.AppendLine(
                $"{t.Id} | {t.Data:dd/MM/yyyy} | {t.Tipo} | {t.Descricao} | {t.Valor}€ | Categoria: {categoria}"
            );
        }

        sb.AppendLine();
        return sb.ToString();
    }

    // --------------------------------------------------------------------
    // RELATÓRIO MENSAL
    // --------------------------------------------------------------------
    public string GerarRelatorioMensal(int ano, int mes)
    {
        var transacoes = _transacoesServico.ObterTransacoes()
            .Where(t => t.Data.Month == mes && t.Data.Year == ano)
            .ToList();

        var sb = new StringBuilder();

        sb.AppendLine("=========================================");
        sb.AppendLine($"   RELATÓRIO MENSAL {mes:00}/{ano}   ");
        sb.AppendLine("=========================================\n");

        if (transacoes.Count == 0)
        {
            sb.AppendLine("Nenhuma transação neste mês.\n");
            return sb.ToString();
        }

        decimal receitas = transacoes
            .Where(t => t.Tipo == TipoTransacao.Receita)
            .Sum(t => t.Valor);

        decimal despesas = transacoes
            .Where(t => t.Tipo == TipoTransacao.Despesa)
            .Sum(t => t.Valor);

        sb.AppendLine($"Receitas: {receitas}€");
        sb.AppendLine($"Despesas: {despesas}€");
        sb.AppendLine($"Saldo Mensal: {receitas - despesas}€\n");

        sb.AppendLine("--- Transações ---");
        foreach (var t in transacoes)
        {
            sb.AppendLine($"{t.Data:dd/MM/yyyy} | {t.Tipo} | {t.Descricao} | {t.Valor}€");
        }

        sb.AppendLine();

        return sb.ToString();
    }
}
