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
        //cria os objetos alunoController, disciplinacController e matriculaController
        public AlunoController alunoController { get; set; }
        public DisciplinaController DisciplinaController { get; set; }
        public MatriculaController matriculaController { get; set; }

        //inicializa os objetos alunoController, disciplinacController e matriculaController
        public MenuController()
        {
            alunoController = new AlunoController();
            DisciplinaController = new DisciplinaController();
            matriculaController = new MatriculaController(alunoController, DisciplinaController);
        }

        public void MostrarSubMenu()
        {

            
            int opcao;

            do
            {
                opcao = MenuView.ExibirMenuPrincipal();

                switch (opcao)
                {

                    //chama o menu de consultas
                    case 1:
                        MenuView.ExibirMenuConsultas(this);
                        break;


                    //chama o menu de cadastros
                    case 2:
                        MenuView.ExibirMenuCadastros(this);
                        break;


                    //salva tudo nos arquivos dat
                    case 3:
                        alunoController.GravarAlunos();
                        DisciplinaController.GravarDisciplinas();
                        matriculaController.GravarMatriculas();

                        Console.WriteLine("Dados salvos com sucesso!");
                        Console.ReadKey();
                        break;


                    //encerra o programa
                    case 4:
                        Console.WriteLine("Saindo...");
                        break;
                }

            } 
            //roda o menu enquanto o usuario nao escolher a opcao de sair
            while (opcao != 4);
        }
    }
}
