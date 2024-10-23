using BookStore.Domain.Enums;

namespace BookStore.Api.Commands
{
    public class UpdatePrecoCommand
    {
        public int CodLivro { get; set; }
        public TipoCompraEnum TipoCompra { get; set; }
        public decimal Valor { get; set; }
    }
}
