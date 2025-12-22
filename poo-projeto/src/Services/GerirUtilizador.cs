using System;
using System.Collections.Generic;
using System.Linq;
using SistemaFinanceiro.Models;

namespace SistemaFinanceiro.Services
{
    // Classe responsável pela gestão de utilizadores do sistema.
    // Permite criar, listar e remover utilizadores.
    public class GerirUtilizador
    {
        // Referência ao sistema central.
        private readonly Sistema _sistema;

        // Construtor da classe GerirUtilizador.

        public GerirUtilizador(Sistema sistema)
        {
            _sistema = sistema;
        }

        // Devolve todos os utilizadores registados.

        public List<Utilizador> ObterTodos()
        {
            return _sistema.Utilizador;
        }

        // Cria um novo utilizador.
        public Utilizador Criar(string nome, string email, string password, string perfil)
        {
            if (string.IsNullOrWhiteSpace(nome) ||
                string.IsNullOrWhiteSpace(email) ||
                string.IsNullOrWhiteSpace(password))
            {
                throw new Exception("Dados inválidos.");
            }

            // Verifica se já existe um utilizador com o mesmo email
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

        // Remove um utilizador existente.

        public bool Remover(int id)
        {
            var utilizador = _sistema.Utilizador.FirstOrDefault(u => u.Id == id);
            if (utilizador == null) return false;

            _sistema.Utilizador.Remove(utilizador);
            _sistema.SalvarTudo();
            return true;
        }
    }
}