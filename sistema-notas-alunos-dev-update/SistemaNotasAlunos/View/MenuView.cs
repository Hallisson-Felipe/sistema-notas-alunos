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
                ExibirCabecalhos("SISTEMA DE NOTAS DE ALUNOS");
                Console.WriteLine("1. Consultas");
                Console.WriteLine("2. Cadastros");
                Console.WriteLine("3. Salvar");
                Console.WriteLine("4. Sair");
                ExibirSeparador();
                Console.Write("Escolha uma opção: ");

                switch (Console.ReadLine())
                {
                    case "1":
                        Console.WriteLine("Opção 'Consultas' selecionada.");
                        return 1;
                    case "2":
                        Console.WriteLine("Opção 'Cadastros' selecionada.");
                        return 2;
                    case "3":
                        Console.WriteLine("Opção 'Salvar' selecionada.");
                        return 3;
                    case "4":
                        Console.WriteLine("Opção 'Sair' selecionada. Encerrando o programa...");
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


                    //consulta de alunos por nome ou matricula
                    case "1":
                        Console.WriteLine("Opção 'Alunos' selecionada.");
                        Console.WriteLine("entre com o nome ou a matricula do aluno para consultar:");
                        string resp = Console.ReadLine();
                        Aluno aluno = menuController.ac.Buscar(resp, int.Parse(resp));
                        if(aluno == null)
                        {
                            Console.WriteLine("Aluno não encontrado.");
                            Console.ReadKey();
                            ExibirMenuConsultas();
                        }
                        else
                        {
                            Console.WriteLine($"Aluno encontrado: {aluno.Nome}, Matricula: {aluno.Matricula}, Idade: {aluno.Idade}");

                        }
                        break;


                    //consulta de disciplinas por nome ou codigo
                    case "2":
                        Console.WriteLine("Opção 'Disciplinas' selecionada.");
                        Console.WriteLine("entre com o nome ou o código da disciplina para consultar:");
                        string resp2 = Console.ReadLine();
                        Disciplina disciplina = menuController.dc.Buscar(resp2, int.Parse(resp2));
                        if(disciplina == null)
                        {
                            Console.WriteLine("Dispciplina não encontrada.");
                            Console.ReadKey();
                            ExibirMenuConsultas();
                        }


                        break;

                    //consulta de alunos matriculados em uma disciplina
                    case "3":
                        Console.WriteLine("Opção 'Alunos da Disciplina' selecionada.");
                        Console.WriteLine("entre com o nome ou o código da disciplina para consultar os alunos matriculados:");
                        string resp3 = Console.ReadLine();
                        string alunosMatriculados = menuController.mc.AlunosDaDisciplina(resp3, int.Parse(resp3));
                        foreach (var nomeAluno in alunosMatriculados.Split(";"))
                        {
                            Console.WriteLine(nomeAluno);
                        }
                        break;


                    //consulta de disciplinas em que um aluno está matriculado
                    case "4":
                        Console.WriteLine("Opção 'Disciplinas do Aluno' selecionada.");
                        Console.WriteLine("entre com o nome ou a matricula do aluno para consultar as disciplinas em que ele está matriculado:");
                        string resp4 = Console.ReadLine();
                        string disciplinasMatriculadas = menuController.mc.DisciplinasDoAluno(resp4, int.Parse(resp4));
                        foreach (var nomeDisciplina in disciplinasMatriculadas.Split(";"))
                        {
                            Console.WriteLine(nomeDisciplina);
                        }
                        break;


                    case "0":
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
                    case "1":
                        Console.WriteLine("Opção 'Alunos' selecionada.");

                        break;
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


