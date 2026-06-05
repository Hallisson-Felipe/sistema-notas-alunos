using System;
using System.IO;
using SistemaNotasAlunos.Model;

namespace SistemaNotasAlunos.Controller
{
    // controller de arquivos dat
    public class ArquivoController
    {
        // caminhos completos dos arquivos de dados
        private readonly string caminhoAlunos = ObterCaminho("Alunos.dat");
        private readonly string caminhoDisciplinas = ObterCaminho("Disciplinas.dat");
        private readonly string caminhoMatriculas = ObterCaminho("Matriculas.dat");

        // obtem o caminho absoluto do arquivo resolvendo para a pasta do projeto ou para a pasta local
        private static string ObterCaminho(string nomeArquivo)
        {
            string baseDir = AppDomain.CurrentDomain.BaseDirectory;
            
            string projetoDir = Path.GetFullPath(Path.Combine(baseDir, "..", "..", ".."));
            // define a pasta Data dentro do diretorio do projeto
            string dataDirNoProjeto = Path.Combine(projetoDir, "Data");

            // se a pasta Data existe no projeto, retorna o caminho do arquivo la dentro
            if (Directory.Exists(dataDirNoProjeto))
            {
                return Path.Combine(dataDirNoProjeto, nomeArquivo);
            }

            // se nao encontrar, retorna o caminho local
            return Path.Combine(baseDir, "Data", nomeArquivo);
        }

        // le alunos do arquivo dat e retorna uma lista encadeada
        public ListaDuplamenteEncadeada<Aluno> LerAlunos()
        {
            var alunos = new ListaDuplamenteEncadeada<Aluno>();

            // se o arquivo de alunos nao existe, retorna a lista vazia
            if (!File.Exists(caminhoAlunos))
            {
                return alunos;
            }

            using (StreamReader sr = new StreamReader(caminhoAlunos))
            {
                string linha;
                // le linha por linha ate o fim do arquivo
                while ((linha = sr.ReadLine()) != null)
                {
                    // ignora linhas vazias ou espacos em branco
                    if (string.IsNullOrWhiteSpace(linha)) continue;

                    // cada linha tem: matricula;nome;idade
                    var partes = linha.Split(';');
                    // se tiver todas as partes, parseia e adiciona
                    if (partes.Length >= 3)
                    {
                        var aluno = new Aluno
                        {
                            Matricula = int.Parse(partes[0]),
                            Nome = partes[1],
                            Idade = int.Parse(partes[2])
                        };
                        // adiciona o aluno criado na lista
                        alunos.Adicionar(aluno);
                    }
                }
            }

            // retorna a lista de alunos populada
            return alunos;
        }

        // le disciplinas do arquivo dat e retorna uma lista encadeada
        public ListaDuplamenteEncadeada<Disciplina> LerDisciplinas()
        {
            var disciplinas = new ListaDuplamenteEncadeada<Disciplina>();

            // se o arquivo de disciplinas nao existe, retorna a lista vazia
            if (!File.Exists(caminhoDisciplinas))
            {
                return disciplinas;
            }

            using (StreamReader sr = new StreamReader(caminhoDisciplinas))
            {
                string linha;
                // le linha por linha ate o fim do arquivo
                while ((linha = sr.ReadLine()) != null)
                {
                    // ignora linhas vazias ou espacos em branco
                    if (string.IsNullOrWhiteSpace(linha)) continue;

                    // cada linha tem: codigo;nome;notaMinima
                    var partes = linha.Split(';');
                    // se tiver todas as partes, parseia e adiciona
                    if (partes.Length >= 3)
                    {
                        var disciplina = new Disciplina
                        {
                            Codigo = int.Parse(partes[0]),
                            Nome = partes[1],
                            NotaMinima = double.Parse(partes[2])
                        };
                        // adiciona a disciplina criada na lista
                        disciplinas.Adicionar(disciplina);
                    }
                }
            }

            // retorna a lista de disciplinas populada
            return disciplinas;
        }

        // le matriculas do arquivo dat, ligando cada uma ao aluno e disciplina certos
        public ListaDuplamenteEncadeada<Matricula> LerMatricula(ListaDuplamenteEncadeada<Aluno> alunos, ListaDuplamenteEncadeada<Disciplina> disciplinas)
        {
            var matriculas = new ListaDuplamenteEncadeada<Matricula>();

            // se o arquivo de matriculas nao existe, retorna a lista vazia
            if (!File.Exists(caminhoMatriculas))
            {
                return matriculas;
            }

            using (StreamReader sr = new StreamReader(caminhoMatriculas))
            {
                string linha;
                // le linha por linha ate o fim do arquivo
                while ((linha = sr.ReadLine()) != null)
                {
                    // ignora linhas vazias ou espacos em branco
                    if (string.IsNullOrWhiteSpace(linha)) continue;

                    // cada linha tem: codigoDisciplina;matriculaAluno;nota1;nota2
                    var partes = linha.Split(';');
                    // se tiver todas as partes, parseia e processa
                    if (partes.Length >= 4)
                    {
                        int codigoDisciplina = int.Parse(partes[0]);
                        int matriculaAluno = int.Parse(partes[1]);
                        double nota1 = double.Parse(partes[2]);
                        double nota2 = double.Parse(partes[3]);

                        // busca a disciplina correspondente percorrendo a lista
                        Disciplina disc = null;
                        Node<Disciplina> atualDisc = disciplinas.Cabeca;
                        while (atualDisc != null)
                        {
                            if (atualDisc.Valor != null && atualDisc.Valor.Codigo == codigoDisciplina)
                            {
                                disc = atualDisc.Valor;
                                break;
                            }
                            atualDisc = atualDisc.Prox;
                        }

                        // busca o aluno correspondente percorrendo a lista
                        Aluno alu = null;
                        Node<Aluno> atualAlu = alunos.Cabeca;
                        while (atualAlu != null)
                        {
                            if (atualAlu.Valor != null && atualAlu.Valor.Matricula == matriculaAluno)
                            {
                                alu = atualAlu.Valor;
                                break;
                            }
                            atualAlu = atualAlu.Prox;
                        }

                        // so adiciona se encontrou os dois
                        if (disc != null && alu != null)
                        {
                            var m = new Matricula
                            {
                                aluno = alu,
                                disciplina = disc,
                                Nota1 = nota1,
                                Nota2 = nota2
                            };
                            // adiciona a matricula criada na lista
                            matriculas.Adicionar(m);
                        }
                    }
                }
            }

            // retorna a lista de matriculas populada
            return matriculas;
        }

        // grava os alunos no arquivo percorrendo a lista encadeada
        public void GravarAlunos(ListaDuplamenteEncadeada<Aluno> alunos)
        {
            // garante que a pasta Data exista antes de gravar
            Directory.CreateDirectory(Path.GetDirectoryName(caminhoAlunos));

            using (StreamWriter sw = new StreamWriter(caminhoAlunos))
            {
                Node<Aluno> atual = alunos.Cabeca;
                while (atual != null)
                {
                    Aluno alu = atual.Valor;
                    if (alu != null)
                    {
                        sw.WriteLine($"{alu.Matricula};{alu.Nome};{alu.Idade}");
                    }
                    atual = atual.Prox;
                }
            }
        }

        // grava as disciplinas no arquivo percorrendo a lista encadeada
        public void GravarDisciplinas(ListaDuplamenteEncadeada<Disciplina> disciplinas)
        {
            // garante que a pasta Data exista antes de gravar
            Directory.CreateDirectory(Path.GetDirectoryName(caminhoDisciplinas));

            using (StreamWriter sw = new StreamWriter(caminhoDisciplinas))
            {
                Node<Disciplina> atual = disciplinas.Cabeca;
                while (atual != null)
                {
                    Disciplina disc = atual.Valor;
                    if (disc != null)
                    {
                        sw.WriteLine($"{disc.Codigo};{disc.Nome};{disc.NotaMinima}");
                    }
                    atual = atual.Prox;
                }
            }
        }

        // grava as matriculas no arquivo percorrendo a lista encadeada
        public void GravarMatriculas(ListaDuplamenteEncadeada<Matricula> matriculas)
        {
            // garante que a pasta Data exista antes de gravar
            Directory.CreateDirectory(Path.GetDirectoryName(caminhoMatriculas));

            using (StreamWriter sw = new StreamWriter(caminhoMatriculas))
            {
                Node<Matricula> atual = matriculas.Cabeca;
                while (atual != null)
                {
                    Matricula m = atual.Valor;
                    if (m != null && m.disciplina != null && m.aluno != null)
                    {
                        sw.WriteLine($"{m.disciplina.Codigo};{m.aluno.Matricula};{m.Nota1};{m.Nota2}");
                    }
                    atual = atual.Prox;
                }
            }
        }
    }
}