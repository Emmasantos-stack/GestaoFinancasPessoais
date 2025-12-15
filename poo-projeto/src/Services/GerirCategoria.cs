using System;
using System.Collections.Generic;
using System.Linq;
using SistemaFinanceiro.Models;

namespace SistemaFinanceiro.Services
{
    public class GerirCategoria
    {
        private readonly Sistema _sistema;

        public GerirCategoria(Sistema sistema)
        {
            _sistema = sistema;
        }

        public List<Categoria> ObterCategoria()
        {
            return _sistema.Categoria;
        }

        public Categoria CriarCategoria(string nome)
        {
            int novoId = _sistema.Categoria.Count > 0
                ? _sistema.Categoria.Max(c => c.Id) + 1
                : 1;

            var categoria = new Categoria(novoId, nome);

            if (!categoria.Validar())
                throw new Exception("Nome de categoria inválido!");

            _sistema.Categoria.Add(categoria);
            _sistema.SalvarTudo();

            return categoria;
        }

        public bool EditarCategoria(int id, string novoNome)
        {
            var categoria = _sistema.Categoria.FirstOrDefault(c => c.Id == id);
            if (categoria == null)
                return false;

            categoria.Nome = novoNome;
            _sistema.SalvarTudo();
            return true;
        }

        public bool RemoverCategoria(int id)
        {
            var categoria = _sistema.Categoria.FirstOrDefault(c => c.Id == id);
            if (categoria == null)
                return false;

            _sistema.Categoria.Remove(categoria);
            _sistema.SalvarTudo();
            return true;
        }
    }
}
