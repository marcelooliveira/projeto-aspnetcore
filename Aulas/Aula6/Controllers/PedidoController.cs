using Aula.Models;
using Aula.Models.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace Aula.Controllers
{
    public class PedidoController : Controller
    {
        IDataService _dataService;
        public PedidoController(IDataService dataService)
        {
            this._dataService = dataService;
        }

        private static List<Produto> GetProdutosDeTeste()
        {
            return new List<Produto>()
            {
                new Produto(1, "Produto A", 12.34m),
                new Produto(2, "Produto B", 23.45m),
                new Produto(3, "Produto C", 34.56m),
                new Produto(4, "Produto D", 12.34m)
            };
        }

        public IActionResult Carrossel()
        {
            GeraCookie();
            List<Produto> modelo = this._dataService.GetProdutos();
            return View(modelo);
        }

        private void GeraCookie()
        {
            int? pedidoId = GetCookiePedidoId();
            if (!pedidoId.HasValue)
            {
                Pedido pedido = this._dataService.AddPedido();
                pedidoId = pedido.Id;
            }
            AddCookiePedidoId(pedidoId);
        }

        private void AddCookiePedidoId(int? pedidoId)
        {
            Response.Cookies.Append("pedidoId", pedidoId.ToString());
        }

        private int? GetCookiePedidoId()
        {
            int? pedidoId = null;
            if (Request.Cookies.ContainsKey("pedidoId"))
            {
                pedidoId = int.Parse(Request.Cookies["pedidoId"]);
            }

            return pedidoId;
        }

        public IActionResult Carrinho()
        {
            int? pedidoId = GetCookiePedidoId();

            if (pedidoId.HasValue)
            {
                List<ItemPedido> itensPedido =
                    this._dataService
                    .GetCarrinho(pedidoId.Value).ItensCarrinho;
                return View(new CarrinhoViewModel(pedidoId.Value, itensPedido));
            }
            else
            {
                return RedirectToAction("Carrossel");
            }
        }

        public IActionResult Cadastro()
        {
            Pedido model = new Pedido();
            return View(model);
        }

        public IActionResult Resumo()
        {
            int? pedidoId = GetCookiePedidoId();
            if (pedidoId.HasValue)
            {
                List<ItemPedido> itensPedido = 
                    this._dataService.GetCarrinho(pedidoId.Value).ItensCarrinho;
                if (itensPedido.Count == 0)
                {
                    return RedirectToAction("Carrossel");
                }
                Response.Cookies.Delete("pedidoId");
                return View(new CarrinhoViewModel(pedidoId.Value, itensPedido));
            }
            else
            {
                return RedirectToAction("Carrossel");
            }
        }

        public IActionResult AdicionarAoCarrinho(int produtoId)
        {
            int? pedidoId = GetCookiePedidoId();
            if (pedidoId.HasValue)
            {
                this._dataService.AddItemPedido(pedidoId.Value, produtoId);
                return RedirectToAction("Carrinho", "Pedido");
            }
            else
            {
                return View("Carrossel");
            }
        }
    }
}
