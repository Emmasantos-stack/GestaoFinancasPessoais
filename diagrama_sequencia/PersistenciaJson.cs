using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Threading;

namespace SistemaFinanceiro
{
    public class PersistenciaJson
    {
        private readonly string dataDir;
        private readonly JsonSerializerOptions options = new JsonSerializerOptions { WriteIndented = true };
        private readonly object locker = new object();

        public PersistenciaJson(string dataDir = "data")
        {
            this.dataDir = dataDir;
            Directory.CreateDirectory(dataDir);
        }

        private string PathFor(string filename) => Path.Combine(dataDir, filename);

        public List<T> LoadList<T>(string filename)
        {
            var path = PathFor(filename);
            lock (locker)
            {
                if (!File.Exists(path)) return new List<T>();
                var txt = File.ReadAllText(path);
                if (string.IsNullOrWhiteSpace(txt)) return new List<T>();
                return JsonSerializer.Deserialize<List<T>>(txt, options) ?? new List<T>();
            }
        }

        public void SaveList<T>(string filename, List<T> list)
        {
            var path = PathFor(filename);
            var tmp = path + ".tmp";
            lock (locker)
            {
                var txt = JsonSerializer.Serialize(list, options);
                File.WriteAllText(tmp, txt);
                File.Copy(tmp, path, true);
                File.Delete(tmp);
            }
        }
    }
}
