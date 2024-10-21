using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Domain.Entity
{
    public class LivroAssunto
    {
        public int CodLivro { get; set; }
        public int CodAssunto { get; set; }

        public Livro Livro { get; set; }
        public Assunto Assunto { get; set; }
    }
}
