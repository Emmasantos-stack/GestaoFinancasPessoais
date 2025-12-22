 namespace SistemaFinanceiro.Models
{

    // Classe que representa um utilizador do sistema. Os utilizadores podem ter diferentes perfis(ex: utilizador normal ou administrador).

    public class Utilizador
    {
        // Identificador único do utilizador.
        public int Id { get; set; }
        // Nome do utilizador.
        public string Nome { get; set; }

        // Endereço de email do utilizador.
        // Usado para autenticação no sistema.
        public string Email { get; set; }

        // Palavra-passe do utilizador.
        public string Password { get; set; }

        // Perfil do utilizador (ex.: "user" ou "admin").
        public string Perfil { get; set; }

        /// Construtor da classe Utilizador.
        //Inicializa todos os atributos do utilizador.

        public Utilizador(int id, string nome, string email, string password, string perfil)
        {
            Id = id;
            Nome = nome;
            Email = email;
            Password = password;
            Perfil = perfil;
        }
    }
}
