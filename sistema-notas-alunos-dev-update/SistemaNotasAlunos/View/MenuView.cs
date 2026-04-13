using SistemaNotasAlunos.Controller;
using SistemaNotasAlunos.View;
using System;
using SistemaNotasAlunos.Model;

namespace SistemaNotasAlunos.View
{
    public static class MenuView
    {
        static MenuController menuController = new MenuController();

        public static int ExibirMenuPrincipal()
        {
            while (true)
            {
                Console.Clear();
                ExibirCabecalho("SISTEMA DE NOTAS DE ALUNOS");
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
                    case "4":
                        Console.WriteLine("Encerrando o programa...");
                        return 4;
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
                ExibirCabecalho("CONSULTAS");
                Console.WriteLine("  1. Alunos");
                Console.WriteLine("  2. Disciplinas");
                Console.WriteLine("  3. Alunos da Disciplina");
                Console.WriteLine("  4. Disciplinas do Aluno");
                Console.WriteLine("  0. Voltar");
                ExibirSeparador();
                Console.Write("Escolha uma opção: ");

                switch (Console.ReadLine())
                {
                    case "1":
                        Console.Write("Nome ou matrícula do aluno: ");
                        string resp = Console.ReadLine();
                        // CORREÇÃO 4: int.Parse substituído por TryParse — evita FormatException
                        // quando o usuário digita um nome em vez de número.
                        int matBusca = int.TryParse(resp, out int mb) ? mb : -1;
                        Aluno aluno = menuController.ac.Buscar(resp, matBusca);
                        if (aluno == null)
                        {
                            Console.WriteLine("Aluno não encontrado.");
                        }
                        else
                        {
                            Console.WriteLine($"Aluno: {aluno.Nome} | Matrícula: {aluno.Matricula} | Idade: {aluno.Idade}");
                        }
                        Console.ReadKey();
                        continue;

                    case "2":
                        Console.Write("Nome ou código da disciplina: ");
                        string resp2 = Console.ReadLine();
                        int codBusca = int.TryParse(resp2, out int cb) ? cb : -1;
                        Disciplina disciplina = menuController.dc.Buscar(resp2, codBusca);
                        if (disciplina == null)
                        {
                            Console.WriteLine("Disciplina não encontrada.");
                        }
                        else
                        {
                            Console.WriteLine($"Disciplina: {disciplina.Nome} | Código: {disciplina.Codigo} | Nota Mínima: {disciplina.NotaMinima}");
                        }
                        Console.ReadKey();
                        break;

                    case "3":
                        Console.Write("Nome ou código da disciplina: ");
                        string resp3 = Console.ReadLine();
                        int codDisc3 = int.TryParse(resp3, out int cd3) ? cd3 : -1;
                        Console.WriteLine(menuController.mc.AlunosDaDisciplina(resp3, codDisc3));
                        Console.ReadKey();
                        break;

                    case "4":
                        Console.Write("Nome ou matrícula do aluno: ");
                        string resp4 = Console.ReadLine();
                        int matAluno4 = int.TryParse(resp4, out int ma4) ? ma4 : -1;
                        string disciplinas = menuController.mc.DisciplinasDoAluno(resp4, matAluno4);
                        foreach (var nome in disciplinas.Split(';', StringSplitOptions.RemoveEmptyEntries))
                            Console.WriteLine(nome);
                        Console.ReadKey();
                        break;

                    case "0":
                        // CORREÇÃO 5: estava chamando ExibirMenuConsultas() (a si mesmo) em vez de ExibirMenuPrincipal().
                        ExibirMenuPrincipal();
                        break;

                    default:
                        Console.WriteLine("Opção inválida. Digite um número entre 0 e 4.");
                        Console.ReadKey();
                        break;
                }
            }
        }

        public static int ExibirMenuCadastros()
        {
            while (true)
            {
                Console.Clear();
                ExibirCabecalho("CADASTROS");
                Console.WriteLine("  1. Alunos");
                Console.WriteLine("  2. Disciplinas");
                Console.WriteLine("  3. Matrículas");
                Console.WriteLine("  4. Atribuir Nota a Aluno");
                Console.WriteLine("  0. Voltar");
                ExibirSeparador();
                Console.Write("Escolha uma opção: ");

                switch (Console.ReadLine())
                {
                    case "1":
                        Console.Write("Nome do aluno: ");
                        string nomeAluno = Console.ReadLine();
                        Console.Write("Idade do aluno: ");
                        if (!int.TryParse(Console.ReadLine(), out int idadeAluno))
                        {
                            Console.WriteLine("Idade inválida.");
                            Console.ReadKey();
                            break;
                        }
                        menuController.ac.Cadastrar(nomeAluno, idadeAluno);
                        Console.WriteLine("Aluno cadastrado com sucesso!");
                        Console.ReadKey();
                        break;

                    case "2":
                        Console.Write("Nome da disciplina: ");
                        string nomeDisciplina = Console.ReadLine();
                        // CORREÇÃO 6: perguntava "código" e passava int, mas Cadastro espera
                        // notaMinima (double) como segundo parâmetro.
                        Console.Write("Nota mínima para aprovação: ");
                        if (!double.TryParse(Console.ReadLine(), out double notaMinima))
                        {
                            Console.WriteLine("Nota inválida.");
                            Console.ReadKey();
                            break;
                        }
                        menuController.dc.Cadastro(nomeDisciplina, notaMinima);
                        Console.WriteLine("Disciplina cadastrada com sucesso!");
                        Console.ReadKey();
                        break;

                    case "3":
                        Console.Write("Nome ou matrícula do aluno: ");
                        string respAluno = Console.ReadLine();
                        Console.Write("Nome ou código da disciplina: ");
                        string respDisc = Console.ReadLine();
                        int matAluno3 = int.TryParse(respAluno, out int ma3) ? ma3 : -1;
                        int codDisc3b = int.TryParse(respDisc, out int cd3b) ? cd3b : -1;

                        // CORREÇÃO 7: Cadastro era chamado 3 vezes (1 antes dos ifs + 2 dentro),
                        // inserindo a matrícula em triplicata. Agora é chamado apenas uma vez.
                        string resultado = menuController.mc.Cadastro(respAluno, matAluno3, respDisc, codDisc3b);

                        if (resultado == "Aluno não encontrado.")
                        {
                            Console.WriteLine("Aluno não encontrado. Matrícula não realizada.");
                        }
                        // CORREÇÃO 8: "else if (Cadastro(...))" comparava string com bool (CS0029).
                        else if (resultado == "Disciplina não encontrada.")
                        {
                            Console.WriteLine("Disciplina não encontrada. Matrícula não realizada.");
                        }
                        else
                        {
                            Console.WriteLine("Matrícula realizada com sucesso!");
                        }
                        Console.ReadKey();
                        break;

                    case "4":
                        Console.Write("Nome ou matrícula do aluno: ");
                        string alunoNota = Console.ReadLine();
                        Console.Write("Nome ou código da disciplina: ");
                        string discNota = Console.ReadLine();
                        Console.Write("Nota 1: ");
                        if (!double.TryParse(Console.ReadLine(), out double nota1))
                        {
                            Console.WriteLine("Nota 1 inválida.");
                            Console.ReadKey();
                            break;
                        }
                        Console.Write("Nota 2: ");
                        if (!double.TryParse(Console.ReadLine(), out double nota2))
                        {
                            Console.WriteLine("Nota 2 inválida.");
                            Console.ReadKey();
                            break;
                        }

                        int matNota = int.TryParse(alunoNota, out int mn) ? mn : -1;
                        int codNota = int.TryParse(discNota, out int cn) ? cn : -1;

                        // CORREÇÃO 9: chamada passava objetos Aluno/Disciplina onde o método
                        // espera string/int (CS1503). Agora passa strings e ints corretamente.
                        bool atribuido = menuController.mc.AtribuirNota(alunoNota, matNota, discNota, codNota, nota1, nota2);
                        Console.WriteLine(atribuido ? "Nota atribuída com sucesso!" : "Aluno ou disciplina não encontrados.");
                        Console.ReadKey();
                        break;

                    case "0":
                        menuController.MostrarSubMenu();
                        return 0;

                    default:
                        Console.WriteLine("Opção inválida. Digite um número entre 0 e 4.");
                        Console.ReadKey();
                        break;
                }
            }
        }

        internal static void ExibirCabecalho(string titulo)
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
