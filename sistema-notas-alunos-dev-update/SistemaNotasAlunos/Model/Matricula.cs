using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaNotasAlunos.Model
{
    public class Matricula
    {
        public Aluno aluno { get; set; }
        public Disciplina disciplina { get; set; }
        public double Nota1 { get; set; }
        public double Nota2 { get; set; }

        public Matricula()
        {
            aluno = new Aluno();
            disciplina = new Disciplina();

        }
        public double CalcularMedia()
        {
            return (Nota1 + Nota2) / 2;
        }
    }
}
