using System.Collections.Generic;

namespace Carrinho.Core.DTOs
{
    public class CarrinhoDTO
    {
        public CarrinhoDTO()
        {
        }

        public decimal Subtotal { get; set; }
        public decimal RegraDesconto { get; set; }
        public decimal ValorDesconto { get; set; }
        public decimal Total { get; set; }

        public List<ItemCarrinhoDTO> ItemsCarrinho { get; set; }
    }
}