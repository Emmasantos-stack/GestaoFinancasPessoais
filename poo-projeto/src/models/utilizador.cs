namespace SistemaFinanceiro.Models
{
    public class Utilizador
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Perfil { get; set; }

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
