using BookStore.Domain.Entity;
using BookStore.Infra.Interfaces;
using BookStore.Infra.Repositories.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Infra.Repositories
{
    public class AssuntoRepository : BaseRepository<Assunto>, IAssuntoRepository
    {
        private readonly ApplicationDbContext _context;

        public AssuntoRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }
    }
}
