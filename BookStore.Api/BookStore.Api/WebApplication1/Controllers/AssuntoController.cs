using BookStore.Services.DTO;
using BookStore.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AssuntoController : ControllerBase
    {
        private readonly IAssuntoService _assuntoService;

        public AssuntoController(IAssuntoService assuntoService)
        {
            _assuntoService = assuntoService;
        }

        /// <summary>
        /// Obter todos os registros de assunto.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AssuntoDto>>> GetAussuntos()
        {
            var autores = await _assuntoService.GetAllAsync();
            return Ok(autores);
        }

        /// <summary>
        /// Obter um assunto pelo ID.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<AssuntoDto>> GetAssunto(int id)
        {
            var autor = await _assuntoService.GetByIdAsync(id);
            if (autor == null)
            {
                return NotFound();
            }
            return Ok(autor);
        }

        /// <summary>
        /// Inserir um registro de assunto na base.
        /// </summary>
        /// <param name="assunto"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<AssuntoDto>> PostAssunto([FromBody] AssuntoDto assunto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _assuntoService.AddAsync(assunto);

            return CreatedAtAction(nameof(GetAssunto), new { id = assunto.CodAssunto }, assunto);
        }

        /// <summary>
        /// Alterar um registro de assunto na base.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="assunto"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAssunto(int id, [FromBody] AssuntoDto assunto)
        {
            if (id != assunto.CodAssunto)
            {
                return BadRequest();
            }

            await _assuntoService.UpdateAsync(assunto);
            return NoContent();
        }

        /// <summary>
        /// Deletar um assunto da base.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAssunto(int id)
        {
            await _assuntoService.DeleteAsync(id);
            return NoContent();
        }
    }
}
