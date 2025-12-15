using System;
using System.Collections.Generic;
using System.Linq;
using SistemaFinanceiro.Models;

namespace SistemaFinanceiro.Services
{
    public class GerirUtilizadores
    {
        private readonly Sistema _sistema;

        public GerirUtilizadores(Sistema sistema)
        {
            _sistema = sistema;
        }

        public List<Utilizador> ObterTodos()
        {
            return _sistema.Utilizadores;
        }

        public Utilizador Criar(string nome, string email, string password, string perfil)
        {
            if (string.IsNullOrWhiteSpace(nome) ||
                string.IsNullOrWhiteSpace(email) ||
                string.IsNullOrWhiteSpace(password))
                throw new Exception("Dados inválidos.");

            if (_sistema.Utilizadores.Any(u => u.Email == email))
                throw new Exception("Email já existe.");

            int novoId = _sistema.Utilizadores.Count == 0
                ? 1
                : _sistema.Utilizadores.Max(u => u.Id) + 1;

            var utilizador = new Utilizador(novoId, nome, email, password, perfil);
            _sistema.Utilizadores.Add(utilizador);
            _sistema.SalvarTudo();

            return utilizador;
        }

        public bool Remover(int id)
        {
            var u = _sistema.Utilizadores.FirstOrDefault(x => x.Id == id);
            if (u == null) return false;

            _sistema.Utilizadores.Remove(u);
            _sistema.SalvarTudo();
            return true;
        }
    }
}
