using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Aluno.Models.ViewModels
{
    public class CarrinhoViewModel
    {
        public int PedidoId { get; private set; }
        public List<ItemPedido> ItensCarrinho { get; private set; }

        public decimal Total
        {
            get
            {
                return ItensCarrinho.Select(i => i.Quantidade * i.PrecoUnitario).Sum();
            }
        }

        public CarrinhoViewModel()
        {

        }

        public CarrinhoViewModel(int pedidoId,
            List<ItemPedido> itensCarrinho)
        {
            this.PedidoId = pedidoId;
            this.ItensCarrinho = itensCarrinho;
        }
    }
}
