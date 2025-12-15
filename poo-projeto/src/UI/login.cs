using System.Linq;
using SistemaFinanceiro.Models;

namespace SistemaFinanceiro.Services
{
    public class Login
    {
        private readonly Sistema _sistema;

        public Login(Sistema sistema)
        {
            _sistema = sistema;
        }

        // ---------------------------------------------
        // AUTENTICAÇÃO
        // ---------------------------------------------
        public Utilizador? Autenticar(string email, string password)
        {
            return _sistema.Utilizadores.FirstOrDefault(u =>
                string.Equals(u.Email, email) &&
                string.Equals(u.Password, password));
        }

        public bool CredenciaisValidas(string email, string password)
        {
            return Autenticar(email, password) != null;
        }
    }
}
