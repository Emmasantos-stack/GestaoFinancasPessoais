using System;
using System.Collections.Generic;
using System.Linq;
using SistemaFinanceiro.Models;

namespace SistemaFinanceiro.Services
{
    public class GerirTransacoes
    {
        private readonly Sistema _sistema;

        public GerirTransacoes(Sistema sistema)
        {
            _sistema = sistema;<
        }

        public List<Transacao> ObterTransacoes() => _sistema.Transacoes;

        public Transacao CriarTransacao(string desc, double valor, DateTime data, TipoTransacao tipo, int? catId)
        {
            int id = _sistema.Transacoes.Any() ? _sistema.Transacoes.Max(t => t.Id) + 1 : 1;
            var t = new Transacao(id, desc, valor, data, tipo, catId);
            _sistema.Transacoes.Add(t);
            _sistema.SalvarTudo();
            return t;
        }

        public bool RemoverTransacao(int id)
        {
            var t = _sistema.Transacoes.FirstOrDefault(x => x.Id == id);
            if (t == null) return false;
            _sistema.Transacoes.Remove(t);
            _sistema.SalvarTudo();
            return true;
        }

        public double ObterSaldoAtual()
        {
            var r = _sistema.Transacoes.Where(t => t.Tipo == TipoTransacao.Receita).Sum(t => t.Valor);
            var d = _sistema.Transacoes.Where(t => t.Tipo == TipoTransacao.Despesa).Sum(t => t.Valor);
            return r - d;
        }

        public bool EditarTransacao(
    int id,
    string descricao,
    double valor,
    DateTime data,
    TipoTransacao tipo,
    int? categoriaId)
{
    var t = _sistema.Transacoes.FirstOrDefault(x => x.Id == id);

    if (t == null)
        return false;

    t.Descricao = descricao;
    t.Valor = valor;
    t.Data = data;
    t.Tipo = tipo;
    t.CategoriaId = categoriaId;

    _sistema.SalvarTudo();
    return true;
}

    }
}
