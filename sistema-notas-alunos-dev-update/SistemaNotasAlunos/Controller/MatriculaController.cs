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

        ArquivoController ar = new ArquivoController();

        // CORREÇÃO 1: loop duplo removido — null check estava dentro do for externo,
        // causando N inserções duplicadas de cada matrícula (N = total de matrículas).
        public MatriculaController()
        {
            var matriculasLidas = ar.LerMatricula(alunoController.alunos, disciplinaController.disciplinas);
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

        public string Cadastro(string nomeAluno = "", int matriculaAluno = -1, string nomeDisciplina = "", int codigoDisciplina = -1)
        {
            Aluno aluno = alunoController.Buscar(nomeAluno, matriculaAluno);
            Disciplina disciplina = disciplinaController.Buscar(nomeDisciplina, codigoDisciplina);

            if (aluno == null)
                return "Aluno não encontrado.";

            if (disciplina == null)
                return "Disciplina não encontrada.";

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
                return "Disciplina não encontrada.";

            for (int i = 0; i < qtdMatriculas; i++)
            {
                if (matriculas[i] == null) continue;

                if (matriculas[i].disciplina.Codigo == disciplina.Codigo)
                {
                    double nota1 = matriculas[i].Nota1;
                    double nota2 = matriculas[i].Nota2;
                    double media = (nota1 + nota2) / 2;
                    string status = media >= disciplina.NotaMinima ? "Aprovado" : "Reprovado";

                    resultado += $"Aluno: {matriculas[i].aluno.Nome} | " +
                                 $"Nota1: {nota1} | Nota2: {nota2} | " +
                                 $"Média: {media:F2} | {status}\n";
                }
            }

            return resultado == "" ? "Nenhum aluno encontrado para essa disciplina." : resultado;
        }

        public string DisciplinasDoAluno(string nomeAluno = "", int matriculaAluno = -1)
        {
            string nomes = "";
            Aluno aluno = alunoController.Buscar(nomeAluno, matriculaAluno);

            if (aluno == null)
                return "Aluno não encontrado.";

            for (int i = 0; i < qtdMatriculas; i++)
            {
                if (matriculas[i] == null) continue;

                if (matriculas[i].aluno.Matricula == aluno.Matricula)
                    nomes += matriculas[i].disciplina.Nome + ";";
            }

            return nomes;
        }

        public double CalcularMedia(double nota1, double nota2)
        {
            return (nota1 + nota2) / 2;
        }

        // CORREÇÃO 2: assinatura corrigida para receber nota1 e nota2 separadas.
        // Antes o parâmetro era "double nota" e o corpo referenciava variáveis nota1/nota2
        // inexistentes, além de tentar acessar campos nota1/nota2 (minúsculos) que não existem no model.
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
    }
}
