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
            // Validação defensiva
            if (string.IsNullOrWhiteSpace(email) ||
                string.IsNullOrWhiteSpace(password) ||
                _sistema.Utilizador == null)
                return null;

            return _sistema.Utilizador.FirstOrDefault(u =>
                u.Email == email &&
                u.Password == password);
        }

        public bool CredenciaisValidas(string email, string password)
        {
            return Autenticar(email, password) != null;
        }
    }
}
