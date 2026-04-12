using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SistemaNotasAlunos.Model;

namespace SistemaNotasAlunos.Controller
{
    public class DisciplinaController
    {
        public Disciplina[] disciplinas { get; set; }
        int qtdDisciplinas = 0;

        //instancia o controller de arquivo para ler as disciplinas do arquivo e preencher o vetor de alunos
        ArquivoController ar = new ArquivoController();


        public DisciplinaController()
        {
            var disciplinasLidas = ar.LerDisciplinas();
            disciplinas = new Disciplina[100];
            qtdDisciplinas = 0;
            if(disciplinasLidas != null)
            {
                for (int i = 0; i < disciplinasLidas.Length; i++)
                {
                    disciplinas[i] = disciplinasLidas[i];
                    qtdDisciplinas++;
                }
            }
        }

        //cadastra disciplina
        public void Cadastro(string nome, double notaMinima)
        {
            disciplinas[qtdDisciplinas] = new Disciplina();
            disciplinas[qtdDisciplinas].Nome = nome;
            disciplinas[qtdDisciplinas].NotaMinima = notaMinima;
            disciplinas[qtdDisciplinas].Codigo = GerarCodigo();
            qtdDisciplinas++;
        }


        //gera codigo de disciplina aleatorio de 3 digitos e veficica se nao ha duplicidade
        public int GerarCodigo()
        {
            int rand;
            while (true)
            {
                bool find = false;
                rand = Random.Shared.Next(100, 1000);
                for (int i = 0; i < qtdDisciplinas; i++)
                {
                    if (disciplinas[i].Codigo == rand)
                    {
                        find = true;
                        break;
                    }
                }
                if (find)
                {
                    continue;
                }
                break;
            }
            return rand;
        }

        //busca a disciplina pelo nome ou codigo
        public Disciplina Buscar(string nome, int codigo)
        {
            for (int i = 0; i < qtdDisciplinas; i++)
            {
                if (disciplinas[i].Nome == nome || disciplinas[i].Codigo == codigo)
                {
                    return disciplinas[i];
                }
            }
            return null;
        }
    }
}
