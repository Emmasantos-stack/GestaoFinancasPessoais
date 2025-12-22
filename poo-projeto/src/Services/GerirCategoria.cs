using SistemaFinanceiro.Models;

namespace SistemaFinanceiro.Services
{
    // Classe responsável pela gestão das categorias do sistema.
    // Permite criar, editar, remover e listar categorias.
    public class GerirCategoria
    {
        // Referência ao sistema central, onde os dados são armazenados.
        private readonly Sistema _sistema;

        // Construtor da classe GerirCategoria.
        // Recebe o sistema central por injeção de dependência.
        public GerirCategoria(Sistema sistema)
        {
            _sistema = sistema;
        }

        // Devolve todas as categorias existentes no sistema.
        public List<Categoria> ObterTodas()
        {
            return _sistema.Categoria;
        }

        // Cria uma nova categoria.
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

        // Edita o nome de uma categoria existente.
        public bool Editar(int id, string novoNome)
        {
            if (string.IsNullOrWhiteSpace(novoNome))
                throw new Exception("Nome inválido.");

            var categoria = _sistema.Categoria.FirstOrDefault(c => c.Id == id);
            if (categoria == null) return false;

            // Evita categorias com nomes duplicados
            if (_sistema.Categoria.Any(c => c.Nome == novoNome && c.Id != id))
                throw new Exception("Já existe uma categoria com esse nome.");

            categoria.Nome = novoNome;
            _sistema.SalvarTudo();
            return true;
        }

        // Remove uma categoria do sistema.
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
