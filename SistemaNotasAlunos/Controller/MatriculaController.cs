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
        //instacia os objetos alunoController, disciplinaController e arquivoController
        AlunoController alunoController;
        DisciplinaController disciplinaController;
        ArquivoController arquivoController = new ArquivoController();
        
        //inicializa o vetor de matriculas com 100 posicoes
        Matricula[] matriculas = new Matricula[100];
        int qtdMatriculas = 0;

        //contrutor de matricula controller que le as matriculas do arquivo e preenche o vetor
        public MatriculaController(AlunoController ac, DisciplinaController dc)
        {
            alunoController = ac;
            disciplinaController = dc;

            var matriculasLidas = arquivoController.LerMatricula(alunoController.alunos, disciplinaController.disciplinas);
            qtdMatriculas = 0;

            if (matriculasLidas != null)
            {
                for (int i = 0; i < matriculasLidas.Length && i < matriculas.Length; i++)
                {
                    matriculas[i] = matriculasLidas[i];
                    qtdMatriculas++;
                }
            }
        }

        //metodo que cadastra matriculas a partir do nome/matricula do aluno e nome/codigo da disciplina
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

        //metodo que retorna a string com todos os alunos matriculados em uma disciplina
        public string AlunosDaDisciplina(string nomeDisciplina = "", int codigoDisciplina = -1)
        {
            string resultado = "";
            Disciplina disciplina = disciplinaController.Buscar(nomeDisciplina, codigoDisciplina);

            if (disciplina == null)
                return "Disciplina não encontrada.";

            for (int i = 0; i < qtdMatriculas; i++)
            {
                if (matriculas[i] == null) continue;

                if (matriculas[i].disciplina.Codigo == disciplina.Codigo)
                {
                    double nota1 = matriculas[i].Nota1;
                    double nota2 = matriculas[i].Nota2;
                    double media = (nota1 + nota2) / 2;
                    string status = "";
                    if (media >= disciplina.NotaMinima)
                    {
                        status = "Aprovado";
                    }
                    else
                    {
                        status = "Reprovado";
                    }

                    resultado += $"Aluno: {matriculas[i].aluno.Nome} | " +
                                 $"Nota1: {nota1} | Nota2: {nota2} | " +
                                 $"Média: {media:F2} | {status}\n";
                }
            }

            return resultado == "" ? "Nenhum aluno encontrado para essa disciplina." : resultado;
        }

        //metodo que retorna s tring com todos as disciplinas em que um aluno esta matriculado
        public string DisciplinasDoAluno(string nomeAluno = "", int matriculaAluno = -1)
        {
            string resultado = "";
            Aluno aluno = alunoController.Buscar(nomeAluno, matriculaAluno);

            if (aluno == null)
                return "Aluno não encontrado.";

            for (int i = 0; i < qtdMatriculas; i++)
            {
                if (matriculas[i] == null) continue;

                if (matriculas[i].aluno.Matricula == aluno.Matricula)
                {
                    Disciplina disciplina = matriculas[i].disciplina;

                    double nota1 = matriculas[i].Nota1;
                    double nota2 = matriculas[i].Nota2;
                    double media = (nota1 + nota2) / 2;
                    string status = "";
                    if (media >= disciplina.NotaMinima) {
                        status = "Aprovado";
                    }
                    else
                    {
                        status = "Reprovado";
                    }

                        resultado += $"Disciplina: {disciplina.Nome} | " +
                                     $"Nota1: {nota1} | Nota2: {nota2} | " +
                                     $"Média: {media:F2} | {status}\n";
                }
            }

            return resultado == "" ? "Nenhuma disciplina encontrada para esse aluno." : resultado;
        }

        //calcula a media do aluno
        public double CalcularMedia(double nota1, double nota2)
        {
            return (nota1 + nota2) / 2;
        }

        //metodo que atribui as notas ao aluno
        public bool AtribuirNota(string nomeAluno = "", int matriculaAluno = -1,
                                  string nomeDisciplina = "", int codigoDisciplina = -1,
                                  double nota1 = 0, double nota2 = 0)
        {
            Aluno aluno = alunoController.Buscar(nomeAluno, matriculaAluno);
            Disciplina disciplina = disciplinaController.Buscar(nomeDisciplina, codigoDisciplina);

            if (aluno == null || disciplina == null)
                return false;

            for (int i = 0; i < qtdMatriculas; i++)
            {
                if (matriculas[i] == null) continue;

                if (matriculas[i].aluno.Matricula == aluno.Matricula &&
                    matriculas[i].disciplina.Codigo == disciplina.Codigo)
                {
                    matriculas[i].Nota1 = nota1;
                    matriculas[i].Nota2 = nota2;
                    return true;
                }
            }

            return false;
        }

        //grava as matriculas no arquivo DAT
        public void GravarMatriculas()
        {
            Matricula[] matriculasValidas = new Matricula[qtdMatriculas];

            for (int i = 0; i < qtdMatriculas; i++)
            {
                matriculasValidas[i] = matriculas[i];
            }

            arquivoController.GravarMatriculas(matriculasValidas);
        }
    }
}
