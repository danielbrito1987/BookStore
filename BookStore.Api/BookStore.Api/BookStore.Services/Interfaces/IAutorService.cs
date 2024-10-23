using BookStore.Domain.Entity;
using BookStore.Infra.Interfaces;
using BookStore.Services.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Services.Interfaces
{
    public interface IAutorService
    {
        Task<IEnumerable<AutorDto>> GetAllAsync();
        Task<AutorDto> GetByIdAsync(int id);
        Task AddAsync(AutorDto autor);
        Task<string> UpdateAsync(AutorDto autor);
        Task<string> DeleteAsync(int id);
    }
}
