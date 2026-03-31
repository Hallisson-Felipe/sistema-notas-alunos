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
        Disciplina[] disciplinas = new Disciplina[100];
        int qtdDisciplinas = 0;
        
        public  Disciplina[] GetDisciplinas()
        {
            return disciplinas;
        }

        public void Cadastro(string nome, double notaMinima)
        {
            disciplinas[qtdDisciplinas].Nome = nome;
            disciplinas[qtdDisciplinas].NotaMinima = notaMinima;
            disciplinas[qtdDisciplinas].Codigo = GerarCodigo();
            
        }

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
    }
}
