using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Domain.Entity
{
    public class LivroReport
    {
        public int CodLivro { get; set; }
        public string Titulo { get; set; }
        public int CodAutor { get; set; }
        public string NomeAutor { get; set; }
        public string Assuntos { get; set; }
        public int Edicao { get; set; }
        public int AnoPublicacao { get; set; }
    }

    public class AutorReport
    {
        public int CodAutor { get; set; }
        public string Nome { get; set; }
        public List<LivroReport> Livros { get; set; } = new List<LivroReport>();
    }
}
