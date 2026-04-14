using SistemaNotasAlunos.Controller;
using SistemaNotasAlunos.View;
using System;
using SistemaNotasAlunos.Model;

namespace SistemaNotasAlunos.View
{
    public static class MenuView
    {
        
        //exibe o menu principal e retorna a escolha feita pelo usuario
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

        //exibe o menu de consultas
        public static int ExibirMenuConsultas(MenuController menuController)
        {
            while (true)
            {
                //pergunta ao usuario o que ele quer consultar
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
                    //busca o aluno pelo o nome ou matricula atraves do metodo "Buscar" de AlunoController
                    case "1":
                        Console.Write("Nome ou matrícula do aluno: ");
                        string resp = Console.ReadLine();
                        int matBusca = -1;
                        if (int.TryParse(resp, out int mb))
                        {
                            matBusca = mb;
                        }
                        Aluno aluno = menuController.alunoController.Buscar(resp, matBusca);
                        if (aluno == null)
                        {
                            Console.WriteLine("Aluno não encontrado.");
                        }
                        else
                        {
                            Console.WriteLine($"Aluno: {aluno.Nome} | Matrícula: {aluno.Matricula} | Idade: {aluno.Idade}");
                        }
                        Console.ReadKey();
                        break;

                    //busca a disciplina pelo o nome ou codigo atraves do metodo "Buscar" de DisciplinaController
                    case "2":
                        Console.Write("Nome ou código da disciplina: ");
                        string resp2 = Console.ReadLine();
                        int codBusca = -1;

                        if (int.TryParse(resp2, out int cb))
                        {
                            codBusca = cb;
                        }
                        Disciplina disciplina = menuController.DisciplinaController.Buscar(resp2, codBusca);
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

                    //consulta os alunos matriculados em uma disciplina pelo nome ou codigo da disciplina
                    //atraves do metodo "AlunosDaDisciplina" de MatriculaController
                    case "3":
                        Console.Write("Nome ou código da disciplina: ");
                        string resp3 = Console.ReadLine();
                        int codDisc = -1;
                        if(int.TryParse(resp3, out int cd))
                        {
                            codDisc = cd;
                        }
                        Console.WriteLine(menuController.matriculaController.AlunosDaDisciplina(resp3, codDisc));
                        Console.ReadKey();
                        break;

                    //consulta as disciplinas em que o aluno esta matriculado pelo nome ou matricula do aluno
                    //atraves do metodo "DisciplinasDoAluno"
                    case "4":
                        Console.Write("Nome ou matrícula do aluno: ");
                        string resp4 = Console.ReadLine();
                        int matAluno4 = -1;
                        if (int.TryParse(resp4, out int ma4))
                        {
                            matAluno4 = ma4;
                        }
                        string disciplinas = menuController.matriculaController.DisciplinasDoAluno(resp4, matAluno4);
                        foreach (var nome in disciplinas.Split(';', StringSplitOptions.RemoveEmptyEntries))
                            Console.WriteLine(nome);
                        Console.ReadKey();
                        break;

                    //caso o usuario escolha "0", volta ao menu principal
                    case "0":
                        return 0;

                    //mensagem padrao caso o usuario digite uma opcao que nao existe
                    default:
                        Console.WriteLine("Opção inválida. Digite um número entre 0 e 4.");
                        Console.ReadKey();
                        break;
                }
            }
        }

        public static int ExibirMenuCadastros(MenuController menuController)
        {
            while (true)
            {
                //pergunta ao usuario o que ele quer cadastrar
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
                    //pede o nome e idade do aluno e faz o cadastro atrvaes do metodo "Cadastro" de AlunoController
                    //que gera a matrícula automaticamente
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
                        menuController.alunoController.Cadastrar(nomeAluno, idadeAluno);
                        Console.WriteLine("Aluno cadastrado com sucesso!");
                        Console.ReadKey();
                        break;


                    //pede o nome e a nota minima da disciplina e faz o cadastro usando o metodo "Cadastro" de DisciplinasController
                    //que gera o codigo automaticamente
                    case "2":
                        Console.Write("Nome da disciplina: ");
                        string nomeDisciplina = Console.ReadLine();
                        Console.Write("Nota mínima para aprovação: ");
                        if (!double.TryParse(Console.ReadLine(), out double notaMinima))
                        {
                            Console.WriteLine("Nota inválida.");
                            Console.ReadKey();
                            break;
                        }
                        menuController.DisciplinaController.Cadastro(nomeDisciplina, notaMinima);
                        Console.WriteLine("Disciplina cadastrada com sucesso!");
                        Console.ReadKey();
                        break;

                    //pede o nome/matricula do aluno e o nome/codigo da disciplina e faz o cadastro da matricula
                    //pelo metodo "Cadastro" de MatriculaController
                    case "3":
                        Console.Write("Nome ou matrícula do aluno: ");
                        string respAluno = Console.ReadLine();
                        Console.Write("Nome ou código da disciplina: ");
                        string respDisc = Console.ReadLine();
                        int matAluno3 = -1;
                        if (int.TryParse(respAluno, out int aux))
                        {
                            matAluno3 = aux;
                        }

                        int codDisc3b = -1;
                        if (int.TryParse(respDisc, out aux))
                        {
                            codDisc3b = aux;
                        }

                        string resultado = menuController.matriculaController.Cadastro(respAluno, matAluno3, respDisc, codDisc3b);

                        if (resultado == "Aluno não encontrado.")
                        {
                            Console.WriteLine("Aluno não encontrado. Matrícula não realizada.");
                        }
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

                    //atribui as notas do aluno pedindo nome/matricula do aluno e nome/codigo da disciplina
                    //atraves do metodo "AtribuirNota" de MatriculaController
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

                        int matNota = -1;
                        if (int.TryParse(alunoNota, out int mn))
                        {
                            matNota = mn;
                        }

                        int codNota = -1;
                        if (int.TryParse(discNota, out int cn))
                        {
                            codNota = cn;
                        }

                        bool atribuido = menuController.matriculaController.AtribuirNota(alunoNota, matNota, discNota, codNota, nota1, nota2);
                        Console.WriteLine(atribuido ? "Nota atribuída com sucesso!" : "Aluno ou disciplina não encontrados.");
                        Console.ReadKey();
                        break;

                    //caso o usuario escolha "0", volta ao menu principal
                    case "0":
                        return 0;

                    //mensagem padrao caso o usuario digite uma opcao que nao existe
                    default:
                        Console.WriteLine("Opção inválida. Digite um número entre 0 e 4.");
                        Console.ReadKey();
                        break;
                }
            }
        }

        //metodo interno que exibe o cabecalho no console
        internal static void ExibirCabecalho(string titulo)
        {
            ExibirSeparador();
            Console.WriteLine($"  {titulo}");
            ExibirSeparador();
        }

        //metodo interno que exibe o separador no console;
        internal static void ExibirSeparador()
        {
            Console.WriteLine(new string('─', 60));
        }
    }
}
