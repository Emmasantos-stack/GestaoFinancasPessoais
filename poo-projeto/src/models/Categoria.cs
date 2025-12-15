namespace SistemaFinanceiro.Models
{
    public class Categoria
    {
        public int Id { get; set; }
        public string Nome { get; set; }


        public Categoria(int id, string nome)
        {
            Id = id;
            Nome = nome;
        }

        public bool Validar()
        {
            return !string.IsNullOrWhiteSpace(Nome);
        }

        public override string ToString()
        {
            return $"{Id} - {Nome}";
        }
    }
}
