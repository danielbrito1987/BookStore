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

namespace BookStore.Test.Repositories
{
    public class LivroRepositoryTest
    {
        [Fact]
        public async Task GetById_ReturnsLivro_WhenExists()
        {
            Mock<ILivroRepository> _livroRepository = new Mock<ILivroRepository>();

            // Arrange
            var livroId = 1;
            var expectedLivro = new Livro { CodLivro = livroId, Titulo = "Livro Teste" };
            _livroRepository.Setup(repo => repo.GetByIdAsync(livroId)).ReturnsAsync(expectedLivro);

            // Act
            var result = await _livroRepository.Object.GetByIdAsync(livroId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(expectedLivro.Titulo, result.Titulo);
        }

        [Fact]
        public async Task Add_AddsLivro()
        {
            Mock<ILivroRepository> _livroRepository = new Mock<ILivroRepository>();

            var livrosAssunto = new List<LivroAssunto>();
            var livroAssunto = new LivroAssunto();
            livroAssunto.CodAssunto = 1;
            livroAssunto.CodLivro = 1;

            livrosAssunto.Add(livroAssunto);

            var livroPrecos = new List<PrecoLivro>();
            var preco = new PrecoLivro();
            preco.CodLivro = 1;
            preco.TipoCompra = Domain.Enums.TipoCompraEnum.Balcao;
            preco.Valor = 59.9M;

            livroPrecos.Add(preco);

            // Arrange
            var novoLivro = new Livro { Titulo = "Título Teste", Editora = "Editora teste", Edicao = 1, AnoPublicacao = "2020", Precos = livroPrecos, LivroAssuntos = livrosAssunto };
            _livroRepository.Setup(repo => repo.AddAsync(novoLivro));

            // Act
            await _livroRepository.Object.AddAsync(novoLivro);

            // Assert
            _livroRepository.Verify(repo => repo.AddAsync(novoLivro), Times.Once);
        }

        [Fact]
        public async Task Update_UpdateLivro()
        {
            Mock<ILivroRepository> _livroRepository = new Mock<ILivroRepository>();

            // Arrange
            var livro = new Livro { CodLivro = 1, Titulo = "Título Teste", Editora = "Editora teste", Edicao = 1, AnoPublicacao = "2020" };

            // Act
            _livroRepository.Object.Update(livro);

            // Assert
            _livroRepository.Verify(repo => repo.Update(livro), Times.Once);
        }

        [Fact]
        public async Task Delete_DeleteLivro()
        {
            Mock<ILivroRepository> _livroRepository = new Mock<ILivroRepository>();

            // Arrange
            var livro = new Livro { CodLivro = 1, Titulo = "Título Teste", Editora = "Editora teste", Edicao = 1, AnoPublicacao = "2020" };

            // Act
            _livroRepository.Object.Delete(livro);

            // Assert
            _livroRepository.Verify(repo => repo.Delete(livro), Times.Once);
        }
    }
}
