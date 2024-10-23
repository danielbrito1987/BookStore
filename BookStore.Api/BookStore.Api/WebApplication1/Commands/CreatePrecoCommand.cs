using BookStore.Domain.Enums;

namespace BookStore.Api.Commands
{
    public class CreatePrecoCommand
    {
        public TipoCompraEnum TipoCompra { get; set; }
        public decimal Valor { get; set; }
    }
}
