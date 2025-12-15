using System.IO;
using System.Text.Json;
using SistemaFinanceiro.Models;

namespace SistemaFinanceiro.Services
{
    public static class PersistenciaJson
    {
        private const string Ficheiro = "dados.json";

        private class Dados
        {
            public List<Utilizador> Utilizadores { get; set; } = new();
            public List<Categoria> Categoria { get; set; } = new();
            public List<Transacao> Transacoes { get; set; } = new();
        }

        // -------------------------------
        // GUARDAR TUDO
        // -------------------------------
        public static void Guardar(
            List<Utilizador> utilizadores,
            List<Categoria> Categoria,
            List<Transacao> transacoes)
        {
            var dados = new Dados
            {
                Utilizadores = utilizadores,
                Categoria = Categoria,
                Transacoes = transacoes
            };

            var json = JsonSerializer.Serialize(dados, new JsonSerializerOptions
            {
                WriteIndented = true
            });

            File.WriteAllText(Ficheiro, json);
        }

        // -------------------------------
        // CARREGAR TUDO
        // -------------------------------
        public static void Carregar(
            out List<Utilizador> utilizadores,
            out List<Categoria> Categoria,
            out List<Transacao> transacoes)
        {
            if (!File.Exists(Ficheiro))
            {
                utilizadores = new();
                Categoria = new();
                transacoes = new();
                return;
            }

            var json = File.ReadAllText(Ficheiro);
            var dados = JsonSerializer.Deserialize<Dados>(json);

            utilizadores = dados?.Utilizadores ?? new();
            Categoria = dados?.Categoria ?? new();
            transacoes = dados?.Transacoes ?? new();
        }

        internal static void Guarda(List<Utilizador> utilizadores, List<Categoria> Categoria, List<Transacao> transacoes)
        {
            throw new NotImplementedException();
        }
    }
}
