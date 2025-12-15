using System;
using System.Collections.Generic;
using System.Linq;
using SistemaFinanceiro.Models;

namespace SistemaFinanceiro.Services
{
    // Classe responsável por gerir todas as operações relacionadas com transações
    public class GerirTransacoes
    {
        // Referência ao sistema principal onde os dados estão guardados
        private readonly Sistema _sistema;

        // Construtor que recebe o sistema principal por injeção de dependência
        public GerirTransacoes(Sistema sistema)
        {
            // Guarda a referência para ser usada nos métodos da classe
            _sistema = sistema;
        }

        // Método que devolve a lista completa de transações existentes
        public List<Transacao> ObterTransacoes() => _sistema.Transacoes;

        // Método responsável por criar uma nova transação
        public Transacao CriarTransacao(string desc, double valor, DateTime data, TipoTransacao tipo, int? catId)
        {
            // Define o ID automaticamente
            // Se já existirem transações, usa o maior ID + 1
            // Caso contrário, começa com ID = 1
            int id = _sistema.Transacoes.Any() ? _sistema.Transacoes.Max(t => t.Id) + 1 : 1;
            
            // Cria um novo objeto Transacao com os dados recebidos
            var t = new Transacao(id, desc, valor, data, tipo, catId);
            
            // Adiciona a transação à lista do sistema
            _sistema.Transacoes.Add(t);
           
           // Guarda todas as alterações no ficheiro de persistência
            _sistema.SalvarTudo();
           
           // Devolve a transação criada
            return t;
        }

        // Método responsável por remover uma transação através do ID
        public bool RemoverTransacao(int id)
        {
            // Procura a transação com o ID indicado
            var t = _sistema.Transacoes.FirstOrDefault(x => x.Id == id);
            
            // Se não existir, devolve false
            if (t == null) return false;

            // Remove a transação da lista
            _sistema.Transacoes.Remove(t);

            // Guarda as alterações
            _sistema.SalvarTudo();

            // Indica que a remoção foi feita com sucesso
            return true;
        }

        // Método que calcula o saldo atual do sistema
        public double ObterSaldoAtual()
        {
            // Soma todos os valores das transações do tipo Receita
            var r = _sistema.Transacoes.Where(t => t.Tipo == TipoTransacao.Receita).Sum(t => t.Valor);
            
            // Soma todos os valores das transações do tipo Despesa
            var d = _sistema.Transacoes.Where(t => t.Tipo == TipoTransacao.Despesa).Sum(t => t.Valor);
            
            // O saldo é a diferença entre receitas e despesas
            return r - d;
        }

    // Método responsável por editar uma transação existente
    public bool EditarTransacao(
       int id,
       string descricao,
       double valor,
       DateTime data,
       TipoTransacao tipo,
       int? categoriaId)
{
    // Procura a transação pelo ID
    var t = _sistema.Transacoes.FirstOrDefault(x => x.Id == id);

    // Se não existir, devolve false
    if (t == null)
        return false;

    // Atualiza os dados da transação
    t.Descricao = descricao;
    t.Valor = valor;
    t.Data = data;
    t.Tipo = tipo;
    t.CategoriaId = categoriaId;

    // Guarda as alterações feitas
    _sistema.SalvarTudo();

    // Indica que a edição foi feita com sucesso
    return true;
}

    }
}