using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Services.Helper
{
    public class CustomPdfPageEventHelper : PdfPageEventHelper
    {
        // Sobrescrever o método OnEndPage para adicionar rodapé em cada página
        public override void OnEndPage(PdfWriter writer, Document document)
        {
            // Criar a fonte para o rodapé
            Font font = FontFactory.GetFont(FontFactory.HELVETICA, 8, Font.NORMAL, BaseColor.GRAY);

            // Criar o rodapé com a data/hora
            string rodapeTexto = "Relatório gerado em " + DateTime.Now.ToString("dd/MM/yyyy 'às' HH:mm");

            // Criar o parágrafo com o rodapé
            Phrase rodape = new Phrase(rodapeTexto, font);

            // Criar o número da página
            Phrase numeroPagina = new Phrase("Página " + writer.PageNumber, font);

            // Adicionar o rodapé no documento
            PdfContentByte cb = writer.DirectContent;
            ColumnText.ShowTextAligned(cb, Element.ALIGN_LEFT, rodape, document.LeftMargin, document.Bottom - 10, 0);
            ColumnText.ShowTextAligned(cb, Element.ALIGN_RIGHT, numeroPagina, document.Right - document.RightMargin, document.Bottom - 10, 0);
        }
    }
}
