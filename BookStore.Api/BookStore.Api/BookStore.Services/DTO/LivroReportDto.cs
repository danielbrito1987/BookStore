using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Services.DTO
{
    public class LivroReportDto
    {
        public string Titulo { get; set; }
        public string Autores { get; set; }
        public string Assuntos { get; set; }
        public int Edicao { get; set; }
        public int AnoPublicacao { get; set; }
        public decimal Valor { get; set; }

    }
}
