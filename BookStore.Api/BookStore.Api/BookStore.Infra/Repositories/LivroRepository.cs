using BookStore.Domain.Entity;
using BookStore.Infra.Interfaces;
using BookStore.Infra.Repositories.Base;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Infra.Repositories
{
    public class LivroRepository : BaseRepository<Livro>, ILivroRepository
    {
        private readonly ApplicationDbContext _context;

        public LivroRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Livro>> GetLivrosWithAuthorsAsync()
        {
            return await _context.Livros
                .Include(p => p.Precos)
                .Include(a => a.LivroAssuntos)
                .Include(b => b.LivroAutores)
                .ThenInclude(la => la.Autor)
                .ToListAsync();
        }
    }
}
