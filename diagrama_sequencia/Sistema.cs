using System;
using System.Collections.Generic;
using System.Linq;

namespace SistemaFinanceiro
{
    public class Sistema
    {
        private readonly PersistenciaJson persist;
        private readonly string catFile = "categorias.json";
        private readonly string transFile = "transacoes.json";
        private readonly string usersFile = "utilizadores.json";

        public List<Categoria> Categorias { get; private set; }
        public List<Transacao> Transacoes { get; private set; }
        public List<Utilizador> Utilizadores { get; private set; }

        private int nextCategoriaId;
        private int nextTransacaoId;
        private int nextUserId;

        public Sistema(PersistenciaJson persistencia)
        {
            persist = persistencia;
            Categorias = persist.LoadList<Categoria>(catFile);
            Transacoes = persist.LoadList<Transacao>(transFile);
            Utilizadores = persist.LoadList<Utilizador>(usersFile);

            nextCategoriaId = (Categorias.Any() ? Categorias.Max(c => c.Id) + 1 : 1);
            nextTransacaoId = (Transacoes.Any() ? Transacoes.Max(t => t.Id) + 1 : 1);
            nextUserId = (Utilizadores.Any() ? Utilizadores.Max(u => u.Id) + 1 : 1);
        }

        // --- Categorias
        public Categoria CriarCategoria(string nome)
        {
            var c = new Categoria(nextCategoriaId++, nome);
            string erro;
            if (!c.Validar(out erro)) throw new ArgumentException(erro);
            Categorias.Add(c);
            PersistirCategorias();
            return c;
        }

        public void RemoverCategoria(int id)
        {
            var c = Categorias.FirstOrDefault(x => x.Id == id);
            if (c == null) throw new InvalidOperationException("Categoria não encontrada.");
            // opcional: remover ligação em transações
            Categorias.Remove(c);
            PersistirCategorias();
        }

        // --- Transações
        public Transacao CriarTransacao(string descricao, decimal valor, DateTime data, string tipo, int? categoriaId = null)
        {
            var t = new Transacao(nextTransacaoId++, descricao, valor, data, tipo, categoriaId);
            if (!t.Validar(out var erro)) throw new ArgumentException(erro);
            if (categoriaId.HasValue && !Categorias.Any(c => c.Id == categoriaId.Value))
                throw new ArgumentException("Categoria inexistente.");
            Transacoes.Add(t);
            PersistirTransacoes();
            return t;
        }

        public void RemoverTransacao(int id)
        {
            var t = Transacoes.FirstOrDefault(x => x.Id == id);
            if (t == null) throw new InvalidOperationException("Transação não encontrada.");
            Transacoes.Remove(t);
            PersistirTransacoes();
        }

        // --- Utilizadores
        public Utilizador CriarUtilizador(string nome, string email, string password, string perfil = "User")
        {
            var u = new Utilizador(nextUserId++, nome, email, password, perfil);
            if (!u.Validar(out var erro)) throw new ArgumentException(erro);
            Utilizadores.Add(u);
            PersistirUtilizadores();
            return u;
        }

        public Utilizador? ValidarCredenciais(string email, string password)
        {
            return Utilizadores.FirstOrDefault(u => u.Email == email && u.Password == password);
        }

        // --- Relatórios
        public decimal ObterSaldoAtual()
        {
            var receitas = Transacoes.Where(t => t.Tipo == "Receita").Sum(t => t.Valor);
            var despesas = Transacoes.Where(t => t.Tipo == "Despesa").Sum(t => t.Valor);
            return receitas - despesas;
        }

        // Persistência
        private void PersistirCategorias() => persist.SaveList(catFile, Categorias);
        private void PersistirTransacoes() => persist.SaveList(transFile, Transacoes);
        private void PersistirUtilizadores() => persist.SaveList(usersFile, Utilizadores);

        public void SalvarTudo()
        {
            PersistirCategorias();
            PersistirTransacoes();
            PersistirUtilizadores();
        }
    }
}
