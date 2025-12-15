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
            public List<Categoria> Categorias { get; set; } = new();
            public List<Transacao> Transacoes { get; set; } = new();
        }

        // -------------------------------
        // GUARDAR TUDO
        // -------------------------------
        public static void Guardar(
            List<Utilizador> utilizadores,
            List<Categoria> categorias,
            List<Transacao> transacoes)
        {
            var dados = new Dados
            {
                Utilizadores = utilizadores,
                Categorias = categorias,
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
            out List<Categoria> categorias,
            out List<Transacao> transacoes)
        {
            if (!File.Exists(Ficheiro))
            {
                utilizadores = new();
                categorias = new();
                transacoes = new();
                return;
            }

            var json = File.ReadAllText(Ficheiro);
            var dados = JsonSerializer.Deserialize<Dados>(json);

            utilizadores = dados?.Utilizadores ?? new();
            categorias = dados?.Categorias ?? new();
            transacoes = dados?.Transacoes ?? new();
        }
    }
}
