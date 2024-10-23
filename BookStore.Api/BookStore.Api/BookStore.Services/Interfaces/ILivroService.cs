using BookStore.Domain.DTO;
using BookStore.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Services.Interfaces
{
    public interface ILivroService
    {
        Task<IEnumerable<LivroDto>> GetAllLivrosAsync();
        Task<LivroDto> GetLivroByIdAsync(int id);
        Task<LivroDto> AddLivroAsync(LivroDto livro);
        Task<string> UpdateLivroAsync(LivroDto livro);
        Task<string> DeleteLivroAsync(int id);
        Task<byte[]> GerarRelatorioPDF(RelatorioFilter filter);
    }
}
