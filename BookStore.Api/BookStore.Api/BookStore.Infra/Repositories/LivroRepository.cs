using BookStore.Domain.Entity;
using BookStore.Infra.Interfaces;
using BookStore.Infra.Repositories.Base;
using Microsoft.EntityFrameworkCore;
using MySqlConnector;
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

        public IList<LivroReport> ObterDadosRelatorio()
        {
            var listaLivros = new List<LivroReport>();

            using(MySqlConnection conn = new MySqlConnection(_context.Database.GetConnectionString()))
            {
                conn.Open();

                string query = "SELECT titulo, autores, assuntos, edicao, anoPublicacao, valor FROM vw_Rel_Livros";

                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    MySqlDataReader reader = cmd.ExecuteReaderAsync().Result;

                    while (reader.Read())
                    {
                        listaLivros.Add(new LivroReport
                        {
                            Titulo = reader["titulo"] != DBNull.Value ? reader["titulo"].ToString() : "-",
                            Autores = reader["autores"] != DBNull.Value ? reader["autores"].ToString() : "-",
                            Assuntos = reader["assuntos"] != DBNull.Value ? reader["assuntos"].ToString() : "-",
                            Edicao = reader["edicao"] != DBNull.Value ? Convert.ToInt32(reader["edicao"]) : 0,
                            AnoPublicacao = reader["anoPublicacao"] != DBNull.Value ? Convert.ToInt32(reader["anoPublicacao"]) : 0,
                            Valor = reader["valor"] != DBNull.Value ? Convert.ToDecimal(reader["valor"]) : 0
                        });
                    }
                }
            }

            return listaLivros;
        }
    }
}
