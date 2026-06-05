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
                    // Consulta de Alunos: lista todos os alunos cadastrados na lista encadeada
                    case "1":
                        Console.Clear();
                        ExibirCabecalho("LISTAGEM DE ALUNOS");
                        var atualAluno = menuController.alunoController.alunos.Cabeca;
                        if (atualAluno == null)
                        {
                            Console.WriteLine("Nenhum aluno cadastrado no sistema.");
                        }
                        else
                        {
                            while (atualAluno != null)
                            {
                                Aluno al = atualAluno.Valor;
                                if (al != null)
                                {
                                    Console.WriteLine($"Nome: {al.Nome} | Matrícula: {al.Matricula} | Idade: {al.Idade}");
                                }
                                atualAluno = atualAluno.Prox; // caminha pela lista
                            }
                        }
                        Console.ReadKey();
                        break;

                    // Consulta de Disciplinas: lista todas as disciplinas cadastradas na lista encadeada
                    case "2":
                        Console.Clear();
                        ExibirCabecalho("LISTAGEM DE DISCIPLINAS");
                        var atualDisc = menuController.DisciplinaController.disciplinas.Cabeca;
                        if (atualDisc == null)
                        {
                            Console.WriteLine("Nenhuma disciplina cadastrada no sistema.");
                        }
                        else
                        {
                            while (atualDisc != null)
                            {
                                Disciplina d = atualDisc.Valor;
                                if (d != null)
                                {
                                    Console.WriteLine($"Disciplina: {d.Nome} | Código: {d.Codigo} | Nota Mínima: {d.NotaMinima}");
                                }
                                atualDisc = atualDisc.Prox; // caminha pela lista
                            }
                        }
                        Console.ReadKey();
                        break;

                    // Consulta de Alunos de uma Disciplina: solicita e valida a disciplina em loop
                    case "3":
                        Console.Clear();
                        ExibirCabecalho("CONSULTA DE ALUNOS DA DISCIPLINA");
                        Disciplina discBusca = null;
                        while (true)
                        {
                            Console.Write("Digite o nome ou código da disciplina (ou deixe em branco para cancelar): ");
                            string resp3 = Console.ReadLine();
                            if (string.IsNullOrWhiteSpace(resp3)) break;

                            int codDisc = -1;
                            if (int.TryParse(resp3, out int cd))
                            {
                                codDisc = cd;
                            }

                            discBusca = menuController.DisciplinaController.Buscar(resp3, codDisc);
                            if (discBusca == null)
                            {
                                Console.WriteLine("Disciplina não existe! Por favor, informe uma disciplina válida.");
                                continue;
                            }
                            break; // sai do loop de validação
                        }

                        if (discBusca != null)
                        {
                            Console.WriteLine(menuController.matriculaController.AlunosDaDisciplina(discBusca));
                        }
                        else
                        {
                            Console.WriteLine("Consulta cancelada.");
                        }
                        Console.ReadKey();
                        break;

                    // Consulta de Disciplinas de um Aluno: solicita e valida o aluno em loop
                    case "4":
                        Console.Clear();
                        ExibirCabecalho("CONSULTA DE DISCIPLINAS DO ALUNO");
                        Aluno alunoBusca = null;
                        while (true)
                        {
                            Console.Write("Digite o nome ou matrícula do aluno (ou deixe em branco para cancelar): ");
                            string resp4 = Console.ReadLine();
                            if (string.IsNullOrWhiteSpace(resp4)) break;

                            int matAluno4 = -1;
                            if (int.TryParse(resp4, out int ma4))
                            {
                                matAluno4 = ma4;
                            }

                            alunoBusca = menuController.alunoController.Buscar(resp4, matAluno4);
                            if (alunoBusca == null)
                            {
                                Console.WriteLine("Aluno não existe! Por favor, informe um aluno válido.");
                                continue;
                            }
                            break; // sai do loop de validação
                        }

                        if (alunoBusca != null)
                        {
                            string resultado = menuController.matriculaController.DisciplinasDoAluno(alunoBusca);
                            Console.WriteLine(resultado);
                        }
                        else
                        {
                            Console.WriteLine("Consulta cancelada.");
                        }
                        Console.ReadKey();
                        break;

                    case "0":
                        return 0;

                    default:
                        Console.WriteLine("Opção inválida. Digite um número entre 0 e 4.");
                        Console.ReadKey();
                        break;
                }
            }
        }

        //exibe o menu de cadastros
        public static int ExibirMenuCadastros(MenuController menuController)
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
                    // Cadastro de Alunos: nome e idade. Matrícula é calculada e validada como única.
                    case "1":
                        Console.Clear();
                        ExibirCabecalho("CADASTRO DE ALUNO");
                        Console.Write("Nome do aluno: ");
                        string nomeAluno = Console.ReadLine();
                        if (string.IsNullOrWhiteSpace(nomeAluno))
                        {
                            Console.WriteLine("Nome não pode ser vazio.");
                            Console.ReadKey();
                            break;
                        }
                        Console.Write("Idade do aluno: ");
                        if (!int.TryParse(Console.ReadLine(), out int idadeAluno) || idadeAluno < 0)
                        {
                            Console.WriteLine("Idade inválida.");
                            Console.ReadKey();
                            break;
                        }
                        menuController.alunoController.Cadastrar(nomeAluno, idadeAluno);
                        Console.WriteLine("Aluno cadastrado com sucesso!");
                        Console.ReadKey();
                        break;

                    // Cadastro de Disciplinas: nome e nota mínima. Código é calculado e validado como único.
                    case "2":
                        Console.Clear();
                        ExibirCabecalho("CADASTRO DE DISCIPLINA");
                        Console.Write("Nome da disciplina: ");
                        string nomeDisciplina = Console.ReadLine();
                        if (string.IsNullOrWhiteSpace(nomeDisciplina))
                        {
                            Console.WriteLine("Nome da disciplina não pode ser vazio.");
                            Console.ReadKey();
                            break;
                        }
                        Console.Write("Nota mínima para aprovação: ");
                        if (!double.TryParse(Console.ReadLine(), out double notaMinima) || notaMinima < 0)
                        {
                            Console.WriteLine("Nota inválida.");
                            Console.ReadKey();
                            break;
                        }
                        menuController.DisciplinaController.Cadastro(nomeDisciplina, notaMinima);
                        Console.WriteLine("Disciplina cadastrada com sucesso!");
                        Console.ReadKey();
                        break;

                    // Cadastro de Matrículas: solicita aluno e disciplina em loops de validação separados
                    case "3":
                        Console.Clear();
                        ExibirCabecalho("CADASTRO DE MATRÍCULA");
                        Aluno alMat = null;
                        Disciplina discMat = null;

                        // Valida a escolha do Aluno
                        while (alMat == null)
                        {
                            Console.Write("Nome ou matrícula do aluno (ou deixe em branco para cancelar): ");
                            string respAluno = Console.ReadLine();
                            if (string.IsNullOrWhiteSpace(respAluno)) break;

                            int matAluno3 = -1;
                            if (int.TryParse(respAluno, out int aux))
                            {
                                matAluno3 = aux;
                            }

                            alMat = menuController.alunoController.Buscar(respAluno, matAluno3);
                            if (alMat == null)
                            {
                                Console.WriteLine("Aluno não encontrado! Por favor, digite novamente.");
                            }
                        }

                        if (alMat == null)
                        {
                            Console.WriteLine("Operação cancelada.");
                            Console.ReadKey();
                            break;
                        }

                        // Valida a escolha da Disciplina
                        while (discMat == null)
                        {
                            Console.Write("Nome ou código da disciplina (ou deixe em branco para cancelar): ");
                            string respDisc = Console.ReadLine();
                            if (string.IsNullOrWhiteSpace(respDisc)) break;

                            int codDisc3b = -1;
                            if (int.TryParse(respDisc, out int aux))
                            {
                                codDisc3b = aux;
                            }

                            discMat = menuController.DisciplinaController.Buscar(respDisc, codDisc3b);
                            if (discMat == null)
                            {
                                Console.WriteLine("Disciplina não encontrada! Por favor, digite novamente.");
                            }
                        }

                        if (discMat == null)
                        {
                            Console.WriteLine("Operação cancelada.");
                            Console.ReadKey();
                            break;
                        }

                        // Realiza o cadastro
                        string resultadoMat = menuController.matriculaController.Cadastro(alMat, discMat);
                        if (resultadoMat != null)
                        {
                            Console.WriteLine(resultadoMat);
                        }
                        else
                        {
                            Console.WriteLine("Matrícula realizada com sucesso!");
                        }
                        Console.ReadKey();
                        break;

                    // Atribuir Notas: solicita aluno e disciplina em loop. Informa as notas e atualiza.
                    case "4":
                        Console.Clear();
                        ExibirCabecalho("ATRIBUIR NOTA AO ALUNO");
                        Aluno alNota = null;
                        Disciplina discNota = null;

                        // Valida Aluno
                        while (alNota == null)
                        {
                            Console.Write("Nome ou matrícula do aluno (ou deixe em branco para cancelar): ");
                            string alunoNota = Console.ReadLine();
                            if (string.IsNullOrWhiteSpace(alunoNota)) break;

                            int matNota = -1;
                            if (int.TryParse(alunoNota, out int mn))
                            {
                                matNota = mn;
                            }

                            alNota = menuController.alunoController.Buscar(alunoNota, matNota);
                            if (alNota == null)
                            {
                                Console.WriteLine("Aluno não encontrado! Por favor, digite novamente.");
                            }
                        }

                        if (alNota == null)
                        {
                            Console.WriteLine("Operação cancelada.");
                            Console.ReadKey();
                            break;
                        }

                        // Valida Disciplina
                        while (discNota == null)
                        {
                            Console.Write("Nome ou código da disciplina (ou deixe em branco para cancelar): ");
                            string discNotaInput = Console.ReadLine();
                            if (string.IsNullOrWhiteSpace(discNotaInput)) break;

                            int codNota = -1;
                            if (int.TryParse(discNotaInput, out int cn))
                            {
                                codNota = cn;
                            }

                            discNota = menuController.DisciplinaController.Buscar(discNotaInput, codNota);
                            if (discNota == null)
                            {
                                Console.WriteLine("Disciplina não encontrada! Por favor, digite novamente.");
                            }
                        }

                        if (discNota == null)
                        {
                            Console.WriteLine("Operação cancelada.");
                            Console.ReadKey();
                            break;
                        }

                        // Localiza a matrícula correspondente
                        Matricula m = menuController.matriculaController.BuscarMatricula(alNota, discNota);
                        if (m == null)
                        {
                            Console.WriteLine("Erro: O aluno não está matriculado nesta disciplina.");
                            Console.ReadKey();
                            break;
                        }

                        // Exibe as notas atuais
                        Console.WriteLine($"Notas atuais na disciplina {discNota.Nome}:");
                        Console.WriteLine($"  Nota 1: {m.Nota1:F1}");
                        Console.WriteLine($"  Nota 2: {m.Nota2:F1}");
                        ExibirSeparador();

                        // Solicita novas notas
                        Console.Write("Nova Nota 1: ");
                        if (!double.TryParse(Console.ReadLine(), out double nota1) || nota1 < 0 || nota1 > 10)
                        {
                            Console.WriteLine("Nota 1 inválida. Deve ser de 0 a 10.");
                            Console.ReadKey();
                            break;
                        }

                        Console.Write("Nova Nota 2: ");
                        if (!double.TryParse(Console.ReadLine(), out double nota2) || nota2 < 0 || nota2 > 10)
                        {
                            Console.WriteLine("Nota 2 inválida. Deve ser de 0 a 10.");
                            Console.ReadKey();
                            break;
                        }

                        menuController.matriculaController.AtribuirNota(m, nota1, nota2);
                        Console.WriteLine("Notas atribuídas com sucesso!");
                        Console.ReadKey();
                        break;

                    case "0":
                        return 0;

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
