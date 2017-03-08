using Aula.Models;
using Aula.Models.PedidoViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Aula.WebApi
{
    public class PostCarrinhoResponse
    {
        public CarrinhoViewModel CarrinhoViewModel { get; private set; }
        public ItemPedido ItemPedido { get; private set; }

        public PostCarrinhoResponse(CarrinhoViewModel carrinho, 
            ItemPedido itemPedido)
        {
            this.CarrinhoViewModel = carrinho;
            this.ItemPedido = itemPedido;
        }
    }
}
