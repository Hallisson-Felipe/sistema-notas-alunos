namespace SistemaNotasAlunos.Model
{
    public class Node<T>
    {
        public T Valor { get; set; }
        public Node<T> Prox { get; set; } // aponta pro proximo no
        public Node<T> Ant { get; set; } // aponta pro no anterior

        public Node(T value)
        {
            Valor = value;
            Prox = null;
            Ant = null;
        }
    }
}
