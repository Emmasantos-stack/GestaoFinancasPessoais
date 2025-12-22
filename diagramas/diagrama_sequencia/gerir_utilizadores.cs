namespace SistemaFinanceiro
{
    public class Utilizador
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }

        public List<Categoria> Categorias { get; private set; } = new();
        public List<Transacao> Transacao { get; private set; } = new();

        public Utilizador(int id, string nome, string email)
        {
            Id = id;
            Nome = nome;
            Email = email;
        }

        // ------------ Categorias ----------------

        public Categoria CriarCategoria(string nome, string descricao = "")
        {
            int novoId = Categorias.Count == 0 ? 1 : Categorias.Max(c => c.Id) + 1;
            var categoria = new Categoria(novoId, nome, descricao);
            Categorias.Add(categoria);
            return categoria;
        }

        // ------------ Transações ----------------

        public Transacao CriarTransacao(decimal valor, TipoTransacao tipo, int categoriaId)
        {
            var categoria = Categorias.FirstOrDefault(c => c.Id == categoriaId);
            if (categoria == null)
                throw new Exception("Categoria não encontrada.");

            int novoId = Transacao.Count == 0 ? 1 : Transacao.Max(t => t.Id) + 1;

            var transacao = new Transacao(novoId, valor, tipo, categoria);
            Transacao.Add(transacao);
            return transacao;
        }

        // ------------ Relatórios ----------------

        public decimal CalcularSaldo()
        {
            decimal entradas = Transacao
                .Where(t => t.Tipo == TipoTransacao.Entrada)
                .Sum(t => t.Valor);

            decimal saidas = Transacao
                .Where(t => t.Tipo == TipoTransacao.Saida)
                .Sum(t => t.Valor);

            return entradas - saidas;
        }

        public IEnumerable<Transacao> ListarTransacao(bool ordenarDesc = true)
        {
            return ordenarDesc
                ? Transacao.OrderByDescending(t => t.Data)
                : Transacao.OrderBy(t => t.Data);
        }
    }
}
