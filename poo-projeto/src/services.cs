using System;
using System.Collections.Generic;
using System.Linq;

namespace SistemaFinanceiro
{
    public class Sistema
    {
        private readonly PersistenciaJson _persistencia;

        public List<Categoria> Categorias { get; private set; }
        public List<Transacao> Transacoes { get; private set; }
        public List<Utilizador> Utilizadores { get; private set; }

        private int _nextIdCategoria = 1;
        private int _nextIdTransacao = 1;
        private int _nextIdUtilizador = 1;

        public Sistema(PersistenciaJson persistencia)
        {
            _persistencia = persistencia;

            // Carregar dados existentes
            Categorias = _persistencia.CarregarCategorias();
            Transacoes = _persistencia.CarregarTransacoes();
            Utilizadores = _persistencia.CarregarUtilizadores();

            // Ajustar IDs automáticos
            if (Categorias.Any()) _nextIdCategoria = Categorias.Max(c => c.Id) + 1;
            if (Transacoes.Any()) _nextIdTransacao = Transacoes.Max(t => t.Id) + 1;
            if (Utilizadores.Any()) _nextIdUtilizador = Utilizadores.Max(u => u.Id) + 1;
        }

        // CATEGORIAS
   
        public Categoria CriarCategoria(string nome)
        {
            var c = new Categoria(_nextIdCategoria++, nome);

            if (!c.Validar())
                return null;

            Categorias.Add(c);
            _persistencia.GravarCategorias(Categorias);

            return c;
        }

        public bool RemoverCategoria(int id)
        {
            var c = Categorias.FirstOrDefault(x => x.Id == id);
            if (c == null) return false;

            Categorias.Remove(c);
            _persistencia.GravarCategorias(Categorias);
            return true;
        }

        // TRANSAÇÕES
     
        public Transacao CriarTransacao(string descricao, double valor, DateTime data, string tipo, int categoriaId)
        {
            var t = new Transacao(_nextIdTransacao++, descricao, valor, data, tipo, categoriaId);

            if (!t.Validar())
                return null;

            Transacoes.Add(t);
            _persistencia.GravarTransacoes(Transacoes);

            return t;
        }

        public bool RemoverTransacao(int id)
        {
            var t = Transacoes.FirstOrDefault(x => x.Id == id);
            if (t == null) return false;

            Transacoes.Remove(t);
            _persistencia.GravarTransacoes(Transacoes);

            return true;
        }

    
        // UTILIZADORES
  
        public Utilizador CriarUtilizador(string nome, string email, string password, string perfil)
        {
            var u = new Utilizador(_nextIdUtilizador++, nome, email, password, perfil);

            if (!u.Validar())
                return null;

            Utilizadores.Add(u);
            _persistencia.GravarUtilizadores(Utilizadores);

            return u;
        }

        public bool RemoverUtilizador(int id)
        {
            var u = Utilizadores.FirstOrDefault(x => x.Id == id);
            if (u == null) return false;

            Utilizadores.Remove(u);
            _persistencia.GravarUtilizadores(Utilizadores);

            return true;
        }

        // RELATÓRIO / SALDO
     
        public double ObterSaldoAtual()
        {
            double receitas = Transacoes.Where(t => t.Tipo == "Receita").Sum(t => t.Valor);
            double despesas = Transacoes.Where(t => t.Tipo == "Despesa").Sum(t => t.Valor);
            return receitas - despesas;
        }
    }
}