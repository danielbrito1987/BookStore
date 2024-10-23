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
        Task UpdateLivroAsync(LivroDto livro);
        Task DeleteLivroAsync(int id);
        Task<byte[]> GerarRelatorioPDF();
    }
}
