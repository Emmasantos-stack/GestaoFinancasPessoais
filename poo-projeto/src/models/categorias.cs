namespace SistemaFinanceiro
{
        // Classe que representa uma Categoria dentro do sistema financeiro
    public class Categorias
    {
         // Identificador único da categoria
        public int Id { get; set; }
        // Nome da categoria (ex: Alimentação, Transporte, Lazer...)
        public string Nome { get; set; }

        // Construtor vazio (necessário para desserialização e compatibilidade com JSON)
        public Categorias() { }

         // Construtor que recebe os valores iniciais da categoria
        public Categorias(int id, string nome)
        {
            Id = id;            // Define o ID da categoria
            Nome = nome;        // Define o nome da categoria
        }

        // Função para validar se a categoria está correta
        // Aqui verifica se o nome não está vazio ou composto apenas por espaços
        public bool Validar()
        {
            return !string.IsNullOrWhiteSpace(Nome);
        }

        // Representação textual da categoria (útil para listagens, debug ou logs)
        public override string ToString()
        {
            return $"{Id} - {Nome}";
        }
    }
}