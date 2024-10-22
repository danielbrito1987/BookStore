using AutoMapper;
using BookStore.Domain.DTO;
using BookStore.Domain.Entity;
using BookStore.Infra.Interfaces;
using BookStore.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Servuces.Services
{
    public class LivroService : ILivroService
    {
        private readonly IMapper _mapper;
        private readonly ILivroRepository _repository;

        public LivroService(ILivroRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<LivroDto>> GetAllLivrosAsync()
        {
            return _mapper.Map<IList<LivroDto>>(await _repository.GetLivrosWithAuthorsAsync());
        }

        public async Task<LivroDto> GetLivroByIdAsync(int id)
        {
            return _mapper.Map<LivroDto>(await _repository.GetByIdAsync(id));
        }

        public async Task<LivroDto> AddLivroAsync(LivroDto livroDto)
        {
            var livro = _mapper.Map<Livro>(livroDto);

            foreach (var autorId in livroDto.AutoresIds)
            {
                var livroAutor = new LivroAutor { CodAutor = autorId };
                livro.LivroAutores.Add(livroAutor);
            }

            foreach (var assuntoId in livroDto.AssuntosIds)
            {
                var livroAssunto = new LivroAssunto { CodAssunto = assuntoId };
                livro.LivroAssuntos.Add(livroAssunto);
            }

            await _repository.AddAsync(livro);
            await _repository.SaveAsync();

            return _mapper.Map<LivroDto>(livro);
        }

        public async Task UpdateLivroAsync(LivroDto livroDto)
        {
            var livro = _mapper.Map<Livro>(livroDto);

            _repository.Update(livro);
            await _repository.SaveAsync();
        }

        public async Task DeleteLivroAsync(int id)
        {
            var livro = await _repository.GetByIdAsync(id);
            if (livro != null)
            {
                _repository.Delete(livro);
                await _repository.SaveAsync();
            }
        }
    }
}
