namespace SistemaFinanceiro
{
    public class Transacao
    {
        public int Id { get; set; }
        public string Descricao { get; set; }
        public double Valor { get; set; }
        public DateTime Data { get; set; }
        public string Tipo { get; set; }  
        public int? CategoriaId { get; set; }

        public Transacao() { }

        public Transacao(int id, string descricao, double valor, DateTime data, string tipo, int? categoriaId)
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
            if (Tipo != "Receita" && Tipo != "Despesa") return false;

            return true;
        }

        public override string ToString()
        {
            return $"{Id} - {Tipo} | {Descricao} | {Valor}€ | {Data:yyyy-MM-dd} | Cat: {CategoriaId}";
        }
    }
}