// Define o namespace do projeto, usado para organizar o código
namespace SistemaFinanceiro
{
    // Enumeração usada para definir os tipos possíveis de uma transação
    // Serve para evitar erros de escrita como "Reseita" ou "Despessa"
    public enum TipoTransacao
    {
        // Representa uma entrada de dinheiro no sistema
        // O valor 1 é associado automaticamente a Receita
        Receita = 1,

        // Representa uma saída de dinheiro do sistema
        // O valor 2 é associado automaticamente a Despesa
        Despesa = 2
    }
}