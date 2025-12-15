// Define o namespace Models, onde ficam as classes que representam dados do sistema
namespace SistemaFinanceiro.Models
{
    // Classe que representa uma transação financeira
    // Uma transação pode ser uma receita ou uma despesa
    public class Transacao
    {
       // Identificador único da transação
        public int Id { get; set; }
       
       // Texto que descreve a transação
        public string Descricao { get; set; }

        // Valor monetário da transação
        public double Valor { get; set; }

        // Data em que a transação ocorreu
        public DateTime Data { get; set; }

        // Tipo da transação (Receita ou Despesa)
        // Usa um enum para evitar erros de escrita
        public TipoTransacao Tipo { get; set; }

        // ID da categoria associada à transação
        // É opcional (por isso é nullable: int?)
        public int? CategoriaId { get; set; }

        // Construtor da classe Transacao
        // É chamado quando uma nova transação é criada
        public Transacao(int id, string descricao, double valor, DateTime data, TipoTransacao tipo, int? categoriaId)
        {
            // Guarda o ID recebido
            Id = id;

            // Guarda a descrição da transação
            Descricao = descricao;

            // Guarda o valor da transação
            Valor = valor;

            // Guarda a data da transação
            Data = data;

            // Guarda o tipo da transação (Receita ou Despesa)
            Tipo = tipo;

            // Guarda o ID da categoria (ou null se não tiver categoria)
            CategoriaId = categoriaId;
        }

        // Método que valida se a transação tem dados corretos
        public bool Validar()
        {
            // Verifica se a descrição está vazia ou só com espaços
            if (string.IsNullOrWhiteSpace(Descricao)) return false;

            // Verifica se o valor é maior que zero
            if (Valor <= 0) return false;

            // Se passar todas as validações, a transação é válida
            return true;
        }
    }
}
