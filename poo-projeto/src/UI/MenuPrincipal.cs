using SistemaFinanceiro.Services;

namespace SistemaFinanceiro.UI
{
    // Classe responsável pelo menu principal da aplicação. Este menu permite ao utilizador aceder às principais funcionalidades do sistema: gestão de utilizadores, categorias e transações.
    public class MenuPrincipal
    {
        
        private readonly GerirUtilizadores _gerirUtilizadores;
        private readonly GerirTransacoes _gerirTransacoes;
        private readonly GerirCategorias _gerirCategorias;
        // Construtor do MenuPrincipal.
        // <param name="gerirUtilizadores">Serviço de gestão de utilizadores</param>
        // <param name="gerirCategorias">Serviço de gestão de categorias</param>
        // <param name="gerirTransacoes">Serviço de gestão de transações</param>
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
// Método responsável por apresentar o menu principal e gerir a navegação entre os vários menus do sistema.
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
   // Abre o menu de gestão de utilizadores
                    case 1:
                        new MenuGerirUtilizadores(_gerirUtilizadores).Abrir();
                        break;
// Abre o menu de gestão de categorias
                    case 2:
                        new MenuGerirCategorias(_gerirCategorias).Abrir();
                        break;
// Abre o menu de gestão de transações
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
