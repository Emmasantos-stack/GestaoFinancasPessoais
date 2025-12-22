
using SistemaFinanceiro.Models; 
// Importa as classes do namespace Models (Utilizador, Categoria, Transacao)

namespace SistemaFinanceiro.Services
{
        

    // Atua como camada de serviços/regras de negócio
    // Centraliza os dados principais do sistema financeiro
    public class Sistema
    {
        // Lista de Utilizador registados no sistema
        // O set é privado para impedir alterações diretas fora da classe
        public List<Utilizador> Utilizador { get; private set; }

        // Lista de Categoria financeiras (ex.: Alimentação, Transporte, etc.)
        // Apenas a classe Sistema pode alterar esta lista
        public List<Categoria> Categoria { get; private set; }

        // Lista de transações financeiras (receitas e despesas)
        // Controlada internamente pela classe Sistema
        public List<Transacao> Transacao { get; private set; }

        public Utilizador? UtilizadorAutenticado { get; set; }
        // Construtor da classe Sistema
        // É executado quando o sistema é iniciado
        public Sistema()
        {
            // Carrega os dados guardados nos ficheiros JSON
            // Utiliza variáveis locais para receber os dados carregados
            PersistenciaJson.Carregar(
                out var utilizadores,
                out var categorias,
                out var transacoes
            );

            Utilizador = utilizadores ?? new List<Utilizador>();
            Categoria = categorias ?? new List<Categoria>();
            Transacao = transacoes ?? new List<Transacao>();
        
        }

        // Método responsável por guardar todos os dados do sistema
        // Chama a camada de persistência para escrever nos ficheiros JSON
        public void SalvarTudo()
        {
            PersistenciaJson.Guardar(
                Utilizador,  // Guarda os Utilizador
                Categoria,    // Guarda as Categoria
                Transacao     // Guarda as transações
            );
        }
    }
}
