using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SistemaNotasAlunos.View;

namespace SistemaNotasAlunos.Controller
{
    public class MenuController
    {
        public AlunoController ac { get; set; }
        public DisciplinaController dc { get; set; }
        public MatriculaController mc { get; set; }

        public MenuController()
        {
            ac = new AlunoController();
            dc = new DisciplinaController();
            mc = new MatriculaController();
        }

        // CORREÇÃO 3: "int opcao = MenuView.ExibirMenuPrincipal()" foi removido do campo.
        // Como campo inicializador, ele era executado durante o new MenuController(),
        // mas MenuView possui "static MenuController menuController = new MenuController()",
        // criando dependência circular: MenuView inicializa MenuController que chama MenuView,
        // onde menuController ainda é null — causando NullReferenceException na primeira opção escolhida.
        public void MostrarSubMenu()
        {
            int opcao = MenuView.ExibirMenuPrincipal();

            switch (opcao)
            {
                case 1:
                    MenuView.ExibirMenuConsultas();
                    opcao = 0;
                    break;
                case 2:
                    MenuView.ExibirMenuCadastros();
                    opcao = 0;
                    break;
                case 3:
                    // salvar
                    opcao = 0;
                    break;
                case 4:
                    // sair
                    break;
            }
        }
    }
}