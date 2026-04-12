using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SistemaNotasAlunos.Model;

namespace SistemaNotasAlunos.Controller
{
    public class MatriculaController
    {
        AlunoController alunoController = new AlunoController();
        DisciplinaController disciplinaController = new DisciplinaController();
        Matricula[] matriculas = new Matricula[100];
        int qtdMatriculas = 0;

        //instancia o controller de arquivo para ler os matriculas do arquivo e preencher o vetor de matriculas
        ArquivoController ar = new ArquivoController();

        //construtor do controller de matricula, le as matriculas do arquivo e preenche o vetor de metriculas
        public MatriculaController()
        {
            var matriculasLidas = ar.LerMatricula(alunoController.alunos, disciplinaController.disciplinas);

            qtdMatriculas = 0;

            for (int i = 0; i < matriculasLidas.Length; i++)
                if (matriculasLidas != null)
                {
                    for (int j = 0; j < matriculasLidas.Length && j < matriculas.Length; j++)
                    {
                        matriculas[j] = matriculasLidas[j];
                        qtdMatriculas++;
                    }
                }
        }

        //cadastra a matricula pegando o nome/matricula do aluno e o nome/codigo da disciplina
        public string Cadastro(string nomeAluno = "", int matriculaAluno = -1, string nomeDisciplina = "", int codigoDisciplina = -1)
        {
            Aluno aluno = alunoController.Buscar(nomeAluno, matriculaAluno);
            Disciplina disciplina = disciplinaController.Buscar(nomeDisciplina, codigoDisciplina);

            if (aluno == null)
            {
                return "Aluno não encontrado.";
            }

            if (disciplina == null)
            {
                return "Disciplina não encontrada.";
            }

            matriculas[qtdMatriculas] = new Matricula();
            matriculas[qtdMatriculas].aluno = aluno;
            matriculas[qtdMatriculas].disciplina = disciplina;

            qtdMatriculas++;

            return null;
        }

        public string AlunosDaDisciplina(string nomeDisciplina = "", int codigoDisciplina = -1)
        {
            string resultado = "";
            Disciplina disciplina = disciplinaController.Buscar(nomeDisciplina, codigoDisciplina);

            if (disciplina == null)
            {
                return "Disciplina não encontrada.";
            }

            for (int i = 0; i < qtdMatriculas; i++)
            {
                if (matriculas[i] == null)
                {
                    continue;
                }

                if (matriculas[i].disciplina.Codigo == disciplina.Codigo)
                {
                    double nota1 = matriculas[i].Nota1;
                    double nota2 = matriculas[i].Nota2;
                    double media = (nota1 + nota2) / 2;

                    string status = media >= disciplina.NotaMinima ? "Aprovado" : "Reprovado";

                    resultado += $"Aluno: {matriculas[i].aluno.Nome} | " +
                                 $"Nota1: {nota1} | Nota2: {nota2} | " +
                                 $"Média: {media} | {status}\n";
                }
            }

            if (resultado == "")
            {
                return "Nenhum aluno encontrado para essa disciplina.";
            }

            return resultado;
        }

        public string DisciplinasDoAluno(string nomeAluno = "", int matriculaAluno = -1)
        {
            string nomes = "";
            Aluno aluno = alunoController.Buscar(nomeAluno, matriculaAluno);

            if (aluno == null)
                return "Aluno não encontrado.";

            for (int i = 0; i < qtdMatriculas; i++)
            {
                if (matriculas[i] == null)
                {
                    continue;
                }

                if (matriculas[i].aluno.Matricula == aluno.Matricula)
                {
                    nomes += matriculas[i].disciplina.Nome + ";";
                }
            }

            return nomes;
        }

        public double CalcularMedia(double nota1, double nota2)
        {
            return (nota1 + nota2) / 2;
        }

        public bool AtribuirNota(string nomeAluno = "", int matriculaAluno = -1, string nomeDisciplina = "", int codigoDisciplina = -1, double nota = 0)
        {
            Aluno aluno = alunoController.Buscar(nomeAluno, matriculaAluno);
            Disciplina disciplina = disciplinaController.Buscar(nomeDisciplina, codigoDisciplina);

            if (aluno == null || disciplina == null)
            {
                return false;
            }

            for (int i = 0; i < qtdMatriculas; i++)
            {
                if (matriculas[i] == null)
                {
                    continue;
                }

                if (matriculas[i].aluno.Matricula == aluno.Matricula &&
                    matriculas[i].disciplina.Codigo == disciplina.Codigo)
                {
                    matriculas[i].nota1 = nota1;
                    matriculas[i].nota2 = nota2;

                    if (matriculas[i].Nota1 == 0)
                    {
                        matriculas[i].Nota1 = nota;
                    }
                    else
                    {
                        matriculas[i].Nota2 = nota;
                    }

                    return true;
                }
            }

            return false;
        }
    }
}