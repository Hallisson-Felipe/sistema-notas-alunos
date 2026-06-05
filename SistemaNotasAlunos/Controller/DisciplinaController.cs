using System;
using SistemaNotasAlunos.Model;

namespace SistemaNotasAlunos.Controller
{
    // controller de disciplinas
    public class DisciplinaController
    {
        // lista duplamente encadeada de disciplinas
        public ListaDuplamenteEncadeada<Disciplina> disciplinas { get; set; }

        private ArquivoController ar = new ArquivoController();

        // construtor: carrega as disciplinas do arquivo ao iniciar
        public DisciplinaController()
        {
            disciplinas = ar.LerDisciplinas();
        }

        // cadastra disciplina
        public void Cadastro(string nome, double notaMinima)
        {
            Disciplina novaDisciplina = new Disciplina
            {
                Nome = nome,
                NotaMinima = notaMinima,
                Codigo = GerarCodigo()
            };

            // adiciona na lista
            disciplinas.Adicionar(novaDisciplina);
        }

        // gera codigo de 3 digitos sem repetir
        public int GerarCodigo()
        {
            int rand;
            while (true)
            {
                bool find = false;
                rand = Random.Shared.Next(100, 1000);

                // percorre a lista procurando se o codigo ja existe
                Node<Disciplina> atual = disciplinas.Cabeca;
                while (atual != null)
                {
                    if (atual.Valor != null && atual.Valor.Codigo == rand)
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

        // busca disciplina por nome ou por codigo
        public Disciplina Buscar(string nome, int codigo)
        {
            // percorre a lista do inicio ao fim
            Node<Disciplina> atual = disciplinas.Cabeca;
            while (atual != null)
            {
                Disciplina d = atual.Valor;
                if (d != null)
                {
                    // ve se o nome bate (ignora maiuscula/minuscula)
                    if (!string.IsNullOrEmpty(nome) && d.Nome.Trim().ToLower() == nome.Trim().ToLower())
                    {
                        return d;
                    }

                    // ve se o codigo bate
                    if (codigo != -1 && d.Codigo == codigo)
                    {
                        return d;
                    }
                }
                atual = atual.Prox;
            }
            return null;
        }

        // grava as disciplinas
        public void GravarDisciplinas()
        {
            ar.GravarDisciplinas(disciplinas);
        }
    }
}
