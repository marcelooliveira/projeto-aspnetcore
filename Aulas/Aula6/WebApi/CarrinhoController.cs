using Aula.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Aula.WebApi
{
    [Route("api/[controller]")]
    public class CarrinhoController : Controller
    {
        private readonly IDataService _dataService;
        public CarrinhoController(IDataService dataService)
        {
            this._dataService = dataService;
        }

        public PostCarrinhoResponse Post([FromBody]ItemPedido itemPedido)
        {
            ItemPedido itemAlterado = null;
            if (itemPedido.Quantidade > 0)
            {
                this._dataService.UpdateItemPedido(itemPedido);
                itemAlterado = this._dataService.GetItemPedido(itemPedido.Id);
            }
            else
            {
                this._dataService.DeleteItemPedido(itemPedido.Id);
            }
            var carrinho = this._dataService.GetCarrinho(itemPedido.PedidoId);
            return new PostCarrinhoResponse(carrinho, itemAlterado);
        }
    }
}
