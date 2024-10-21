using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Domain.Entity
{
    public class Autor
    {
        public int CodAutor { get; set; }
        public string Nome { get; set; }

        public ICollection<LivroAutor> LivroAutores { get; set; }
    }
}
