using System.Collections.Generic;

namespace Carrinho.Core.DTOs
{
    public class ResumoPedidoDTO
    {
        public string NumeroPedido { get; set; }
        public decimal Total { get; set; }
        public int EntregueAteNDias { get; set; }

        public string EntregueAte
        {
            get
            {
                return string.Format("{0} {1}", EntregueAteNDias, 
                    EntregueAteNDias == 1 ? " dia útil" : " dias úteis");
            }
        }

        public ClienteInfoDTO ClienteInfo { get; set; }
        public List<ItemCarrinhoDTO> ItemsCarrinho { get; set; }
    }
}
