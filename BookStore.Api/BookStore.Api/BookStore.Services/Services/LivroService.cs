using AutoMapper;
using BookStore.Domain.DTO;
using BookStore.Domain.Entity;
using BookStore.Infra.Interfaces;
using BookStore.Services;
using BookStore.Services.Helper;
using BookStore.Services.Interfaces;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Servuces.Services
{
    public class LivroService : ILivroService
    {
        private readonly IMapper _mapper;
        private readonly ILivroRepository _repository;

        public LivroService(ILivroRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<LivroDto>> GetAllLivrosAsync()
        {
            return _mapper.Map<IList<LivroDto>>(await _repository.GetLivrosWithAuthorsAsync());
        }

        public async Task<LivroDto> GetLivroByIdAsync(int id)
        {
            return _mapper.Map<LivroDto>(await _repository.GetByIdAsync(id));
        }

        public async Task<LivroDto> AddLivroAsync(LivroDto livroDto)
        {
            var livro = _mapper.Map<Livro>(livroDto);

            foreach (var autorId in livroDto.AutoresIds)
            {
                var livroAutor = new LivroAutor { CodAutor = autorId };
                livro.LivroAutores.Add(livroAutor);
            }

            foreach (var assuntoId in livroDto.AssuntosIds)
            {
                var livroAssunto = new LivroAssunto { CodAssunto = assuntoId };
                livro.LivroAssuntos.Add(livroAssunto);
            }

            await _repository.AddAsync(livro);
            await _repository.SaveAsync();

            return _mapper.Map<LivroDto>(livro);
        }

        public async Task UpdateLivroAsync(LivroDto livroDto)
        {
            var livro = _mapper.Map<Livro>(livroDto);

            _repository.Update(livro);
            await _repository.SaveAsync();
        }

        public async Task DeleteLivroAsync(int id)
        {
            var livro = await _repository.GetByIdAsync(id);
            if (livro != null)
            {

                _repository.Delete(livro);
                await _repository.SaveAsync();
            }
        }

        public async Task<byte[]> GerarRelatorioPDF(RelatorioFilter filter)
        {
            using(MemoryStream stream = new MemoryStream())
            {
                Document doc = new Document(PageSize.A4.Rotate());
                var writer = PdfWriter.GetInstance(doc, stream);
                writer.PageEvent = new CustomPdfPageEventHelper();

                try
                {
                    doc.Open();

                    // Título do Relatório
                    var titleFont = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 16);
                    var headerFont = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 12);

                    Paragraph title = new Paragraph("Relatório de Livros por Autor", titleFont);
                    title.Alignment = Element.ALIGN_CENTER;
                    doc.Add(title);
                    doc.Add(new Paragraph("\n"));                    

                    BaseColor corCabecalho = new BaseColor(207, 207, 196);

                    var listaAutores = _repository.ObterDadosRelatorio(filter.Titulo, filter.CodAutor);

                    if (listaAutores.Count > 0)
                    {
                        foreach (var autor in listaAutores)
                        {
                            Paragraph autorHeader = new Paragraph($"Autor: {autor.Nome}", headerFont);
                            autorHeader.SpacingBefore = 10;
                            doc.Add(autorHeader);

                            // Criar a Tabela com cabeçalhos
                            PdfPTable table = new PdfPTable(5); // Define 5 colunas
                            table.WidthPercentage = 100; // Tabela ocupa 100% da página
                            table.SetWidths(new float[] { 1f, 5f, 3f, 1f, 1f });

                            string[] cabecalhos = { "Cód.", "Título", "Assuntos", "Edição", "Ano" };

                            foreach (string cabecalho in cabecalhos)
                            {
                                PdfPCell celulaCabecalho = new PdfPCell(new Phrase(cabecalho))
                                {
                                    BackgroundColor = corCabecalho, // Definir a cor de fundo
                                    HorizontalAlignment = Element.ALIGN_CENTER, // Alinhamento do texto
                                    Padding = 5f // Espaçamento interno
                                };

                                table.AddCell(celulaCabecalho);
                            }

                            foreach (var livro in autor.Livros)
                            {
                                table.AddCell(new PdfPCell(new Phrase(livro.CodLivro.ToString()))
                                {
                                    HorizontalAlignment = Element.ALIGN_CENTER,
                                });
                                
                                table.AddCell(new PdfPCell(new Phrase(livro.Titulo)));
                                table.AddCell(new PdfPCell(new Phrase(livro.Assuntos)));
                                table.AddCell(new PdfPCell(new Phrase(livro.Edicao.ToString()))
                                {
                                    HorizontalAlignment = Element.ALIGN_CENTER,
                                });

                                table.AddCell(new PdfPCell(new Phrase(livro.AnoPublicacao.ToString()))
                                {
                                    HorizontalAlignment = Element.ALIGN_CENTER,
                                });
                            }

                            table.SpacingBefore = 10f;
                            table.SpacingAfter = 10f;

                            doc.Add(table);
                        }
                    }

                    // Fechar o documento
                    doc.Close();

                    return stream.ToArray();
                }
                catch (Exception ex)
                {
                    throw new Exception("Erro ao gerar o PDF", ex);
                }
            }
        }
    }
}
