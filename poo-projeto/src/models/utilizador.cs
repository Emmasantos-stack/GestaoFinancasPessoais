namespace SistemaFinanceiro
{
    public class Utilizador
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Perfil { get; set; }

        public Utilizador() { }

        public Utilizador(int id, string nome, string email, string password, string perfil = "User")
        {
            Id = id;
            Nome = nome;
            Email = email;
            Password = password;
            Perfil = perfil;
        }

        public bool Validar()
        {
            return 
                !string.IsNullOrWhiteSpace(Nome) &&
                !string.IsNullOrWhiteSpace(Email) &&
                !string.IsNullOrWhiteSpace(Password) &&
                !string.IsNullOrWhiteSpace(Perfil);
        }

        public override string ToString() =>
            $"{Id} - {Nome} ({Email}) | Perfil: {Perfil}";
    }
}