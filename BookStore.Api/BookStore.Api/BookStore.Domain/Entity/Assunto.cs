using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Domain.Entity
{
    public class Assunto
    {
        public int CodAssunto { get; set; }
        public string Descricao { get; set; }

        public ICollection<LivroAssunto> LivroAssuntos { get; set; }
    }
}
