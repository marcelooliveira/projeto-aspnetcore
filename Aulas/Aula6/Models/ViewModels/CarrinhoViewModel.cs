using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Aula.Models.ViewModels
{
    public class CarrinhoViewModel
    {
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

        public CarrinhoViewModel(List<ItemPedido> itensCarrinho)
        {
            this.ItensCarrinho = itensCarrinho;
        }
    }
}
