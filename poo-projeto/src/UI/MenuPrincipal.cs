using SistemaFinanceiro.Services;

namespace SistemaFinanceiro.UI
{
    public class MenuPrincipal
    {
        private readonly GerirUtilizadores _gerirUtilizadores;
        private readonly GerirTransacoes _gerirTransacoes;
        private readonly GerirCategorias _gerirCategorias;

        public MenuPrincipal(
            GerirUtilizadores gerirUtilizadores,
            GerirCategorias gerirCategorias,
            GerirTransacoes gerirTransacoes
            )
        {
            _gerirUtilizadores = gerirUtilizadores;

            _gerirCategorias = gerirCategorias;
            _gerirTransacoes = gerirTransacoes;
        }

        public void Abrir()
        {
            int opcao;

            do
            {
                try { Console.Clear(); } catch { }

                Console.WriteLine("===== MENU PRINCIPAL =====");
                Console.WriteLine("1 - Gerir Utilizadores");
                Console.WriteLine("2 - Gerir Categorias");
                Console.WriteLine("3 - Gerir Transações");
                Console.WriteLine("0 - Sair");
                Console.Write("Opção: ");

                int.TryParse(Console.ReadLine(), out opcao);

                switch (opcao)
                {
                    case 1:
                        new MenuGerirUtilizadores(_gerirUtilizadores).Abrir();
                        break;

                    case 2:
                        new MenuGerirCategorias(_gerirCategorias).Abrir();
                        break;

                    case 3:
                        new MenuGerirTransacoes(
                            _gerirTransacoes,
                            _gerirCategorias
                        ).Abrir();
                        break;
                }

            } while (opcao != 0);
        }

    }
}
