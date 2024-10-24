using BookStore.Services.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Services.Interfaces
{
    public interface IAssuntoService
    {
        Task<IEnumerable<AssuntoDto>> GetAllAsync();
        Task<AssuntoDto?> GetByIdAsync(int id);
        Task AddAsync(AssuntoDto autor);
        Task<string> UpdateAsync(AssuntoDto autor);
        Task<string> DeleteAsync(int id);
    }
}
