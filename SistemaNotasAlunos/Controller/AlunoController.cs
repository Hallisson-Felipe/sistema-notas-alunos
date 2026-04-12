using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SistemaNotasAlunos.Model;

namespace SistemaNotasAlunos.Controller
{
    public class AlunoController
    {
        private Aluno[] alunos = new Aluno[100];
        int qtdAlunos = 0;
        ArquivoController ar = new ArquivoController();
        public AlunoController()
        {
            var alunosLidos = ar.LerAlunos();

            qtdAlunos = 0;

            for (int i = 0; i < alunosLidos.Length; i++)
            {
                alunos[i] = alunosLidos[i];
                qtdAlunos++;
            }
        }

        public Aluno[] GetAlunos() { 
            return alunos;
        }
        
        //cadastra aluno
        public void Cadastrar(string nome, int idade)
        {
            alunos[qtdAlunos].Idade = idade;
            alunos[qtdAlunos].Nome = nome;
            alunos[qtdAlunos].Matricula = GerarMatricula();
            qtdAlunos++;
            AtualizarQuantidade();
        }

        //gera a matricula e verifica se nao ha duplicidade
        public int GerarMatricula()
        {
            int rand;
            while (true)
            {
                bool find = false;
                rand = Random.Shared.Next(100, 1000);
                for (int i = 0; i < qtdAlunos; i++)
                {
                    if (alunos[i].Matricula == rand)
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

        //busca aluno pelo nome ou pela matricula.
        public Aluno Buscar(string nome, int matricula)
        {
            foreach (Aluno aluno in alunos)
            {
                //se encontrar retonar o aluno
                if (aluno.Nome == nome)
                {
                    return aluno;
                }
                if (aluno.Matricula == matricula)
                {
                    return aluno;
                }
            }
            //caso nao encontre retorna nulo
            return null;
        }
        public void AtualizarQuantidade()
        {
            qtdAlunos = 0;

            for (int i = 0; i < alunos.Length; i++)
            {
                if (alunos[i] != null)
                    qtdAlunos++;
            }
        }



    }
}
