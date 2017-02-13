using System.Collections.Generic;

namespace Carrinho.Core.DTOs
{
    public class ResumoPedidoDTO
    {
        public string OrderNumber { get; set; }
        public decimal Total { get; set; }
        public int DeliveryUpToNWorkingDays { get; set; }

        public string DeliveryUpTo
        {
            get
            {
                return string.Format("{0} {1}", DeliveryUpToNWorkingDays, 
                    DeliveryUpToNWorkingDays == 1 ? " dia útil" : " dias úteis");
            }
        }

        public ClienteInfoDTO ClienteInfo { get; set; }
        public List<ItemCarrinhoDTO> ItemsCarrinho { get; set; }
    }
}
