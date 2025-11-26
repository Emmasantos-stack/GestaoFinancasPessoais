using System;
using System.Globalization;

namespace SistemaFinanceiro
{
    class Program
    {
        static void Main(string[] args)
        {
            var persist = new PersistenciaJson("data");
            var sistema = new Sistema(persist);

            Console.WriteLine("=== Sistema Financeiro (Console) ===");

            bool sair = false;
            while (!sair)
            {
                Console.WriteLine("\nMenu:");
                Console.WriteLine("1) Listar categorias");
                Console.WriteLine("2) Criar categoria");
                Console.WriteLine("3) Listar transações");
                Console.WriteLine("4) Criar transação");
                Console.WriteLine("5) Remover transação");
                Console.WriteLine("6) Ver saldo atual");
                Console.WriteLine("7) Criar utilizador");
                Console.WriteLine("8) Listar utilizadores");
                Console.WriteLine("9) Salvar & Sair");
                Console.Write("Escolha: ");

                var opt = Console.ReadLine();
                try
                {
                    switch (opt)
                    {
                        case "1":
                            foreach (var c in sistema.Categorias) Console.WriteLine(c);
                            break;

                        case "2":
                            Console.Write("Nome da categoria: ");
                            var nome = Console.ReadLine() ?? "";
                            var cat = sistema.CriarCategoria(nome);
                            Console.WriteLine("Categoria criada: " + cat);
                            break;

                        case "3":
                            foreach (var t in sistema.Transacoes) Console.WriteLine(t);
                            break;

                        case "4":
                            Console.Write("Descrição: ");
                            var d = Console.ReadLine() ?? "";
                            Console.Write("Valor (ex: 12.34): ");
                            var sValor = Console.ReadLine() ?? "0";
                            if (!decimal.TryParse(sValor, NumberStyles.Any, CultureInfo.InvariantCulture, out var val))
                            {
                                Console.WriteLine("Valor inválido.");
                                break;
                            }
                            Console.Write("Data (yyyy-mm-dd) ou Enter p/ hoje: ");
                            var sData = Console.ReadLine();
                            var data = string.IsNullOrWhiteSpace(sData) ? DateTime.Today : DateTime.Parse(sData);
                            Console.Write("Tipo (Receita/Despesa): ");
                            var tipo = Console.ReadLine() ?? "Despesa";
                            Console.Write("CategoriaId (opcional) ou Enter: ");
                            var sCatId = Console.ReadLine();
                            int? catId = null;
                            if (!string.IsNullOrWhiteSpace(sCatId) && int.TryParse(sCatId, out var tmp)) catId = tmp;

                            var tNova = sistema.CriarTransacao(d, val, data, tipo, catId);
                            Console.WriteLine("Transação criada: " + tNova);
                            break;

                        case "5":
                            Console.Write("Id da transação a remover: ");
                            if (int.TryParse(Console.ReadLine(), out var idRem))
                            {
                                sistema.RemoverTransacao(idRem);
                                Console.WriteLine("Removido.");
                            }
                            else Console.WriteLine("Id inválido.");
                            break;

                        case "6":
                            var saldo = sistema.ObterSaldoAtual();
                            Console.WriteLine($"Saldo atual: {saldo:N2} €");
                            break;

                        case "7":
                            Console.Write("Nome: ");
                            var unome = Console.ReadLine() ?? "";
                            Console.Write("Email: ");
                            var uemail = Console.ReadLine() ?? "";
                            Console.Write("Password: ");
                            var upass = Console.ReadLine() ?? "";
                            var u = sistema.CriarUtilizador(unome, uemail, upass);
                            Console.WriteLine("Utilizador criado: " + u);
                            break;

                        case "8":
                            foreach (var ulist in sistema.Utilizadores) Console.WriteLine(ulist);
                            break;

                        case "9":
                            sistema.SalvarTudo();
                            Console.WriteLine("Dados salvos. Até logo!");
                            sair = true;
                            break;

                        default:
                            Console.WriteLine("Opção inválida.");
                            break;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Erro: " + ex.Message);
                }
            }
        }
    }
}
