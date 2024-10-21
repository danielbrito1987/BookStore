using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Domain.Entity
{
    public class LivroAutor
    {
        public int CodLivro { get; set; }
        public int CodAutor { get; set; }

        public Livro Livro { get; set; }
        public Autor Autor { get; set; }
    }
}
