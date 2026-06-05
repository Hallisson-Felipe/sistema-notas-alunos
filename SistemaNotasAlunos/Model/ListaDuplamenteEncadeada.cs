using System;

namespace SistemaNotasAlunos.Model
{

    // Lista duplamente encadeada generica
    public class ListaDuplamenteEncadeada<T>
    {
        public Node<T> Cabeca { get; private set; } // primeiro da lista
        public Node<T> Cauda { get; private set; } // ultimo da lista
        public int Tamanho { get; private set; }

        public ListaDuplamenteEncadeada()
        {
            Cabeca = null;
            Cauda = null;
            Tamanho = 0;
        }

        // adiciona o valor no fim
        public void Adicionar(T valor)
        {
            Node<T> novoNo = new Node<T>(valor);

            if (Cabeca == null)
            {
                Cabeca = novoNo;
                Cauda = novoNo;
            }
            else
            {
                Cauda.Prox = novoNo;
                novoNo.Ant = Cauda;
                Cauda = novoNo;
            }

            Tamanho++;
        }
    }
}
