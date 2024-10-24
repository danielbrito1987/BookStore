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
    public class AssuntoService : IAssuntoService
    {
        private readonly IAssuntoRepository _assuntoRepository;
        private readonly IMapper _mapper;

        public AssuntoService(IAssuntoRepository assuntoRepository, IMapper mapper)
        {
            _assuntoRepository = assuntoRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<AssuntoDto>> GetAllAsync()
        {
            return _mapper.Map<IEnumerable<AssuntoDto>>(await _assuntoRepository.GetAllAsync());
        }

        public async Task<AssuntoDto?> GetByIdAsync(int id)
        {
            return _mapper.Map<AssuntoDto>(await _assuntoRepository.GetByIdAsync(id));
        }

        public async Task AddAsync(AssuntoDto assunto)
        {
            await _assuntoRepository.AddAsync(_mapper.Map<Assunto>(assunto));
            await _assuntoRepository.SaveAsync();
        }

        public async Task<string> UpdateAsync(AssuntoDto assunto)
        {
            var assuntoExpcted = await _assuntoRepository.GetByIdAsync(assunto.CodAssunto);

            if(assuntoExpcted != null)
            {
                _assuntoRepository.Update(_mapper.Map<Assunto>(assunto));
                await _assuntoRepository.SaveAsync();

                return "OK";
            }
            else
            {
                return "Erro ao alterar o assunto! Não existe assunto associado ao código.";
            }
        }

        public async Task<string> DeleteAsync(int id)
        {
            var assunto = await _assuntoRepository.GetByIdAsync(id);

            if (assunto != null)
            {
                _assuntoRepository.Delete(assunto);
                await _assuntoRepository.SaveAsync();

                return "OK";
            }
            else
            {
                return "Erro ao excluir o assunto! Não existe assunto associado ao código.";
            }
        }
    }
}
