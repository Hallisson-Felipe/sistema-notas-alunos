using System;

namespace SistemaNotasAlunos.Model
{

    // No para a lista de alunos
    public class NodeAluno
    {
        public Aluno Value { get; set; }
        public NodeAluno Next { get; set; } // aponta pro proximo no
        public NodeAluno Prev { get; set; } // aponta pro no anterior

        public NodeAluno(Aluno value)
        {
            Value = value;
            Next = null;
            Prev = null;
        }
    }

    // lista duplamente encadeada de alunos
    public class ListaAlunos
    {
        public NodeAluno Head { get; private set; } // primeiro da lista
        public NodeAluno Tail { get; private set; } // ultimo da lista
        public int Count { get; private set; }

        public ListaAlunos()
        {
            Head = null;
            Tail = null;
            Count = 0;
        }

        // adiciona o aluno no fim
        public void Adicionar(Aluno valor)
        {
            NodeAluno novoNo = new NodeAluno(valor);

            if (Head == null)
            {
                Head = novoNo;
                Tail = novoNo;
            }
            else
            {
                Tail.Next = novoNo;
                novoNo.Prev = Tail;
                Tail = novoNo;
            }

            Count++;
        }
    }

    // No para a lista de disciplinas
    public class NodeDisciplina
    {
        public Disciplina Value { get; set; }
        public NodeDisciplina Next { get; set; } // aponta pro proximo no
        public NodeDisciplina Prev { get; set; } // aponta pro no anterior

        public NodeDisciplina(Disciplina value)
        {
            Value = value;
            Next = null;
            Prev = null;
        }
    }

    // lista duplamente encadeada de disciplinas
    public class ListaDisciplinas
    {
        public NodeDisciplina Head { get; private set; } // primeiro da lista
        public NodeDisciplina Tail { get; private set; } // ultimo da lista
        public int Count { get; private set; }

        public ListaDisciplinas()
        {
            Head = null;
            Tail = null;
            Count = 0;
        }

        // adiciona a disciplina no fim
        public void Adicionar(Disciplina valor)
        {
            NodeDisciplina novoNo = new NodeDisciplina(valor);

            if (Head == null)
            {
                Head = novoNo;
                Tail = novoNo;
            }
            else
            {
                Tail.Next = novoNo;
                novoNo.Prev = Tail;
                Tail = novoNo;
            }

            Count++;
        }
    }

    // No para a lista de matriculas
    public class NodeMatricula
    {
        public Matricula Value { get; set; }
        public NodeMatricula Next { get; set; } // aponta pro proximo no
        public NodeMatricula Prev { get; set; } // aponta pro no anterior

        public NodeMatricula(Matricula value)
        {
            Value = value;
            Next = null;
            Prev = null;
        }
    }

    // lista duplamente encadeada de matriculas
    public class ListaMatriculas
    {
        public NodeMatricula Head { get; private set; } // primeiro da lista
        public NodeMatricula Tail { get; private set; } // ultimo da lista
        public int Count { get; private set; }

        public ListaMatriculas()
        {
            Head = null;
            Tail = null;
            Count = 0;
        }

        // adiciona a matricula no fim
        public void Adicionar(Matricula valor)
        {
            NodeMatricula novoNo = new NodeMatricula(valor);

            if (Head == null)
            {
                Head = novoNo;
                Tail = novoNo;
            }
            else
            {
                Tail.Next = novoNo;
                novoNo.Prev = Tail;
                Tail = novoNo;
            }

            Count++;
        }
    }
}
