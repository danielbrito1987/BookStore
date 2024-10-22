using BookStore.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Domain.Entity
{
    public class PrecoLivro
    {
        public int CodPreco { get; set; }
        public int CodLivro { get; set; }
        public TipoCompraEnum TipoCompra { get; set; }
        public decimal Valor { get; set; }

        public Livro Livro { get; set; }
    }
}
