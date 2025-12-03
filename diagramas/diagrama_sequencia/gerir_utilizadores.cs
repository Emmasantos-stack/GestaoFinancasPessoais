namespace SistemaFinanceiro
{
    public class Utilizador
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }

        public List<Categoria> Categorias { get; private set; } = new();
        public List<Transacao> Transacoes { get; private set; } = new();

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

            int novoId = Transacoes.Count == 0 ? 1 : Transacoes.Max(t => t.Id) + 1;

            var transacao = new Transacao(novoId, valor, tipo, categoria);
            Transacoes.Add(transacao);
            return transacao;
        }

        // ------------ Relatórios ----------------

        public decimal CalcularSaldo()
        {
            decimal entradas = Transacoes
                .Where(t => t.Tipo == TipoTransacao.Entrada)
                .Sum(t => t.Valor);

            decimal saidas = Transacoes
                .Where(t => t.Tipo == TipoTransacao.Saida)
                .Sum(t => t.Valor);

            return entradas - saidas;
        }

        public IEnumerable<Transacao> ListarTransacoes(bool ordenarDesc = true)
        {
            return ordenarDesc
                ? Transacoes.OrderByDescending(t => t.Data)
                : Transacoes.OrderBy(t => t.Data);
        }
    }
}
