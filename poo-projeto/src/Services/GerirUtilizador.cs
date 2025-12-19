using System;
using System.Collections.Generic;
using System.Linq;
using SistemaFinanceiro.Models;

namespace SistemaFinanceiro.Services
{
    public class GerirUtilizador
    {
        private readonly Sistema _sistema;

        public GerirUtilizador(Sistema sistema)
        {
            _sistema = sistema;
        }

        public List<Utilizador> ObterTodos()
        {
            return _sistema.Utilizador;
        }

        public Utilizador Criar(string nome, string email, string password, string perfil)
        {
            if (string.IsNullOrWhiteSpace(nome) ||
                string.IsNullOrWhiteSpace(email) ||
                string.IsNullOrWhiteSpace(password))
            {
                throw new Exception("Dados inválidos.");
            }

            // ✅ comparação segura de email
            if (_sistema.Utilizador.Any(u =>
                u.Email.Equals(email, StringComparison.OrdinalIgnoreCase)))
            {
                throw new Exception("Email já existe.");
            }

            int novoId = _sistema.Utilizador.Count == 0
                ? 1
                : _sistema.Utilizador.Max(u => u.Id) + 1;

            var utilizador = new Utilizador(
                novoId,
                nome,
                email,
                password,
                perfil
            );

            _sistema.Utilizador.Add(utilizador);
            _sistema.SalvarTudo();

            return utilizador;
        }

        public bool Remover(int id)
        {
            var u = _sistema.Utilizador.FirstOrDefault(x => x.Id == id);
            if (u == null) return false;

            _sistema.Utilizador.Remove(u);
            _sistema.SalvarTudo();
            return true;
        }
    }
}
