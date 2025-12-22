using System.IO;
using System.Text.Json;
using SistemaFinanceiro.Models;

namespace SistemaFinanceiro.Services
{
    // Classe estática responsável pela persistência de dados em formato JSON.
    // Permite guardar e carregar utilizadores, categorias e transações a partir de um ficheiro.
    public static class PersistenciaJson
    {
        // Nome do ficheiro onde os dados são armazenados.
        private const string Ficheiro = "dados.json";

        // Classe interna usada apenas para serialização e desserialização de todos os dados do sistema.
        private class Dados
        {
            public List<Utilizador> Utilizadores { get; set; } = new();
            public List<Categoria> Categoria { get; set; } = new();
            public List<Transacao> Transacao { get; set; } = new();
        }

        // GUARDAR DADOS---------------------------------------
        // Guarda todas as listas do sistema num ficheiro JSON.

        public static void Guardar(
            List<Utilizador> utilizadores,
            List<Categoria> Categoria,
            List<Transacao> Transacao)
        {
            // Cria o objeto auxiliar com todos os dados
            var dados = new Dados
            {
                Utilizadores = utilizadores,
                Categoria = Categoria,
                Transacao = Transacao
            };

            // Serializa os dados para JSON formatado
            var json = JsonSerializer.Serialize(dados, new JsonSerializerOptions
            {
                WriteIndented = true
            });

            // Escreve o conteúdo no ficheiro
            File.WriteAllText(Ficheiro, json);
        }


        // CARREGAR DADOS-------------------------------------------------
        // Carrega os dados do ficheiro JSON para as listas do sistema.  Se o ficheiro não existir, inicializa listas vazias.
        public static void Carregar(
            out List<Utilizador> utilizadores,
            out List<Categoria> Categoria,
            out List<Transacao> Transacao)
        {
            // Caso o ficheiro ainda não exista
            if (!File.Exists(Ficheiro))
            {
                utilizadores = new();
                Categoria = new();
                Transacao = new();
                return;
            }

            // Lê o conteúdo do ficheiro
            var json = File.ReadAllText(Ficheiro);

            // Desserializa os dados
            var dados = JsonSerializer.Deserialize<Dados>(json);

            // Garante que nunca devolve null
            utilizadores = dados?.Utilizadores ?? new();
            Categoria = dados?.Categoria ?? new();
            Transacao = dados?.Transacao ?? new();
        }
    }
}