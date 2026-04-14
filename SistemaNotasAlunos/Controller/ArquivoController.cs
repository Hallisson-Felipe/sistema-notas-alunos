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
        // metodo que le o arquivo, preenche e retorna um vetor de Aluno
        public Aluno[] LerAlunos()
        {
            int count = 0;

            // conta quantos alunos tem no arquivo para criar o vetor do tamanho correto
            using (StreamReader sr = new StreamReader("Alunos.dat"))
            {
                while (sr.ReadLine() != null)
                {
                    count++;
                }
            }

            // cria o vetor com o tamanho exato
            Aluno[] alunos = new Aluno[count];

            // le o arquivo novamente e preenche o vetor de alunos
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

        // metodo que le o arquivo, preenche e retorna um vetor de Disciplina
        public Disciplina[] LerDisciplinas()
        {
            int count = 0;

            // conta quantas disciplinas existem no arquivo
            using (StreamReader sr = new StreamReader("Disciplinas.dat"))
            {
                while (sr.ReadLine() != null)
                {
                    count++;
                }
            }

            // cria o vetor com o tamanho correto
            Disciplina[] disciplinas = new Disciplina[count];

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

        // metodo que le o arquivo de matriculas e relaciona com alunos e disciplinas
        public Matricula[] LerMatricula(Aluno[] alunos, Disciplina[] disciplinas)
        {
            int count = 0;

            // conta quantas matriculas existem no arquivo
            using (StreamReader sr = new StreamReader("Matriculas.dat"))
            {
                while (sr.ReadLine() != null)
                {
                    count++;
                }
            }
            //cria o vetor do tamanho certo
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

                    // procura a disciplina correspondente no vetor
                    for (int j = 0; j < disciplinas.Length; j++)
                    {
                        if (disciplinas[j].Codigo == codigoDisciplina)
                        {
                            m.disciplina = disciplinas[j];
                            break;
                        }
                    }

                    // procura o aluno correspondente no vetor
                    for (int j = 0; j < alunos.Length; j++)
                    {
                        if (alunos[j].Matricula == matriculaAluno)
                        {
                            m.aluno = alunos[j];
                            break;
                        }
                    }

                    // atribui as notas da matricula
                    m.Nota1 = double.Parse(partes[2]);
                    m.Nota2 = double.Parse(partes[3]);

                    matriculas[i] = m;
                    i++;
                }
            }

            return matriculas;
        }

        //metodo que grava os alunos no arquivo
        public void GravarAlunos(Aluno[] alunos)
        {
            using (StreamWriter sw = new StreamWriter("Alunos.dat"))
            {
                // grava cada aluno em uma linha, separando os campos por ';'
                foreach (var aluno in alunos)
                {
                    sw.WriteLine($"{aluno.Matricula};{aluno.Nome};{aluno.Idade}");
                }
            }
        }

        // metodo que grava as disciplinas no arquivo
        public void GravarDisciplinas(Disciplina[] disciplinas)
        {
            using (StreamWriter sw = new StreamWriter("Disciplinas.dat"))
            {
                // grava cada disciplina em uma linha
                foreach (var disciplina in disciplinas)
                {
                    sw.WriteLine($"{disciplina.Codigo};{disciplina.Nome};{disciplina.NotaMinima}");
                }
            }
        }

        // metodo que grava as matriculas no arquivo
        public void GravarMatriculas(Matricula[] matriculas)
        {
            string arquivo = "Matriculas.dat";
            
            using (StreamWriter sw = new StreamWriter(arquivo))
            {
                // grava cada matricula com os dados relacionados
                foreach (var matricula in matriculas)
                {
                    sw.WriteLine($"{matricula.disciplina.Codigo};{matricula.aluno.Matricula};{matricula.Nota1};{matricula.Nota2}");
                }
            }
        }
    }
}