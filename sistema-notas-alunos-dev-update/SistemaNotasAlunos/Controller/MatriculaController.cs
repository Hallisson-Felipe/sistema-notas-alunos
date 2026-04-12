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
            {
                matriculas[i] = matriculasLidas[i];
                qtdMatriculas++;
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
            string nomes = "";
            Disciplina disciplina = disciplinaController.Buscar(nomeDisciplina, codigoDisciplina);

            if (disciplina == null)
                return "Disciplina não encontrada.";

            for (int i = 0; i < qtdMatriculas; i++)
            {
                if (matriculas[i] == null)
                {
                    continue;
                }
                if (matriculas[i].disciplina.Codigo == disciplina.Codigo)
                {
                    nomes += matriculas[i].aluno.Nome + ";";
                }
            }

            return nomes;
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
    }
}
