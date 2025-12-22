using SistemaFinanceiro.Services;

namespace SistemaFinanceiro.UI
{
    // Classe responsável pelo menu principal da aplicação. Este menu permite ao utilizador aceder às principais funcionalidades do sistema: gestão de Utilizador, Categoria e transações.
    public class MenuPrincipal
    {
        private readonly Sistema _sistema;

        private readonly GerirUtilizador _gerirUtilizador;
        private readonly GerirTransacao _gerirTransacao;
        private readonly GerirCategoria _gerirCategoria;
        // Construtor do MenuPrincipal.
        // <param name="gerirUtilizador">Serviço de gestão de Utilizador</param>
        // <param name="gerirCategoria">Serviço de gestão de Categoria</param>
        // <param name="gerirTransacao">Serviço de gestão de transações</param>
        public MenuPrincipal(
    Sistema sistema,
    GerirUtilizador gerirUtilizador,
    GerirCategoria gerirCategoria,
    GerirTransacao gerirTransacao)
{
    _sistema = sistema;
    _gerirUtilizador = gerirUtilizador;
    _gerirCategoria = gerirCategoria;
    _gerirTransacao = gerirTransacao;
}

// Método responsável por apresentar o menu principal e gerir a navegação entre os vários menus do sistema.
        public void Abrir()
{
    int opcao;

    do
    {
        try { Console.Clear(); } catch { }

        Console.WriteLine("===== MENU PRINCIPAL =====");
        Console.WriteLine($"Utilizador: {_sistema.UtilizadorAutenticado?.Nome}");
        Console.WriteLine($"Perfil: {_sistema.UtilizadorAutenticado?.Perfil}");
        Console.WriteLine();

        if (_sistema.UtilizadorAutenticado?.Perfil == "Admin")
        {
            Console.WriteLine("1 - Gerir Utilizadores");
        }

        Console.WriteLine("2 - Gerir Categorias");
        Console.WriteLine("3 - Gerir Transações");
        Console.WriteLine("4 - Relatórios");

        Console.WriteLine("0 - Sair");
        Console.Write("Opção: ");

        int.TryParse(Console.ReadLine(), out opcao);

        switch (opcao)
        {
            case 1:
                if (_sistema.UtilizadorAutenticado?.Perfil == "Admin")
                {
                    new MenuGerirUtilizador(_gerirUtilizador).Abrir();
                }
                else
                {
                    Console.WriteLine("Acesso negado. Apenas administradores.");
                    Console.ReadKey();
                }
                break;

            case 2:
                new MenuGerirCategoria(_gerirCategoria).Abrir();
                break;

            case 3:
                new MenuGerirTransacao(
                    _gerirTransacao,
                    _gerirCategoria
                ).Abrir();
                break;
                case 4:
    var relatorio = new GerarRelatorio(_sistema.Transacao);
    new MenuRelatorio(relatorio).Abrir();
    break;

        }

    } while (opcao != 0);
}



    }
}
