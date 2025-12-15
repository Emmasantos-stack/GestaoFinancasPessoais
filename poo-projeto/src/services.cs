using System;
using System.Collections.Generic;
using System.Linq;

namespace SistemaFinanceiro
{
    public class Sistema
    {
        // Objeto responsável por gravar e carregar dados em JSON
        private readonly PersistenciaJson _persistencia;

        // Listas principais do sistema (categorias, transações e utilizadores)
        public List<Categoria> Categorias { get; private set; }
        public List<Transacao> Transacoes { get; private set; }
        public List<Utilizador> Utilizadores { get; private set; }

    // Contadores para gerar IDs automáticos
        private int _nextIdCategoria = 1;
        private int _nextIdTransacao = 1;
        private int _nextIdUtilizador = 1;


        // Construtor recebe a camada de persistência e carrega todos os dados
        public Sistema(PersistenciaJson persistencia)
        {
            _persistencia = persistencia;

           // Carregar dados já existentes nos ficheiros JSON
            Categorias = _persistencia.CarregarCategorias();
            Transacoes = _persistencia.CarregarTransacoes();
            Utilizadores = _persistencia.CarregarUtilizadores();

            // Ajustar IDs automáticos para evitar duplicações
            if (Categorias.Any()) _nextIdCategoria = Categorias.Max(c => c.Id) + 1;
            if (Transacoes.Any()) _nextIdTransacao = Transacoes.Max(t => t.Id) + 1;
            if (Utilizadores.Any()) _nextIdUtilizador = Utilizadores.Max(u => u.Id) + 1;
        }

        // CATEGORIAS
   
    // Cria uma nova categoria
        public Categoria CriarCategoria(string nome)
        {
            // Instancia categoria com novo ID
            var c = new Categoria(_nextIdCategoria++, nome);

            // Verifica se os dados são válidos
            if (!c.Validar())
                return null;

            // Adiciona à lista e grava no ficheiro JSON
            Categorias.Add(c);
            _persistencia.GravarCategorias(Categorias);

            return c;
        }

        // Remove uma categoria pelo ID
        public bool RemoverCategoria(int id)
        {
            // Procura categoria correspondente ao ID
            var c = Categorias.FirstOrDefault(x => x.Id == id);
            if (c == null) return false;

            // Remove e guarda atualização
            Categorias.Remove(c);
            _persistencia.GravarCategorias(Categorias);
            return true;
        }

        // TRANSAÇÕES
     
        // Cria uma nova transação
        public Transacao CriarTransacao(string descricao, double valor, DateTime data, string tipo, int categoriaId)
        {
            // Instancia transação com ID automático
            var t = new Transacao(_nextIdTransacao++, descricao, valor, data, tipo, categoriaId);

             // Verifica se os dados são válidos
            if (!t.Validar())
                return null;

             // Adiciona à lista e grava no JSON
            Transacoes.Add(t);
            _persistencia.GravarTransacoes(Transacoes);

            return t;
        }

          // Remove uma transação pelo ID
        public bool RemoverTransacao(int id)
        {
              // Procura transação correspondente
            var t = Transacoes.FirstOrDefault(x => x.Id == id);
            if (t == null) return false;
            
            // Remove da lista e grava
            Transacoes.Remove(t);
            _persistencia.GravarTransacoes(Transacoes);

            return true;
        }

    
        // UTILIZADORES
  
   // Cria um novo utilizador
        public Utilizador CriarUtilizador(string nome, string email, string password, string perfil)
        {
             // Cria objeto com ID automático
            var u = new Utilizador(_nextIdUtilizador++, nome, email, password, perfil);

            // Verifica validade
            if (!u.Validar())
                return null;

            
            // Adiciona e grava
            Utilizadores.Add(u);
            _persistencia.GravarUtilizadores(Utilizadores);

            return u;
        }
        // Remove um utilizador pelo ID
        public bool RemoverUtilizador(int id)
        {
             // Procura utilizador
            var u = Utilizadores.FirstOrDefault(x => x.Id == id);
            if (u == null) return false;

              // Remove e grava
            Utilizadores.Remove(u);
            _persistencia.GravarUtilizadores(Utilizadores);

            return true;
        }

        // RELATÓRIO / SALDO
     
      // Calcula o saldo atual (receitas - despesas)
        public double ObterSaldoAtual()
        {
             // Soma das receitas
            double receitas = Transacoes.Where(t => t.Tipo == "Receita").Sum(t => t.Valor);
              // Soma das despesas
            double despesas = Transacoes.Where(t => t.Tipo == "Despesa").Sum(t => t.Valor);
            // Retorno do saldo
            return receitas - despesas;
        }
    }
}