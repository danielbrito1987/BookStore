using BookStore.Domain.Entity;
using BookStore.Infra.Interfaces;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace BookStore.Test.Repositories
{

    public class AssuntoRepositoryTest
    {
        Mock<IAssuntoRepository> _autorRepository;

        public AssuntoRepositoryTest()
        {
            _autorRepository = new Mock<IAssuntoRepository>();
        }

        [Fact]
        public async Task GetById_ReturnsAssunto_WhenExists()
        {
            // Arrange
            var assuntoId = 1;
            var expectedAssunto = new Assunto { CodAssunto = 1, Descricao = "Assunto Teste" };
            _autorRepository.Setup(repo => repo.GetByIdAsync(assuntoId)).ReturnsAsync(expectedAssunto);

            // Act
            var result = await _autorRepository.Object.GetByIdAsync(assuntoId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(expectedAssunto.CodAssunto, result.CodAssunto);
        }

        [Fact]
        public async Task Add_AddsAssunto()
        {
            // Arrange
            var novoAssunto = new Assunto { Descricao = "Assunto Teste" };
            _autorRepository.Setup(repo => repo.AddAsync(novoAssunto));

            // Act
            await _autorRepository.Object.AddAsync(novoAssunto);

            // Assert
            _autorRepository.Verify(repo => repo.AddAsync(novoAssunto), Times.Once);
        }

        [Fact]
        public void Update_UpdateAssunto()
        {
            // Arrange
            var assunto = new Assunto { CodAssunto = 1, Descricao = "Assunto Teste" };
            _autorRepository.Setup(repo => repo.Update(assunto));

            // Act
            _autorRepository.Object.Update(assunto);

            // Assert
            _autorRepository.Verify(repo => repo.Update(assunto), Times.Once);
        }

        [Fact]
        public void Delete_DeleteAssunto()
        {
            // Arrange
            var assunto = new Assunto { CodAssunto = 1, Descricao = "Assunto Teste" };
            _autorRepository.Setup(repo => repo.Delete(assunto));

            // Act
            _autorRepository.Object.Delete(assunto);

            // Assert
            _autorRepository.Verify(repo => repo.Delete(assunto), Times.Once);
        }
    }
}
