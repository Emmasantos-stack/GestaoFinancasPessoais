using SistemaFinanceiro.Services;
using SistemaFinanceiro.UI;

namespace SistemaFinanceiro
{
    internal class Program
    {
        static void Main()
        {
            // Sistema central
            var sistema = new Sistema();

            // Serviços
            var gerirUtilizadores = new GerirUtilizadores(sistema);
            var gerirCategoria = new GerirCategoria(sistema);
            var gerirTransacoes = new GerirTransacoes(sistema);

            // Menu principal
            var menu = new MenuPrincipal(
                gerirUtilizadores,
                gerirCategoria,
                gerirTransacoes
            );

            // Arranque
            menu.Abrir();
        }
    }
}
