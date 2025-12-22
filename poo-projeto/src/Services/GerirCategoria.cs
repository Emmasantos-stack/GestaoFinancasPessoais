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

        public List<Categoria> ObterTodas()
        {
            return _sistema.Categoria;
        }

        public Categoria Criar(string nome)
        {
            if (string.IsNullOrWhiteSpace(nome))
                throw new Exception("Nome inválido.");

            if (_sistema.Categoria.Any(c => c.Nome == nome))
                throw new Exception("Categoria já existe.");

            int novoId = _sistema.Categoria.Count == 0
                ? 1
                : _sistema.Categoria.Max(c => c.Id) + 1;

            var categoria = new Categoria(novoId, nome);
            _sistema.Categoria.Add(categoria);
            _sistema.SalvarTudo();

            return categoria;
        }

        public bool Editar(int id, string novoNome)
        {
            if (string.IsNullOrWhiteSpace(novoNome))
                throw new Exception("Nome inválido.");

            var categoria = _sistema.Categoria.FirstOrDefault(c => c.Id == id);
            if (categoria == null) return false;

            if (_sistema.Categoria.Any(c =>
                c.Id != id &&
                string.Equals(c.Nome, novoNome, StringComparison.OrdinalIgnoreCase)))
                throw new Exception("Já existe outra categoria com esse nome.");

            categoria.Nome = novoNome;
            _sistema.SalvarTudo();

            return true;
        }

        public bool Remover(int id)
        {
            var cat = _sistema.Categoria.FirstOrDefault(c => c.Id == id);
            if (cat == null) return false;

            _sistema.Categoria.Remove(cat);
            _sistema.SalvarTudo();
            return true;
        }
    }
}
