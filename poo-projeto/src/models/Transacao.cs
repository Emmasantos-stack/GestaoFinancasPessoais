namespace SistemaFinanceiro.Models
{
    public class Transacao
    {
        public int Id { get; set; }
        public string Descricao { get; set; }
        public double Valor { get; set; }
        public DateTime Data { get; set; }
        public TipoTransacao Tipo { get; set; }
        public int? CategoriaId { get; set; }

        public Transacao(int id, string descricao, double valor, DateTime data, TipoTransacao tipo, int? categoriaId)
        {
            Id = id;
            Descricao = descricao;
            Valor = valor;
            Data = data;
            Tipo = tipo;
            CategoriaId = categoriaId;
        }

        public bool Validar()
        {
            if (string.IsNullOrWhiteSpace(Descricao)) return false;
            if (Valor <= 0) return false;
            return true;
        }
    }
}
