using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace SistemaFinanceiro
{
    public class PersistenciaJson
    {
        private readonly string _pasta;

        private readonly string _ficheiroCategorias;
        private readonly string _ficheiroTransacoes;
        private readonly string _ficheiroUtilizadores;

        public PersistenciaJson(string pasta)
        {
            _pasta = pasta;

            // Criar pasta se não existir
            if (!Directory.Exists(_pasta))
                Directory.CreateDirectory(_pasta);

            _ficheiroCategorias = Path.Combine(_pasta, "categorias.json");
            _ficheiroTransacoes = Path.Combine(_pasta, "transacoes.json");
            _ficheiroUtilizadores = Path.Combine(_pasta, "utilizadores.json");
        }

        // =============================
        // FUNÇÕES AUXILIARES
        // =============================
        private static JsonSerializerOptions Options => new JsonSerializerOptions
        {
            WriteIndented = true
        };

        private static List<T> LerLista<T>(string ficheiro)
        {
            try
            {
                if (!File.Exists(ficheiro))
                    return new List<T>();

                string json = File.ReadAllText(ficheiro);
                return JsonSerializer.Deserialize<List<T>>(json, Options) ?? new List<T>();
            }
            catch
            {
                return new List<T>();
            }
        }

        private static void GravarLista<T>(string ficheiro, List<T> lista)
        {
            try
            {
                string json = JsonSerializer.Serialize(lista, Options);
                File.WriteAllText(ficheiro, json);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro ao gravar JSON: " + ex.Message);
            }
        }

        // =============================
        // CATEGORIAS
        // =============================
        public List<Categoria> CarregarCategorias() =>
            LerLista<Categoria>(_ficheiroCategorias);

        public void GravarCategorias(List<Categoria> categorias) =>
            GravarLista(_ficheiroCategorias, categorias);

        // =============================
        // TRANSAÇÕES
        // =============================
        public List<Transacao> CarregarTransacoes() =>
            LerLista<Transacao>(_ficheiroTransacoes);

        public void GravarTransacoes(List<Transacao> transacoes) =>
            GravarLista(_ficheiroTransacoes, transacoes);

        // =============================
        // UTILIZADORES
        // =============================
        public List<Utilizador> CarregarUtilizadores() =>
            LerLista<Utilizador>(_ficheiroUtilizadores);

        public void GravarUtilizadores(List<Utilizador> utilizadores) =>
            GravarLista(_ficheiroUtilizadores, utilizadores);
    }
}
