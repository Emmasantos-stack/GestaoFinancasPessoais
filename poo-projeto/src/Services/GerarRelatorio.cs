using System;
using System.Linq;
using System.Text;

namespace SistemaFinanceiro.Services
{
    public class GerarRelatorio
    {
        private readonly GerirTransacoes _transacoes;
        private readonly GerirCategorias _categorias;

        public GerarRelatorio(GerirTransacoes t, GerirCategorias c)
        {
            _transacoes = t;
            _categorias = c;
        }

        public string GerarRelatorioCompleto()
        {
            var sb = new StringBuilder();
            sb.AppendLine($"Saldo atual: {_transacoes.ObterSaldoAtual()}€");
            return sb.ToString();
        }
    }
}
