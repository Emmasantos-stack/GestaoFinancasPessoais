using System.Text.Json;

namespace SistemaFinanceiro
{
    public static class Persistencia
    {
        private const string Ficheiro = "dados.json";

        public static void Guardar(Utilizador utilizador)
        {
            var json = JsonSerializer.Serialize(utilizador, new JsonSerializerOptions
            {
                WriteIndented = true
            });

            File.WriteAllText(Ficheiro, json);
        }

        public static Utilizador? Carregar()
        {
            if (!File.Exists(Ficheiro))
                return null;

            string json = File.ReadAllText(Ficheiro);
            return JsonSerializer.Deserialize<Utilizador>(json);
        }
    }
}
