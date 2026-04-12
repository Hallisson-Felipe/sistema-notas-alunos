using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SistemaNotasAlunos.Model;
using System.IO;

namespace SistemaNotasAlunos.Controller
{
    public class ArquivoController
    {
        public Aluno[] LerAlunos()
        {
            int count = 0;

            //conta quantos alunos tem no arquivo para criar o vetor do tamanho correto
            using (StreamReader sr = new StreamReader("Alunos.dat"))
            {
                while (sr.ReadLine() != null)
                {
                    count++;
                }
            }


            Aluno[] alunos = new Aluno[count];

            //le o arquivo e preenche o vetor de alunos
            using (StreamReader sr = new StreamReader("Alunos.dat"))
            {
                string linha;
                int i = 0;

                while ((linha = sr.ReadLine()) != null)
                {
                    var partes = linha.Split(';');

                    alunos[i] = new Aluno
                    {
                        Matricula = int.Parse(partes[0]),
                        Nome = partes[1],
                        Idade = int.Parse(partes[2]),
                    };

                    i++;
                }
            }

            return alunos;
        }
        public Disciplina[] LerDisciplinas()
        {
            int count = 0;

            //le o arquivo e conta quantas disciplinas tem para criar o vetor do tamanho correto
            using (StreamReader sr = new StreamReader("Disciplinas.dat"))
            {
                while (sr.ReadLine() != null)
                {
                    count++;
                }
            }

            Disciplina[] disciplinas = new Disciplina[count];

            //le o arquivo e preenche o vetor de disciplinas
            using (StreamReader sr = new StreamReader("Disciplinas.dat"))
            {
                string linha;
                int i = 0;

                while ((linha = sr.ReadLine()) != null)
                {
                    var partes = linha.Split(';');

                    disciplinas[i] = new Disciplina
                    {
                        Codigo = int.Parse(partes[0]),
                        Nome = partes[1],
                        NotaMinima = double.Parse(partes[2]),
                    };

                    i++;
                }
            }

            return disciplinas;
        }
        public Matricula[] LerMatricula(Aluno[] alunos, Disciplina[] disciplinas)
        {
            int count = 0;

            using (StreamReader sr = new StreamReader("Matriculas.dat"))
            {
                while (sr.ReadLine() != null)
                {
                    count++;
                }
            }

            Matricula[] matriculas = new Matricula[count];

            using (StreamReader sr = new StreamReader("Matriculas.dat"))
            {
                string linha;
                int i = 0;

                while ((linha = sr.ReadLine()) != null)
                {
                    var partes = linha.Split(';');

                    Matricula m = new Matricula();

                    int codigoDisciplina = int.Parse(partes[0]);
                    int matriculaAluno = int.Parse(partes[1]);

                    // 🔗 vínculo com objetos já carregados
                    for (int j = 0; j < disciplinas.Length; j++)
                    {
                        if (disciplinas[j].Codigo == codigoDisciplina)
                        {
                            m.disciplina = disciplinas[j];
                            break;
                        }
                    }

                    for (int j = 0; j < alunos.Length; j++)
                    {
                        if (alunos[j].Matricula == matriculaAluno)
                        {
                            m.aluno = alunos[j];
                            break;
                        }
                    }

                    m.Nota1 = double.Parse(partes[2]);
                    m.Nota2 = double.Parse(partes[3]);

                    matriculas[i] = m;
                    i++;
                }
            }

            return matriculas;
        }

        public void GravarAlunos(Aluno[] alunos)
        {
            using (StreamWriter sw = new StreamWriter("Alunos.dat"))
            {
                //grava os alunos no arquivo, cada campo separado por ';'
                foreach (var aluno in alunos)
                {
                    sw.WriteLine($"{aluno.Matricula};{aluno.Nome};{aluno.Idade}");
                }
            }
        }
        public void GravarDisciplinas(Disciplina[] disciplinas)
        {
            using (StreamWriter sw = new StreamWriter("Disciplinas.dat"))
            {
                //grava os alunos no arquivo, cada campo separado por ';'
                foreach (var disciplina in disciplinas)
                {
                    sw.WriteLine($"{disciplina.Codigo};{disciplina.Nome};{disciplina.NotaMinima}");
                }
            }
        }
        public void GravarMatriculas(Matricula[] matriculas)
        {
            using (StreamWriter sw = new StreamWriter("Matriculas.dat"))
            {
                foreach (var matricula in matriculas)
                {
                    sw.WriteLine($"{matricula.disciplina.Codigo};{matricula.aluno.Matricula};{matricula.Nota1};{matricula.Nota2}");
                }
            }
        }
    }
}