namespace SistemaFinanceiro
{
    public class Transacao
    {
        public int Id { get; set; }
        public decimal Valor { get; set; }
        public TipoTransacao Tipo { get; set; }
        public Categoria Categoria { get; set; }
        public DateTime Data { get; set; }

        public Transacao(int id, decimal valor, TipoTransacao tipo, Categoria categoria, DateTime? data = null)
        {
            if (valor <= 0)
                throw new ArgumentException("O valor da transação deve ser positivo.");

            Id = id;
            Valor = valor;
            Tipo = tipo;
            Categoria = categoria ?? throw new ArgumentNullException(nameof(categoria));
            Data = data ?? DateTime.Now;
        }

        public override string ToString()
        {
            string sinal = Tipo == TipoTransacao.Entrada ? "+" : "-";

            return $"{Id} | {Data:dd/MM/yyyy} | {Categoria.Nome} | {sinal}{Valor}€";
        }
    }
}
