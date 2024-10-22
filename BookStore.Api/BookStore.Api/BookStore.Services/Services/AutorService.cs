using AutoMapper;
using BookStore.Domain.Entity;
using BookStore.Infra.Interfaces;
using BookStore.Services.DTO;
using BookStore.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Services.Services
{
    public class AutorService : IAutorService
    {
        private readonly IAutorRepository _autorRepository;
        private readonly IMapper _mapper;

        public AutorService(IAutorRepository autorRepository, IMapper mapper)
        {
            _autorRepository = autorRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<AutorDto>> GetAllAsync()
        {
            return _mapper.Map<IEnumerable<AutorDto>>(await _autorRepository.GetAllAsync());
        }

        public async Task<AutorDto> GetByIdAsync(int id)
        {
            return _mapper.Map<AutorDto>(await _autorRepository.GetByIdAsync(id));
        }

        public async Task AddAsync(AutorDto autor)
        {
            await _autorRepository.AddAsync(_mapper.Map<Autor>(autor));
            await _autorRepository.SaveAsync();
        }

        public async Task UpdateAsync(AutorDto autor)
        {
            _autorRepository.Update(_mapper.Map<Autor>(autor));
            await _autorRepository.SaveAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var autor = await _autorRepository.GetByIdAsync(id);

            if(autor != null)
            {
                _autorRepository.Delete(autor);
                await _autorRepository.SaveAsync();
            }
        }
    }
}
