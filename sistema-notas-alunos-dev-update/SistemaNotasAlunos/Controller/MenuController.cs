using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SistemaNotasAlunos.View;

namespace SistemaNotasAlunos.Controller
{
    public class MenuController
    {
        public AlunoController ac {  get; set; }
        public DisciplinaController dc {  get; set; }
        public MatriculaController mc {  get; set; }

        public MenuController()
        {
            ac = new AlunoController();
            dc = new DisciplinaController();
            mc = new MatriculaController();
        }

        int opcao = MenuView.ExibirMenuPrincipal();

        public void MostrarSubMenu()
        {
            switch (opcao)
            {
                
                case 1:
                    MenuView.ExibirMenuConsultas();
                    break;
                case 2:
                    int cadastro = MenuView.ExibirMenuCadastros();
                    Console.WriteLine($"Cadastro escolhido: {cadastro}");
                    break;
                case 3:
                    //salvar
                    break;
                case 4:
                    //sair
                    break;
            }

        }
    }
}
