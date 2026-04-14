using SistemaNotasAlunos.Controller;
using SistemaNotasAlunos.View;

namespace SistemaNotasAlunos
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //inicializa o programa
            MenuController menuController = new MenuController();
            menuController.MostrarSubMenu();
        }
    }
}