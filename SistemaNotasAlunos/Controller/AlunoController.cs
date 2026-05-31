using System;
using SistemaNotasAlunos.Model;

namespace SistemaNotasAlunos.Controller
{
    // controller de aluno
    public class AlunoController
    {
        // lista duplamente encadeada de alunos
        public ListaAlunos alunos { get; set; }

        private ArquivoController ar = new ArquivoController();

        // construtor: carrega os alunos do arquivo ao iniciar
        public AlunoController()
        {
            alunos = ar.LerAlunos();
        }

        // cadastra um novo aluno na lista
        public void Cadastrar(string nome, int idade)
        {
            Aluno novoAluno = new Aluno
            {
                Idade = idade,
                Nome = nome,
                Matricula = GerarMatricula()
            };

            // adiciona na lista duplamente encadeada
            alunos.Adicionar(novoAluno);
        }

        // gera matricula aleatoria sem repetir
        public int GerarMatricula()
        {
            int rand;
            while (true)
            {
                bool find = false;
                rand = Random.Shared.Next(100, 1000);

                // percorre a lista procurando se o numero ja existe
                NodeAluno atual = alunos.Head;
                while (atual != null)
                {
                    if (atual.Value != null && atual.Value.Matricula == rand)
                    {
                        find = true;
                        break;
                    }
                    atual = atual.Next;
                }

                if (!find)
                {
                    break;
                }
            }
            return rand;
        }

        // busca o aluno pelo nome ou pela matricula
        public Aluno Buscar(string nome = "", int matricula = -1)
        {
            // percorre a lista do inicio ao fim
            NodeAluno atual = alunos.Head;
            while (atual != null)
            {
                Aluno aluno = atual.Value;
                if (aluno != null)
                {
                    // verifica se bate o nome (ignora maiuscula e minuscula)
                    if (!string.IsNullOrEmpty(nome) && aluno.Nome.Trim().ToLower() == nome.Trim().ToLower())
                    {
                        return aluno;
                    }

                    // verifica se bate a matricula
                    if (matricula != -1 && aluno.Matricula == matricula)
                    {
                        return aluno;
                    }
                }
                atual = atual.Next;
            }
            return null;
        }

        // grava os alunos no arquivo dat
        public void GravarAlunos()
        {
            ar.GravarAlunos(alunos);
        }
    }
}
