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
        public Aluno[] alunos {  get; set; }
        int qtdAlunos = 0;

        //instancia o controller de arquivo para ler os alunos do arquivo e preencher o vetor de alunos
        ArquivoController ar = new ArquivoController();

        //construtor do controller de aluno, le os alunos do arquivo e preenche o vetor de alunos
        public AlunoController()
        {
            var alunosLidos = ar.LerAlunos();
            alunos = new Aluno[100];
            qtdAlunos = 0;

            for (int i = 0; i < alunosLidos.Length; i++)
            {
                alunos[i] = alunosLidos[i];
                qtdAlunos++;
            }
        }



        //cadastra aluno
        public void Cadastrar(string nome, int idade)
        {
            alunos[qtdAlunos] = new Aluno();

            alunos[qtdAlunos].Idade = idade;
            alunos[qtdAlunos].Nome = nome;
            alunos[qtdAlunos].Matricula = GerarMatricula();

            qtdAlunos++;
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
        public Aluno Buscar(string nome = "", int matricula = -1)
        {
            foreach (Aluno aluno in alunos)
            {
                if (aluno == null)
                { 
                    continue; 
                }

                if (aluno.Nome.Trim().Equals(nome.Trim(), StringComparison.OrdinalIgnoreCase))
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

        //grava os alunos no arquivo
        public void GravarAlunos()
        {
            Aluno[] alunosValidos = new Aluno[qtdAlunos];

            for (int i = 0; i < qtdAlunos; i++)
            {
                alunosValidos[i] = alunos[i];
            }

            ar.GravarAlunos(alunosValidos);
        }

    }
}
