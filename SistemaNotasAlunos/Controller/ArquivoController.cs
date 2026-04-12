using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SistemaNotasAlunos.Model;
using System.Globalization;
using System.IO;

namespace SistemaNotasAlunos.Controller
{
    public class ArquivoController
    {
        public Aluno[] LerAlunos()
        {
            int count = 0;

            // 1. Contar quantas linhas (quantos alunos)
            using (StreamReader sr = new StreamReader("Alunos.dat"))
            {
                while (sr.ReadLine() != null)
                {
                    count++;
                }
            }

            // 2. Criar vetor
            Aluno[] alunos = new Aluno[count];

            // 3. Ler e preencher
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

            // 1. Contar quantas linhas (quantos alunos)
            using (StreamReader sr = new StreamReader("Disciplinas.dat"))
            {
                while (sr.ReadLine() != null)
                {
                    count++;
                }
            }

            // 2. Criar vetor
            Disciplina[] disciplinas = new Disciplina[count];

            // 3. Ler e preencher
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
        public Matricula[] LerMatricula()
        {
            int count = 0;

            // 1. Contar quantas linhas (quantos alunos)
            using (StreamReader sr = new StreamReader("Matriculas.dat"))
            {
                while (sr.ReadLine() != null)
                {
                    count++;
                }
            }

            // 2. Criar vetor
            Matricula[] matriculas = new Matricula[count];

            // 3. Ler e preencher
            using (StreamReader sr = new StreamReader("Matriculas.dat"))
            {
                string linha;
                int i = 0;

                while ((linha = sr.ReadLine()) != null)
                {
                    var partes = linha.Split(';');

                    matriculas[i] = new Matricula
                    {
                        CodigoDisciplina = int.Parse(partes[0]),
                        MatriculaAluno = int.Parse(partes[1]),
                        Nota1 = double.Parse(partes[2]),
                        Nota2 = double.Parse(partes[3]),
                    };

                    i++;
                }
            }

            return matriculas;
        }

        public void GravarAlunos(Aluno[] alunos)
        {
            using (StreamWriter sw = new StreamWriter("Alunos.dat"))
            {
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
                    sw.WriteLine($"{matricula.CodigoDisciplina};{matricula.MatriculaAluno};{matricula.Nota1};{matricula.Nota2}");
                }
            }
        }
    }
}