using System;

namespace SistemaFinanceiro
{
    public class Categoria
    {
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;

        public Categoria() { }

        public Categoria(int id, string nome)
        {
            Id = id;
            Nome = nome;
        }

        public bool Validar(out string erro)
        {
            erro = string.Empty;
            if (string.IsNullOrWhiteSpace(Nome))
            {
                erro = "Nome da categoria inválido.";
                return false;
            }
            return true;
        }

        public override string ToString() => $"[{Id}] {Nome}";
    }
}
