using SistemaNotasAlunos.Views;
using System;

namespace SistemaNotasAlunos.Views
{
    /// Exibe os menus de navegação e retorna a opção escolhida pelo usuário.
    public static class MenuView
    {
        // ── Menus ─────────────────────────────────────────────────────────────
        public static int ExibirMenuPrincipal()
        {
            while (true)
            {
                Console.Clear();
                ExibirCabecalhos("SISTEMA DE NOTAS DE ALUNOS");
                Console.WriteLine("1. Consultas");
                Console.WriteLine("2. Cadastros");
                Console.WriteLine("3. Salvar");
                Console.WriteLine("4. Sair");
                ExibirSeparador();
                Console.Write("Escolha uma opção: ");

                switch (Console.ReadLine())
                {
                    case "1": return 1;
                    case "2": return 2;
                    case "3": return 3;
                    case "4": return 4;
                    default:
                        Console.WriteLine("Opção inválida. Digite um número entre 1 e 4.");
                        Console.ReadKey();
                        break;
                }
            }
        }

        public static int ExibirMenuConsultas()
        {
            while (true)
            {
                Console.Clear();
                ExibirCabecalhos("CONSULTAS");
                Console.WriteLine("  1. Alunos");
                Console.WriteLine("  2. Disciplinas");
                Console.WriteLine("  3. Alunos da Disciplina");
                Console.WriteLine("  4. Disciplinas do Aluno");
                Console.WriteLine("  0. Voltar");
                ExibirSeparador();
                Console.Write("Escolha uma opção: ");

                switch (Console.ReadLine())
                {
                    case "1": return 1;
                    case "2": return 2;
                    case "3": return 3;
                    case "4": return 4;
                    case "0": return 0;
                    default:
                        Console.WriteLine("Opção inválida. Digite um número entre 0 e 4.");
                        Console.ReadKey();
                        break;
                }
            }
        }

        /// <summary>Exibe o submenu de cadastros e retorna a opção escolhida (0–4).</summary>
        public static int ExibirMenuCadastros()
        {
            while (true)
            {
                Console.Clear();
                ExibirCabecalhos("CADASTROS");
                Console.WriteLine("  1. Alunos");
                Console.WriteLine("  2. Disciplinas");
                Console.WriteLine("  3. Matrículas");
                Console.WriteLine("  4. Atribuir Nota a Aluno");
                Console.WriteLine("  0. Voltar");
                ExibirSeparador();
                Console.Write("Escolha uma opção: ");

                switch (Console.ReadLine())
                {
                    case "1": return 1;
                    case "2": return 2;
                    case "3": return 3;
                    case "4": return 4;
                    case "0": return 0;
                    default:
                        Console.WriteLine("Opção inválida. Digite um número entre 0 e 4.");
                        Console.ReadKey();
                        break;
                }
            }
        }

        // ── Salvar / Sair ─────────────────────────────────────────────────────

        /// <summary>Confirma que os dados foram salvos nos arquivos .dat.</summary>
        public static void ExibirConfirmacaoSalvamento()
        {
            Console.WriteLine("Dados salvos nos arquivos .dat com sucesso.");
            Console.ReadKey();
        }

        /// <summary>Exibe mensagem de encerramento do sistema.</summary>
        public static void ExibirMensagemSaida()
        {
            Console.Clear();
            ExibirCabecalhos("SAINDO DO SISTEMA");
            Console.WriteLine("Dados salvos. Até logo!");
            Console.WriteLine();
        }

        // ── Helpers internos (usados pelas outras Views via internal) ─────────

        internal static void ExibirCabecalhos(string titulo)
        {
            ExibirSeparador();
            Console.WriteLine($"  {titulo}");
            ExibirSeparador();
        }

        internal static void ExibirSeparador()
        {
            Console.WriteLine(new string('─', 60));
        }
    }
}
class Program
{
    static void Main()
    {
        MenuView.ExibirCabecalhos("TESTE DE MENUS");
        int opcao = MenuView.ExibirMenuPrincipal();
        Console.WriteLine($"Opção escolhida: {opcao}");

        // Testa submenu de consultas
        int consulta = MenuView.ExibirMenuConsultas();
        Console.WriteLine($"Consulta escolhida: {consulta}");

        // Testa confirmação de salvamento
        MenuView.ExibirConfirmacaoSalvamento();

        // Testa mensagem de saída
        MenuView.ExibirMensagemSaida();
    }
}

