using BookStore.Services.DTO;
using BookStore.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Services.Services
{
    public class DashboardService : IDashboardService
    {
        private readonly ILivroService _livroService;
        private readonly IAutorService _autorService;
        private readonly IAssuntoService _assuntoService;

        public DashboardService(ILivroService livroService, IAutorService autorService, IAssuntoService assuntoService)
        {
            _livroService = livroService;
            _autorService = autorService;
            _assuntoService = assuntoService;
        }

        public async Task<DashboardDto> GetDashboard()
        {
            DashboardDto dto = new DashboardDto();

            var livros = await _livroService.GetAllLivrosAsync();
            var autores = await _autorService.GetAllAsync();
            var assuntos = await _assuntoService.GetAllAsync();

            dto.QtdAutores = autores != null && autores.Count() > 0 ? autores.Count() : 0;
            dto.QtdLivros = livros != null && livros.Count() > 0 ? livros.Count() : 0;
            dto.QtdAssuntos = assuntos != null && assuntos.Count() > 0 ? assuntos.Count() : 0;

            return dto;
        }
    }
}
