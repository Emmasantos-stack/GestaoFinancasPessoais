using System;

public class MenuPrincipal
{
    public void Mostrar()
    {
        int opcao;
        do
        {
            Console.Clear();
            Console.WriteLine("=========================");
            Console.WriteLine("    MENU PRINCIPAL");
            Console.WriteLine("=========================");
            Console.WriteLine("1 - Gerir Utilizadores");
            Console.WriteLine("2 - Gerir Categorias");
            Console.WriteLine("3 - Gerir Transações");
            Console.WriteLine("4 - Gerar Relatório");
            Console.WriteLine("0 - Sair");
            Console.Write("Opção: ");

            if (!int.TryParse(Console.ReadLine(), out opcao))
                opcao = -1;

            switch (opcao)
            {
                case 1:
                    Console.WriteLine("Abrir gestão de utilizadores...");
                    Console.ReadKey();
                    break;

                case 2:
                    Console.WriteLine("Abrir gestão de categorias...");
                    Console.ReadKey();
                    break;

                case 3:
                    Console.WriteLine("Abrir gestão de transações...");
                    Console.ReadKey();
                    break;

                case 4:
                    Console.WriteLine("Gerar relatório...");
                    Console.ReadKey();
                    break;

                case 0:
                    Console.WriteLine("A sair...");
                    break;

                default:
                    Console.WriteLine("Opção inválida!");
                    Console.ReadKey();
                    break;
            }

        } while (opcao != 0);
    }
}