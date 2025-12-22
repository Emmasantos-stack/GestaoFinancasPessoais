namespace SistemaFinanceiro.Models
{
    // Classe que representa uma categoria financeira do sistema.
    // As categorias são usadas para classificar transações (ex: Alimentação, Transporte, Lazer).
    public class Categoria
    {
        //Identificador único da categoria.
        public int Id { get; set; }

        // Nome da categoria.
        public string Nome { get; set; }

#pragma warning disable CS8618
        // Construtor vazio.
        // Necessário para processos de desserialização (ex: leitura de dados a partir de JSON).

        public Categoria() { }
#pragma warning restore CS8618

        // Construtor com parâmetros.
        // Inicializa uma nova categoria com ID e nome.

        public Categoria(int id, string nome)
        {
            Id = id;
            Nome = nome;
        }

        // Valida os dados da categoria.
        // O nome não pode ser nulo, vazio ou apenas espaços.
        //True se for válida, False caso contrário
        public bool Validar()
        {
            return !string.IsNullOrWhiteSpace(Nome);
        }

        // Retorna uma representação textual da categoria.
        // String no formato "Id - Nome"
        public override string ToString()
        {
            return $"{Id} - {Nome}";
        }
    }
}