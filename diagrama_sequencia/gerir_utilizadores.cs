using System;

namespace SistemaFinanceiro
{
    public class Utilizador
    {
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string Perfil { get; set; } = "User"; // User ou Admin

        public Utilizador() { }

        public Utilizador(int id, string nome, string email, string password, string perfil = "User")
        {
            Id = id;
            Nome = nome;
            Email = email;
            Password = password;
            Perfil = perfil;
        }

        public bool Validar(out string erro)
        {
            erro = string.Empty;
            if (string.IsNullOrWhiteSpace(Nome)) erro = "Nome inválido.";
            if (string.IsNullOrWhiteSpace(Email) || !Email.Contains("@")) erro = Combine(erro, "Email inválido.");
            if (string.IsNullOrWhiteSpace(Password) || Password.Length < 4) erro = Combine(erro, "Password inválida (>=4).");
            return string.IsNullOrEmpty(erro);
        }

        private static string Combine(string a, string b) => string.IsNullOrEmpty(a) ? b : a + " " + b;

        public override string ToString() => $"[{Id}] {Nome} <{Email}> ({Perfil})";
    }
}
