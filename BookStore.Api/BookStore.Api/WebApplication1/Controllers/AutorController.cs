using BookStore.Domain.Entity;
using BookStore.Services.DTO;
using BookStore.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AutorController : ControllerBase
    {
        private readonly IAutorService _autorService;

        public AutorController(IAutorService autorService)
        {
            _autorService = autorService;
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
                return NotFound();
            }
            return Ok(autor);
        }

        // POST: api/Autores
        [HttpPost]
        public async Task<ActionResult<Autor>> PostAutor([FromBody] AutorDto autor)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _autorService.AddAsync(autor);

            return CreatedAtAction(nameof(GetAutor), new { id = autor.CodAutor }, autor);
        }

        // PUT: api/Autores/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAutor(int id, [FromBody] AutorDto autor)
        {
            if (id != autor.CodAutor)
            {
                return BadRequest();
            }

            await _autorService.UpdateAsync(autor);
            return NoContent();
        }

        // DELETE: api/Autores/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAutor(int id)
        {
            await _autorService.DeleteAsync(id);
            return NoContent();
        }
    }
}
