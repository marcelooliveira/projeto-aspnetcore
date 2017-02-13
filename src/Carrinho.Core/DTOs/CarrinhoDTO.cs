using System.Collections.Generic;

namespace Carrinho.Core.DTOs
{
    public class CarrinhoDTO
    {
        public CarrinhoDTO()
        {
        }

        public decimal Subtotal { get; set; }
        public decimal DiscountRate { get; set; }
        public decimal DiscountValue { get; set; }
        public decimal Total { get; set; }

        public List<ItemCarrinhoDTO> ItemsCarrinho { get; set; }
    }
}