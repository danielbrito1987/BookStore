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
    public class AutorRepositoryTest
    {
        [Fact]
        public async Task GetById_ReturnsAutor_WhenExists()
        {
            Mock<IAutorRepository> _autorRepository = new Mock<IAutorRepository>();

            // Arrange
            var autorId = 1;
            var expectedAutor = new Autor { CodAutor = 1, Nome = "Autor Teste", LivroAutores = new List<LivroAutor>() };
            _autorRepository.Setup(repo => repo.GetByIdAsync(autorId)).ReturnsAsync(expectedAutor);

            // Act
            var result = await _autorRepository.Object.GetByIdAsync(autorId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(expectedAutor.CodAutor, result.CodAutor);
        }

        [Fact]
        public async Task Add_AddsAutor()
        {
            Mock<IAutorRepository> _autorRepository = new Mock<IAutorRepository>();

            // Arrange
            var novoAutor = new Autor { Nome = "Novo Autor" };
            _autorRepository.Setup(repo => repo.AddAsync(novoAutor));

            // Act
            await _autorRepository.Object.AddAsync(novoAutor);

            // Assert
            _autorRepository.Verify(repo => repo.AddAsync(novoAutor), Times.Once);
        }

        [Fact]
        public async Task Update_UpdateAutor()
        {
            Mock<IAutorRepository> _autorRepository = new Mock<IAutorRepository>();

            // Arrange
            var autor = new Autor { CodAutor = 1, Nome = "Autor teste" };
            _autorRepository.Setup(repo => repo.Update(autor));

            // Act
            _autorRepository.Object.Update(autor);

            // Assert
            _autorRepository.Verify(repo => repo.Update(autor), Times.Once);
        }

        [Fact]
        public async Task Delete_DeleteAutor()
        {
            Mock<IAutorRepository> _autorRepository = new Mock<IAutorRepository>();

            // Arrange
            var autor = new Autor { CodAutor = 1, Nome = "Autor teste" };
            _autorRepository.Setup(repo => repo.Delete(autor));

            // Act
            _autorRepository.Object.Delete(autor);

            // Assert
            _autorRepository.Verify(repo => repo.Delete(autor), Times.Once);
        }
    }
}
