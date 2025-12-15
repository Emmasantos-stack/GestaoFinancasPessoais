namespace SistemaFinanceiro
{
    public class Utilizadores
    {

         // Identificador único do utilizador
        public int Id { get; set; }

        // Nome do utilizador
        public string Nome { get; set; }

         // Email do utilizador (usado para login ou contacto) 
        public string Email { get; set; }

        // Palavra-passe do utilizador
        public string Password { get; set; }

         // Perfil do utilizador (ex: Admin, User)
        public string Perfil { get; set; }

        // Construtor vazio (necessário para serialização ou criação sem dados)

       public Utilizadores() { }


         // Construtor com parâmetros para criar um utilizador completo
        // O perfil tem valor por defeito "User"
        public Utilizadores(int id, string nome, string email, string password, string perfil = "User")
        {
            Id = id; // Define o ID
            Nome = nome; // Define o nome
            Email = email; // Define o email
            Password = password; // Define a password
            Perfil = perfil;  // Define o perfil
        }

   // Método que valida se os dados obrigatórios do utilizador estão preenchidos
        public bool Validar()
        {
            return 
                !string.IsNullOrWhiteSpace(Nome) &&  // Verifica nome
                !string.IsNullOrWhiteSpace(Email) && // Verifica email
                !string.IsNullOrWhiteSpace(Password) && // Verifica password
                !string.IsNullOrWhiteSpace(Perfil);  // Verifica perfil
        }

         // Retorna uma representação em texto do utilizador
        // Usado ao listar ou imprimir utilizadores

        public override string ToString() =>
            $"{Id} - {Nome} ({Email}) | Perfil: {Perfil}";
    }
}