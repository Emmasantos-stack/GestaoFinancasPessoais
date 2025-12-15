
using SistemaFinanceiro.Models; 
// Importa as classes do namespace Models (Utilizador, Categoria, Transacao)

namespace SistemaFinanceiro.Services
{
    // Atua como camada de serviços/regras de negócio
    // Centraliza os dados principais do sistema financeiro
    public class Sistema
    {
        // Lista de utilizadores registados no sistema
        // O set é privado para impedir alterações diretas fora da classe
        public List<Utilizador> Utilizadores { get; private set; }

        // Lista de Categoria financeiras (ex.: Alimentação, Transporte, etc.)
        // Apenas a classe Sistema pode alterar esta lista
        public List<Categoria> Categoria { get; private set; }

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
                out var Categoria,    // Lista de Categoria carregada
                out var transacoes     // Lista de transações carregada
            );

            // Atribui os dados carregados às propriedades da classe
            Utilizadores = utilizadores;
            Categoria = Categoria;
            Transacoes = transacoes;
        }

        // Método responsável por guardar todos os dados do sistema
        // Chama a camada de persistência para escrever nos ficheiros JSON
        public void SalvarTudo()
        {
            PersistenciaJson.Guardar(
                Utilizadores,  // Guarda os utilizadores
                Categoria,    // Guarda as Categoria
                Transacoes     // Guarda as transações
            );
        }
    }
}
