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

        public IList<AutorReport> ObterDadosRelatorio(string titulo, int codAutor)
        {
            var listaLivros = new List<LivroReport>();

            using(MySqlConnection conn = new MySqlConnection(_context.Database.GetConnectionString()))
            {
                conn.Open();

                string query = @"SELECT CodLivro, 
                                        Titulo, 
                                        CodAutor, 
                                        NomeAutor, 
                                        Assuntos, 
                                        Edicao, 
                                        AnoPublicacao, 
                                        Valor 
                                FROM vw_Rel_Livros
                                WHERE 1=1 ";

                if (!string.IsNullOrEmpty(titulo))
                {
                    query += $" AND UPPER(Titulo) LIKE '%{titulo.ToUpper()}%' ";
                }

                if(codAutor > 0)
                {
                    query += $" AND CodAutor = {codAutor} ";
                }

                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    MySqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        listaLivros.Add(new LivroReport
                        {
                            CodLivro = reader["CodLivro"] != DBNull.Value ? Convert.ToInt32(reader["CodLivro"]) : 0,
                            Titulo = reader["Titulo"] != DBNull.Value ? reader["Titulo"].ToString() : "-",
                            CodAutor = reader["CodAutor"] != DBNull.Value ? Convert.ToInt32(reader["CodAutor"]) : 0,
                            NomeAutor = reader["NomeAutor"] != DBNull.Value ? reader["NomeAutor"].ToString() : "-",
                            Assuntos = reader["Assuntos"] != DBNull.Value ? reader["Assuntos"].ToString() : "-",
                            Edicao = reader["Edicao"] != DBNull.Value ? Convert.ToInt32(reader["Edicao"]) : 0,
                            AnoPublicacao = reader["AnoPublicacao"] != DBNull.Value ? Convert.ToInt32(reader["AnoPublicacao"]) : 0
                        });
                    }
                }
            }

            var agrupadoPorAutor = listaLivros.GroupBy(l => l.CodAutor)
                .Select(grupo => new AutorReport
                {
                    CodAutor = grupo.Key,
                    Nome = grupo.First().NomeAutor,
                    Livros = grupo.Select(livro => new LivroReport
                    {
                        CodLivro = livro.CodLivro,
                        Titulo = livro.Titulo,
                        Assuntos = livro.Assuntos,
                        Edicao = livro.Edicao,
                        AnoPublicacao = livro.AnoPublicacao
                    }).ToList()
                }).ToList();

            return agrupadoPorAutor;
        }
    }
}
