namespace SistemaFinanceiro
{
    // Classe Categorias
    // Representa uma categoria financeira do sistema
    public class Categorias
    {
        // Identificador único da categoria
        public int Id { get; set; }

        // Nome da categoria
        public string Nome { get; set; }

#pragma warning disable CS8618
        // Construtor vazio
        // Necessário para desserialização de dados (ex.: JSON)
        // O aviso é desativado porque as propriedades são inicializadas posteriormente
        public Categorias() { }
#pragma warning restore CS8618

        // Construtor com parâmetros
        // Permite criar uma categoria já com os dados definidos
        public Categorias(int id, string nome)
        {
            Id = id;     // Atribui o identificador da categoria
            Nome = nome; // Atribui o nome da categoria
        }

        // Método de validação
        // Verifica se o nome da categoria é válido
        public bool Validar()
        {
            // Retorna true se o nome não for nulo, vazio ou apenas espaços
            return !string.IsNullOrWhiteSpace(Nome);
        }

        // Método ToString sobrescrito
        // Define como a categoria será representada em formato de texto
        public override string ToString()
        {
            return $"{Id} - {Nome}";
        }
    }
}
