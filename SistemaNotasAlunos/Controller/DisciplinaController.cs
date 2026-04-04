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

        //cadastra disciplina
        public void Cadastro(string nome, double notaMinima)
        {
            disciplinas[qtdDisciplinas].Nome = nome;
            disciplinas[qtdDisciplinas].NotaMinima = notaMinima;
            disciplinas[qtdDisciplinas].Codigo = GerarCodigo();
            
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
            foreach (Disciplina disciplina in disciplinas)
            {
                //se encontrar retonar a disciplina
                if (disciplina.Nome == nome)
                {
                    return disciplina;
                }
                if (disciplina.Codigo == codigo)
                {
                    return disciplina;
                }
            }
            //caso nao encontre retorna nulo
            return null;
        }
    }
}
