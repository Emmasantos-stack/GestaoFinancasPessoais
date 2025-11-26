using System;

namespace SistemaFinanceiro
{
    public class Transacao
    {
        public int Id { get; set; }
        public string Descricao { get; set; } = string.Empty;
        public decimal Valor { get; set; }
        public DateTime Data { get; set; }
        public string Tipo { get; set; } = "Despesa"; // "Receita" ou "Despesa"
        public int? CategoriaId { get; set; } // ligação por id

        public Transacao() { }

        public Transacao(int id, string descricao, decimal valor, DateTime data, string tipo, int? categoriaId = null)
        {
            Id = id;
            Descricao = descricao;
            Valor = valor;
            Data = data;
            Tipo = tipo;
            CategoriaId = categoriaId;
        }

        public bool Validar(out string erro)
        {
            erro = string.Empty;
            if (string.IsNullOrWhiteSpace(Descricao)) erro = "Descrição inválida.";
            if (Valor <= 0) erro = (erro == string.Empty ? "" : erro + " ") + "Valor deve ser > 0.";
            if (Tipo != "Receita" && Tipo != "Despesa") erro = (erro == string.Empty ? "" : erro + " ") + "Tipo inválido (Receita/Despesa).";
            return string.IsNullOrEmpty(erro);
        }

        public override string ToString()
        {
            return $"[{Id}] {Data:yyyy-MM-dd} | {Tipo} | {Descricao} | {Valor:N2}€ | CategoriaId: {CategoriaId}";
        }
    }
}
