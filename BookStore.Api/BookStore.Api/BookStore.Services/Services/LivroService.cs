using AutoMapper;
using BookStore.Domain.DTO;
using BookStore.Domain.Entity;
using BookStore.Infra.Interfaces;
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

        public async Task<byte[]> GerarRelatorioPDF()
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
                    var subTitleFont = FontFactory.GetFont(FontFactory.HELVETICA, 12);

                    Paragraph title = new Paragraph("Relatório de Livros", titleFont);
                    title.Alignment = Element.ALIGN_CENTER;
                    doc.Add(title);
                    doc.Add(new Paragraph("\n"));

                    // Criar a Tabela com cabeçalhos
                    PdfPTable table = new PdfPTable(5); // Define 5 colunas
                    table.WidthPercentage = 100; // Tabela ocupa 100% da página
                    table.SetWidths(new float[] { 5f, 4f, 3f, 1f, 1f });

                    BaseColor corCabecalho = new BaseColor(207, 207, 196);

                    string[] cabecalhos = { "Título", "Autores", "Assuntos", "Edição", "Ano" };

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

                    var listaLivros = _repository.ObterDadosRelatorio();

                    if (listaLivros.Count > 0)
                    {
                        foreach (var livro in listaLivros)
                        {
                            table.AddCell(livro.Titulo);
                            table.AddCell(livro.Autores);
                            table.AddCell(livro.Assuntos);
                            table.AddCell(livro.Edicao.ToString());
                            table.AddCell(livro.AnoPublicacao.ToString());
                        }
                    }

                    table.SpacingBefore = 10f;
                    table.SpacingAfter = 10f;

                    // Adicionar a tabela ao documento
                    doc.Add(table);

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
