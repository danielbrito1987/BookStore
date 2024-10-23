using BookStore.Domain.Entity;
using BookStore.Infra;
using BookStore.Infra.Interfaces;
using BookStore.Infra.Repositories;
using BookStore.Servuces.Services;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace BookStore.Test.Services
{
    public class LivroServiceTests
    {
        private readonly Mock<ApplicationDbContext> _mockContext;
        private readonly Mock<ILivroRepository> _mockRepository;
        private readonly LivroRepository _livroRepository;

        public LivroServiceTests()
        {
            _mockContext = new Mock<ApplicationDbContext>();
            _mockRepository = new Mock<ILivroRepository>();
            _livroRepository = new LivroRepository(_mockContext.Object);
        }

        [Fact]
        public async Task GetById_ReturnsLivro_WhenExists()
        {
            // Arrange
            var livroId = 1;
            var expectedLivro = new Livro { CodLivro = livroId, Titulo = "Livro Teste" };
            _mockRepository.Setup(repo => repo.GetByIdAsync(livroId)).ReturnsAsync(expectedLivro);

            // Act
            var result = await _livroRepository.GetByIdAsync(livroId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(expectedLivro.Titulo, result.Titulo);
        }

        [Fact]
        public async Task Add_AddsLivro()
        {
            // Arrange
            var novoLivro = new Livro { Titulo = "Novo Livro" };
            _mockRepository.Setup(repo => repo.AddAsync(novoLivro));

            // Act
            await _livroRepository.AddAsync(novoLivro);

            // Assert
            _mockRepository.Verify(repo => repo.AddAsync(novoLivro), Times.Once);
        }
    }
}
