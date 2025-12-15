
using SistemaFinanceiro.Models; 
// Importa as classes do namespace Models (Utilizador, Categorias, Transacao)

namespace SistemaFinanceiro.Services
{
    // Atua como camada de serviços/regras de negócio
    // Centraliza os dados principais do sistema financeiro
    public class Sistema
    {
        // Lista de utilizadores registados no sistema
        // O set é privado para impedir alterações diretas fora da classe
        public List<Utilizador> Utilizadores { get; private set; }

        // Lista de categorias financeiras (ex.: Alimentação, Transporte, etc.)
        // Apenas a classe Sistema pode alterar esta lista
        public List<Categorias> Categorias { get; private set; }

        // Lista de transações financeiras (receitas e despesas)
        // Controlada internamente pela classe Sistema
        public List<Transacao> Transacoes { get; private set; }

        // Construtor da classe Sistema
        // É executado quando o sistema é iniciado
        public Sistema()
        {
            // Carrega os dados guardados nos ficheiros JSON
            // Utiliza variáveis locais para receber os dados carregados
            PersistenciaJson.Carregar(
                out var utilizadores,  // Lista de utilizadores carregada
                out var categorias,    // Lista de categorias carregada
                out var transacoes     // Lista de transações carregada
            );

            // Atribui os dados carregados às propriedades da classe
            Utilizadores = utilizadores;
            Categorias = categorias;
            Transacoes = transacoes;
        }

        // Método responsável por guardar todos os dados do sistema
        // Chama a camada de persistência para escrever nos ficheiros JSON
        public void SalvarTudo()
        {
            PersistenciaJson.Guardar(
                Utilizadores,  // Guarda os utilizadores
                Categorias,    // Guarda as categorias
                Transacoes     // Guarda as transações
            );
        }
    }
}
