using BookStore.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Infra.Interfaces
{
    public interface ILivroRepository : IBaseRepository<Livro>
    {
        Task<IEnumerable<Livro>> GetLivrosWithAuthorsAsync();
        IList<AutorReport> ObterDadosRelatorio(string titulo, int codAutor);
    }
}
