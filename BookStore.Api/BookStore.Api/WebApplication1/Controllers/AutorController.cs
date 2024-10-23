using AutoMapper;
using BookStore.Api.Commands;
using BookStore.Domain.Entity;
using BookStore.Services.DTO;
using BookStore.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AutorController : ControllerBase
    {
        private readonly IAutorService _autorService;
        private readonly IMapper _mapper;

        public AutorController(IAutorService autorService, IMapper mapper)
        {
            _autorService = autorService;
            _mapper = mapper;
        }

        // GET: api/Autores
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AutorDto>>> GetAutores()
        {
            var autores = await _autorService.GetAllAsync();

            return Ok(autores);
        }

        // GET: api/Autores/5
        [HttpGet("{id}")]
        public async Task<ActionResult<AutorDto>> GetAutor(int id)
        {
            var autor = await _autorService.GetByIdAsync(id);
            if (autor == null)
            {
                return BadRequest("Autor não encontrado!");
            }

            return Ok(autor);
        }

        // POST: api/Autores
        [HttpPost]
        public async Task<ActionResult<Autor>> PostAutor([FromBody] CreateAutorCommand command)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var autorDto = _mapper.Map<AutorDto>(command);

            await _autorService.AddAsync(autorDto);

            return CreatedAtAction(nameof(GetAutor), new { id = autorDto.CodAutor }, autorDto);
        }

        // PUT: api/Autores/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAutor(int id, [FromBody] UpdateAutorCommand command)
        {
            if (id != command.CodAutor)
            {
                return BadRequest();
            }

            var autorDto = _mapper.Map<AutorDto>(command);

            var msg = await _autorService.UpdateAsync(autorDto);

            if(msg != "OK")
            {
                return BadRequest(msg);
            }

            return NoContent();
        }

        // DELETE: api/Autores/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAutor(int id)
        {
            try
            {
                var msg = await _autorService.DeleteAsync(id);

                if (msg != "OK")
                {
                    return BadRequest(msg);
                }

                return NoContent();
            }
            catch (DbUpdateException ex)
            {
                if (ex.InnerException != null)
                {
                    if (ex.InnerException.Message.ToUpper().Contains("FOREIGN KEY CONSTRAINT"))
                    {
                        return BadRequest("Não é possível excluir este autor, pois existem registros relacionados.");
                    }
                }

                return StatusCode(500, "Ocorreu um erro ao tentar excluir o autor.");
            }
        }
    }
}
