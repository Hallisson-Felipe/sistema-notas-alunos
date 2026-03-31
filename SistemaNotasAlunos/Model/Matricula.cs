using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaNotasAlunos.Model
{
    public class Matricula
    {
        public int MatriculaAluno { get; set; }
        public int CodigoDisciplina { get; set; }
        public double Nota1 { get; set; }
        public double Nota2 { get; set; }

        public double CalcularMedia()
        {
            return (Nota1 + Nota2) / 2;
        }
    }
}
