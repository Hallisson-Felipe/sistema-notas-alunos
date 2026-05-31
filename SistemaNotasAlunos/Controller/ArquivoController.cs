using System;
using System.IO;
using SistemaNotasAlunos.Model;

namespace SistemaNotasAlunos.Controller
{
    // controller de arquivos dat
    public class ArquivoController
    {
        // le alunos do arquivo dat e retorna uma lista encadeada
        public ListaAlunos LerAlunos()
        {
            var alunos = new ListaAlunos();

            if (!File.Exists("Alunos.dat"))
            {
                return alunos;
            }

            using (StreamReader sr = new StreamReader("Alunos.dat"))
            {
                string linha;
                // le linha por linha ate o fim do arquivo
                while ((linha = sr.ReadLine()) != null)
                {
                    if (string.IsNullOrWhiteSpace(linha)) continue;

                    // cada linha tem: matricula;nome;idade
                    var partes = linha.Split(';');
                    if (partes.Length >= 3)
                    {
                        var aluno = new Aluno
                        {
                            Matricula = int.Parse(partes[0]),
                            Nome = partes[1],
                            Idade = int.Parse(partes[2])
                        };
                        alunos.Adicionar(aluno);
                    }
                }
            }

            return alunos;
        }

        // le disciplinas do arquivo dat e retorna uma lista encadeada
        public ListaDisciplinas LerDisciplinas()
        {
            var disciplinas = new ListaDisciplinas();

            if (!File.Exists("Disciplinas.dat"))
            {
                return disciplinas;
            }

            using (StreamReader sr = new StreamReader("Disciplinas.dat"))
            {
                string linha;
                // le linha por linha ate o fim do arquivo
                while ((linha = sr.ReadLine()) != null)
                {
                    if (string.IsNullOrWhiteSpace(linha)) continue;

                    // cada linha tem: codigo;nome;notaminima
                    var partes = linha.Split(';');
                    if (partes.Length >= 3)
                    {
                        var disciplina = new Disciplina
                        {
                            Codigo = int.Parse(partes[0]),
                            Nome = partes[1],
                            NotaMinima = double.Parse(partes[2])
                        };
                        disciplinas.Adicionar(disciplina);
                    }
                }
            }

            return disciplinas;
        }

        // le matriculas do arquivo dat, ligando cada uma ao aluno e disciplina certos
        public ListaMatriculas LerMatricula(ListaAlunos alunos, ListaDisciplinas disciplinas)
        {
            var matriculas = new ListaMatriculas();

            if (!File.Exists("Matriculas.dat"))
            {
                return matriculas;
            }

            using (StreamReader sr = new StreamReader("Matriculas.dat"))
            {
                string linha;
                // le linha por linha ate o fim do arquivo
                while ((linha = sr.ReadLine()) != null)
                {
                    if (string.IsNullOrWhiteSpace(linha)) continue;

                    // cada linha tem: codigoDisciplina;matriculaAluno;nota1;nota2
                    var partes = linha.Split(';');
                    if (partes.Length >= 4)
                    {
                        int codigoDisciplina = int.Parse(partes[0]);
                        int matriculaAluno = int.Parse(partes[1]);
                        double nota1 = double.Parse(partes[2]);
                        double nota2 = double.Parse(partes[3]);

                        // busca a disciplina correspondente percorrendo a lista
                        Disciplina disc = null;
                        NodeDisciplina atualDisc = disciplinas.Head;
                        while (atualDisc != null)
                        {
                            if (atualDisc.Value != null && atualDisc.Value.Codigo == codigoDisciplina)
                            {
                                disc = atualDisc.Value;
                                break;
                            }
                            atualDisc = atualDisc.Next;
                        }

                        // busca o aluno correspondente percorrendo a lista
                        Aluno alu = null;
                        NodeAluno atualAlu = alunos.Head;
                        while (atualAlu != null)
                        {
                            if (atualAlu.Value != null && atualAlu.Value.Matricula == matriculaAluno)
                            {
                                alu = atualAlu.Value;
                                break;
                            }
                            atualAlu = atualAlu.Next;
                        }

                        // so adiciona se encontrou os dois
                        if (disc != null && alu != null)
                        {
                            var m = new Matricula
                            {
                                aluno = alu,
                                disciplina = disc,
                                Nota1 = nota1,
                                Nota2 = nota2
                            };
                            matriculas.Adicionar(m);
                        }
                    }
                }
            }

            return matriculas;
        }

        // grava os alunos no arquivo percorrendo a lista encadeada
        public void GravarAlunos(ListaAlunos alunos)
        {
            using (StreamWriter sw = new StreamWriter("Alunos.dat"))
            {
                NodeAluno atual = alunos.Head;
                while (atual != null)
                {
                    Aluno alu = atual.Value;
                    if (alu != null)
                    {
                        // formato: matricula;nome;idade
                        sw.WriteLine($"{alu.Matricula};{alu.Nome};{alu.Idade}");
                    }
                    atual = atual.Next;
                }
            }
        }

        // grava as disciplinas no arquivo percorrendo a lista encadeada
        public void GravarDisciplinas(ListaDisciplinas disciplinas)
        {
            using (StreamWriter sw = new StreamWriter("Disciplinas.dat"))
            {
                NodeDisciplina atual = disciplinas.Head;
                while (atual != null)
                {
                    Disciplina disc = atual.Value;
                    if (disc != null)
                    {
                        // formato: codigo;nome;notaminima
                        sw.WriteLine($"{disc.Codigo};{disc.Nome};{disc.NotaMinima}");
                    }
                    atual = atual.Next;
                }
            }
        }

        // grava as matriculas no arquivo percorrendo a lista encadeada
        public void GravarMatriculas(ListaMatriculas matriculas)
        {
            using (StreamWriter sw = new StreamWriter("Matriculas.dat"))
            {
                NodeMatricula atual = matriculas.Head;
                while (atual != null)
                {
                    Matricula m = atual.Value;
                    if (m != null && m.disciplina != null && m.aluno != null)
                    {
                        // formato: codigoDisciplina;matriculaAluno;nota1;nota2
                        sw.WriteLine($"{m.disciplina.Codigo};{m.aluno.Matricula};{m.Nota1};{m.Nota2}");
                    }
                    atual = atual.Next;
                }
            }
        }
    }
}