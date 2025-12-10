namespace poo_projeto.Models
{
    public class Produto
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public double Preco { get; set; }
        public int Stock { get; set; }

        public Produto(int id, string nome, double preco, int stock)
        {
            Id = id;
            Nome = nome;
            Preco = preco;
            Stock = stock;
        }
    }
}
