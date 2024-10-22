using BookStore.Domain.Entity;
using BookStore.Infra.Mapping;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Infra
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
        {
        }

        public DbSet<Livro> Livros { get; set; }
        public DbSet<Autor> Autores { get; set; }
        public DbSet<Assunto> Assuntos { get; set; }
        public DbSet<LivroAssunto> LivrosAssuntos { get; set; }
        public DbSet<LivroAutor> LivrosAutors { get; set; }
        public DbSet<PrecoLivro> PrecosLivros { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new LivroMap());
            modelBuilder.ApplyConfiguration(new AutorMap());
            modelBuilder.ApplyConfiguration(new AssuntoMap());
            modelBuilder.ApplyConfiguration(new LivroAssuntoMap());
            modelBuilder.ApplyConfiguration(new LivroAutorMap());
            modelBuilder.ApplyConfiguration(new PrecoLivroMap());
        }
    }
}
