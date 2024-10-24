using BookStore.Services.DTO;
using BookStore.Services.Interfaces;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace BookStore.Test.Services
{
    public class AssuntoServiceTest
    {
        Mock<IAssuntoService> _assuntoService;

        public AssuntoServiceTest()
        {
            _assuntoService = new Mock<IAssuntoService>();
        }

        [Fact]
        public async Task GetAllAssuntos()
        {
            // Arrange
            var assunto = new AssuntoDto { CodAssunto = 1, Descricao = "Assunto Teste" };
            var assuntos = new List<AssuntoDto>();
            assuntos.Add(assunto);
            _assuntoService.Setup(s => s.GetAllAsync()).ReturnsAsync(assuntos);

            // Act
            var result = await _assuntoService.Object.GetAllAsync();

            // Assert
            Assert.Equal(assuntos, result);
            Assert.True(result.Count() > 0);
            _assuntoService.Verify(s => s.GetAllAsync(), Times.Once);
        }

        [Fact]
        public async Task GetById_WhenNotExists()
        {
            // Arrange
            var assuntoId = 999;
            AssuntoDto? expectedAssunto = null;
            _assuntoService.Setup(s => s.GetByIdAsync(assuntoId)).ReturnsAsync(expectedAssunto);

            // Act
            var result = await _assuntoService.Object.GetByIdAsync(assuntoId);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task GetById_WhenExists()
        {
            _assuntoService = new Mock<IAssuntoService>();

            // Arrange
            var assuntoId = 1;
            var assunto = new AssuntoDto { CodAssunto = 1, Descricao = "Assunto Teste" };
            _assuntoService.Setup(s => s.GetByIdAsync(assuntoId)).ReturnsAsync(assunto);

            // Act
            var result = await _assuntoService.Object.GetByIdAsync(assuntoId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(assunto.CodAssunto, result.CodAssunto);
        }

        [Fact]
        public async Task AddAssunto()
        {
            _assuntoService = new Mock<IAssuntoService>();

            // Arrange
            var novoAssunto = new AssuntoDto { Descricao = "Assunto Teste" };
            _assuntoService.Setup(s => s.AddAsync(novoAssunto));

            // Act
            await _assuntoService.Object.AddAsync(novoAssunto);

            // Assert
            _assuntoService.Verify(s => s.AddAsync(novoAssunto), Times.Once);
        }

        [Fact]
        public async Task Update_UpdateAssunto()
        {
            _assuntoService = new Mock<IAssuntoService>();

            // Arrange
            var assunto = new AssuntoDto { CodAssunto = 1, Descricao = "Assunto Teste" };
            string expected = "OK";
            _assuntoService.Setup(s => s.UpdateAsync(assunto)).ReturnsAsync(expected);

            // Act
            var result = await _assuntoService.Object.UpdateAsync(assunto);

            // Assert
            _assuntoService.Verify(s => s.UpdateAsync(assunto), Times.Once);
            Assert.Equal(expected, result);
        }

        [Fact]
        public async Task DeleteAssunto_WhenNotExists()
        {
            _assuntoService = new Mock<IAssuntoService>();

            // Arrange
            var assuntoId = 99;
            string expected = "Erro ao excluir o assunto! Não existe assunto associado ao código.";
            _assuntoService.Setup(s => s.DeleteAsync(assuntoId)).ReturnsAsync(expected);

            // Act
            var result = await _assuntoService.Object.DeleteAsync(assuntoId);

            // Assert
            _assuntoService.Verify(s => s.DeleteAsync(assuntoId));
            Assert.Equal(expected, result);
        }
    }
}
