using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BookStore.Domain.Entity
{
    public class Livro
    {
        public int CodLivro { get; set; }
        public string Titulo { get; set; }
        public string Editora { get; set; }
        public int Edicao { get; set; }
        public string AnoPublicacao { get; set; }

        [JsonIgnore]
        public List<LivroAutor> LivroAutores { get; set; } = new List<LivroAutor>();

        [JsonIgnore]
        public List<LivroAssunto> LivroAssuntos { get; set; } = new List<LivroAssunto>();
    }
}
