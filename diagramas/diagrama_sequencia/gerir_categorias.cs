namespace SistemaFinanceiro
{
    public class Categoria
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }

        public Categoria(int id, string nome, string descricao = "")
        {
            if (string.IsNullOrWhiteSpace(nome))
                throw new ArgumentException("O nome da categoria não pode estar vazio.");

            Id = id;
            Nome = nome.Trim();
            Descricao = descricao.Trim();
        }

        public override string ToString()
        {
            return $"{Id} - {Nome} ({Descricao})";
        }
    }
}
