using System;
using SistemaNotasAlunos.Model;

namespace SistemaNotasAlunos.Controller
{
    // controller de aluno
    public class AlunoController
    {
        // lista duplamente encadeada de alunos
        public ListaDuplamenteEncadeada<Aluno> alunos { get; set; }

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
                Node<Aluno> atual = alunos.Cabeca;
                while (atual != null)
                {
                    if (atual.Valor != null && atual.Valor.Matricula == rand)
                    {
                        find = true;
                        break;
                    }
                    atual = atual.Prox;
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
            Node<Aluno> atual = alunos.Cabeca;
            while (atual != null)
            {
                Aluno aluno = atual.Valor;
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
                atual = atual.Prox;
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
