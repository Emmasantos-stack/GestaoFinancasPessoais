using SistemaFinanceiro.Services;

namespace SistemaFinanceiro.UI
{
    // Classe responsável pelo menu principal da aplicação. Este menu permite ao utilizador aceder às principais funcionalidades do sistema: gestão de utilizadores, Categoria e transações.
    public class MenuPrincipal
    {
        
        private readonly GerirUtilizadores _gerirUtilizadores;
        private readonly GerirTransacoes _gerirTransacoes;
        private readonly GerirCategoria _gerirCategoria;
        // Construtor do MenuPrincipal.
        // <param name="gerirUtilizadores">Serviço de gestão de utilizadores</param>
        // <param name="gerirCategoria">Serviço de gestão de Categoria</param>
        // <param name="gerirTransacoes">Serviço de gestão de transações</param>
        public MenuPrincipal(
            GerirUtilizadores gerirUtilizadores,
            GerirCategoria gerirCategoria,
            GerirTransacoes gerirTransacoes
            )
        {
            _gerirUtilizadores = gerirUtilizadores;

            _gerirCategoria = gerirCategoria;
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
                Console.WriteLine("2 - Gerir Categoria");
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
// Abre o menu de gestão de Categoria
                    case 2:
                        new MenuGerirCategoria(_gerirCategoria).Abrir();
                        break;
// Abre o menu de gestão de transações
                    case 3:
                        new MenuGerirTransacoes(
                            _gerirTransacoes,
                            _gerirCategoria
                        ).Abrir();
                        break;
                }

            } while (opcao != 0);
        }

    }
}
