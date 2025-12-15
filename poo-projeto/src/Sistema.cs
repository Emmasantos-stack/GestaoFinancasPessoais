using SistemaFinanceiro.Models;

namespace SistemaFinanceiro.Services
{
    public class Sistema
    {
        public List<Utilizador> Utilizadores { get; private set; }
        public List<Categoria> Categorias { get; private set; }
        public List<Transacao> Transacoes { get; private set; }

        public Sistema()
        {
            PersistenciaJson.Carregar(
                out var utilizadores,
                out var categorias,
                out var transacoes
            );

            Utilizadores = utilizadores;
            Categorias = categorias;
            Transacoes = transacoes;
        }

        public void SalvarTudo()
        {
            PersistenciaJson.Guardar(
                Utilizadores,
                Categorias,
                Transacoes
            );
        }
    }
}
