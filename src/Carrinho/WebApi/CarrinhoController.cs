using Carrinho.Core;
using Carrinho.Core.DTOs;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Carrinho.WebApi
{
    [Route("api/[controller]")]
    public class CarrinhoController : Controller
    {
        readonly IPedidoManager _pedidoManager;

        public CarrinhoController(IPedidoManager pedidoManager)
        {
            this._pedidoManager = pedidoManager;
        }

        [HttpPost]
        [ResponseCache(NoStore = true)]
        //[ValidateAntiForgeryToken]
        public CarrinhoDTO Post([FromBody]ItemCarrinhoDTO value)
        {
            var carrinho = _pedidoManager.GetCarrinho();
            var itemCarrinho = carrinho.ItemsCarrinho.Where(i => i.SKU == value.SKU).SingleOrDefault();
            if (itemCarrinho != null)
            {
                itemCarrinho.Quantidade = value.Quantidade;
                var recalculatedCart = _pedidoManager.GetCarrinho(carrinho.ItemsCarrinho);

                _pedidoManager.SaveCarrinho(itemCarrinho);
                return recalculatedCart;
            }
            else
            {
                return carrinho;
            }
        }
    }
}
