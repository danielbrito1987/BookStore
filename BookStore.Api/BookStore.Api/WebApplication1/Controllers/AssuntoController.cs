using AutoMapper;
using BookStore.Api.Commands;
using BookStore.Services.DTO;
using BookStore.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AssuntoController : ControllerBase
    {
        private readonly IAssuntoService _assuntoService;
        private readonly IMapper _mapper;

        public AssuntoController(IAssuntoService assuntoService, IMapper mapper)
        {
            _assuntoService = assuntoService;
            _mapper = mapper;
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
        public async Task<ActionResult<AssuntoDto>> PostAssunto([FromBody] CreateAssuntoCommand command)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var assunto = _mapper.Map<AssuntoDto>(command);

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
        public async Task<IActionResult> PutAssunto(int id, [FromBody] UpdateAssuntoCommand command)
        {
            if (id != command.CodAssunto)
            {
                return BadRequest();
            }

            var assunto = _mapper.Map<AssuntoDto>(command);

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
            try
            {
                await _assuntoService.DeleteAsync(id);
                
                return NoContent();
            }
            catch (DbUpdateException ex)
            {
                if (ex.InnerException != null)
                {
                    if (ex.InnerException.Message.ToUpper().Contains("FOREIGN KEY CONSTRAINT"))
                    {
                        return BadRequest("Não é possível excluir este assunto, pois existem registros relacionados.");
                    }
                }

                return StatusCode(500, "Ocorreu um erro ao tentar excluir o assunto.");
            }
        }
    }
}
