namespace SistemaFinanceiro
{
    public class Categorias
    {
        public int Id { get; set; }
        public string Nome { get; set; }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
        public Categorias() { }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.

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