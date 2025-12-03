namespace SistemaFinanceiro
{
    public class Categorias
    {
        public int Id { get; set; }
        public string Nome { get; set; }

        public Categorias() { }

        public Categorias(int id, string nome)
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