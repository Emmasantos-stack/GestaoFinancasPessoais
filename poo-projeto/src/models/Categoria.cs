namespace SistemaFinanceiro.Models
{
    // Classe Categoria
    // Representa uma categoria financeira do sistema
    public class Categoria
    {
        // Identificador único da categoria
        public int Id { get; set; }

        // Nome da categoria
        public string Nome { get; set; }

#pragma warning disable CS8618
        // Construtor vazio
        // Necessário para desserialização (ex.: JSON)
        public Categoria() { }
#pragma warning restore CS8618

        // Construtor com parâmetros
        public Categoria(int id, string nome)
        {
            Id = id;
            Nome = nome;
        }

        // Validação do nome da categoria
        public bool Validar()
        {
            return !string.IsNullOrWhiteSpace(Nome);
        }

        // Representação textual da categoria
        public override string ToString()
        {
            return $"{Id} - {Nome}";
        }
    }
}
