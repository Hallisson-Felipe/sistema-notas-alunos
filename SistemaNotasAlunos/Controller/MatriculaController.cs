using System;
using SistemaNotasAlunos.Model;

namespace SistemaNotasAlunos.Controller
{
    // controller de matriculas
    public class MatriculaController
    {
        private AlunoController alunoController;
        private DisciplinaController disciplinaController;
        private ArquivoController arquivoController = new ArquivoController();

        // lista duplamente encadeada de matriculas
        public ListaMatriculas matriculas { get; set; }

        // construtor: recebe os controllers de aluno e disciplina
        public MatriculaController(AlunoController ac, DisciplinaController dc)
        {
            alunoController = ac;
            disciplinaController = dc;

            // le do arquivo passando as listas ja carregadas
            matriculas = arquivoController.LerMatricula(alunoController.alunos, disciplinaController.disciplinas);
        }

        // cadastra matricula
        public string Cadastro(Aluno aluno, Disciplina disciplina)
        {
            if (aluno == null)
            {
                return "Aluno não encontrado.";
            }
            if (disciplina == null)
            {
                return "Disciplina não encontrada.";
            }

            // verifica se ja esta matriculado percorrendo a lista
            NodeMatricula atual = matriculas.Head;
            while (atual != null)
            {
                if (atual.Value != null && 
                    atual.Value.aluno.Matricula == aluno.Matricula && 
                    atual.Value.disciplina.Codigo == disciplina.Codigo)
                {
                    return "Aluno já está matriculado nesta disciplina.";
                }
                atual = atual.Next;
            }

            Matricula novaMatricula = new Matricula
            {
                aluno = aluno,
                disciplina = disciplina
            };

            // adiciona na lista
            matriculas.Adicionar(novaMatricula);
            return null;
        }

        // sobrecarga para cadastrar matricula pelo nome/codigo
        public string Cadastro(string nomeAluno = "", int matriculaAluno = -1, string nomeDisciplina = "", int codigoDisciplina = -1)
        {
            Aluno aluno = alunoController.Buscar(nomeAluno, matriculaAluno);
            Disciplina disciplina = disciplinaController.Buscar(nomeDisciplina, codigoDisciplina);
            return Cadastro(aluno, disciplina);
        }

        // retorna alunos da disciplina com notas e situacao
        public string AlunosDaDisciplina(Disciplina disciplina)
        {
            if (disciplina == null)
                return "Disciplina não encontrada.";

            string resultado = "";
            NodeMatricula atual = matriculas.Head;

            // percorre a lista procurando matriculas dessa disciplina
            while (atual != null)
            {
                Matricula m = atual.Value;
                if (m != null && m.disciplina.Codigo == disciplina.Codigo)
                {
                    double nota1 = m.Nota1;
                    double nota2 = m.Nota2;
                    double media = (nota1 + nota2) / 2;
                    string status = media >= disciplina.NotaMinima ? "Aprovado" : "Reprovado";

                    resultado += $"Aluno: {m.aluno.Nome} | " +
                                 $"Nota1: {nota1} | Nota2: {nota2} | " +
                                 $"Média: {media:F2} | {status}\n";
                }
                atual = atual.Next;
            }

            return resultado == "" ? "Nenhum aluno encontrado para essa disciplina." : resultado;
        }

        // sobrecarga de alunos da disciplina pelo nome/codigo
        public string AlunosDaDisciplina(string nomeDisciplina = "", int codigoDisciplina = -1)
        {
            Disciplina disciplina = disciplinaController.Buscar(nomeDisciplina, codigoDisciplina);
            return AlunosDaDisciplina(disciplina);
        }

        // retorna disciplinas do aluno com notas e situacao
        public string DisciplinasDoAluno(Aluno aluno)
        {
            if (aluno == null)
                return "Aluno não encontrado.";

            string resultado = "";
            NodeMatricula atual = matriculas.Head;

            // percorre a lista procurando matriculas desse aluno
            while (atual != null)
            {
                Matricula m = atual.Value;
                if (m != null && m.aluno.Matricula == aluno.Matricula)
                {
                    Disciplina disciplina = m.disciplina;
                    double nota1 = m.Nota1;
                    double nota2 = m.Nota2;
                    double media = (nota1 + nota2) / 2;
                    string status = media >= disciplina.NotaMinima ? "Aprovado" : "Reprovado";

                    resultado += $"Disciplina: {disciplina.Nome} | " +
                                 $"Nota1: {nota1} | Nota2: {nota2} | " +
                                 $"Média: {media:F2} | {status}\n";
                }
                atual = atual.Next;
            }

            return resultado == "" ? "Nenhuma disciplina encontrada para esse aluno." : resultado;
        }

        // sobrecarga de disciplinas do aluno pelo nome/matricula
        public string DisciplinasDoAluno(string nomeAluno = "", int matriculaAluno = -1)
        {
            Aluno aluno = alunoController.Buscar(nomeAluno, matriculaAluno);
            return DisciplinasDoAluno(aluno);
        }

        // calcula a media das notas
        public double CalcularMedia(double nota1, double nota2)
        {
            return (nota1 + nota2) / 2;
        }

        // busca a matricula na lista encadeada pelo aluno e disciplina
        public Matricula BuscarMatricula(Aluno aluno, Disciplina disciplina)
        {
            if (aluno == null || disciplina == null) return null;

            NodeMatricula atual = matriculas.Head;
            while (atual != null)
            {
                Matricula m = atual.Value;
                if (m != null && m.aluno.Matricula == aluno.Matricula && m.disciplina.Codigo == disciplina.Codigo)
                {
                    return m;
                }
                atual = atual.Next;
            }
            return null;
        }

        // atribui nota diretamente na matricula
        public void AtribuirNota(Matricula matricula, double nota1, double nota2)
        {
            if (matricula != null)
            {
                matricula.Nota1 = nota1;
                matricula.Nota2 = nota2;
            }
        }

        // atribui nota buscando pelo nome/matricula e disciplina
        public bool AtribuirNota(string nomeAluno = "", int matriculaAluno = -1,
                                  string nomeDisciplina = "", int codigoDisciplina = -1,
                                  double nota1 = 0, double nota2 = 0)
        {
            Aluno aluno = alunoController.Buscar(nomeAluno, matriculaAluno);
            Disciplina disciplina = disciplinaController.Buscar(nomeDisciplina, codigoDisciplina);

            Matricula m = BuscarMatricula(aluno, disciplina);
            if (m != null)
            {
                AtribuirNota(m, nota1, nota2);
                return true;
            }

            return false;
        }

        // grava as matriculas no arquivo
        public void GravarMatriculas()
        {
            arquivoController.GravarMatriculas(matriculas);
        }
    }
}
