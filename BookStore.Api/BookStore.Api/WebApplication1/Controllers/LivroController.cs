using AutoMapper;
using BookStore.Api.Commands;
using BookStore.Domain.DTO;
using BookStore.Domain.Entity;
using BookStore.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LivroController : ControllerBase
    {
        private readonly ILivroService _service;
        private readonly IMapper _mapper;

        public LivroController(ILivroService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllBooks()
        {
            var books = await _service.GetAllLivrosAsync();
            
            return Ok(books);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetBookById(int id)
        {
            var book = await _service.GetLivroByIdAsync(id);
            
            if (book == null)
            {
                return NotFound();
            }

            return Ok(book);
        }

        [HttpPost]
        public async Task<IActionResult> AddBook([FromBody] CreateLivroCommand command)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var livroDto = _mapper.Map<LivroDto>(command);

            var livro = await _service.AddLivroAsync(livroDto);

            return CreatedAtAction(nameof(GetBookById), new { id = livro.CodLivro }, livro);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBook(int id, [FromBody]UpdateLivroCommand command)
        {
            if (id != command.CodLivro || !ModelState.IsValid)
            {
                return BadRequest();
            }

            var livro = _mapper.Map<LivroDto>(command);

            await _service.UpdateLivroAsync(livro);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBook(int id)
        {
            try
            {
                await _service.DeleteLivroAsync(id);

                return NoContent();
            }
            catch (DbUpdateException ex)
            {
                if (ex.InnerException != null)
                {
                    if (ex.InnerException.Message.ToUpper().Contains("FOREIGN KEY CONSTRAINT"))
                    {
                        return BadRequest("Não é possível excluir este livro, pois existem registros relacionados.");
                    }
                }

                return StatusCode(500, "Ocorreu um erro ao tentar excluir o livro.");
            }
        }

        [HttpGet("GerarRelatorio")]
        public async Task<IActionResult> GerarRelatorio()
        {
            var relatorio = await _service.GerarRelatorioPDF();

            return File(relatorio, "application/pdf", "RelatorioLivros.pdf");
        }
    }
}
