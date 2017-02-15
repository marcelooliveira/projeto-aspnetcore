using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Carrinho.Core;

namespace Carrinho.Controllers
{
    public class HomeController : Controller
    {
        private readonly IPedidoManager _pedidoManager;

        public HomeController(IPedidoManager pedidoManager)
        {
            this._pedidoManager = pedidoManager;
        }

        public ActionResult Index()
        {
            return View(_pedidoManager.GetProdutos());
        }

        public ActionResult Carrinho()
        {
            return View(_pedidoManager.GetCarrinho());
        }

        public ActionResult SucessoPedido()
        {
            return View(_pedidoManager.GetResumoPedido());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddToCart(string SKU)
        {
            _pedidoManager.SaveCarrinho(new Core.DTOs.ItemCarrinhoDTO
            {
                SKU = SKU,
                Quantidade = 1
            });
            return RedirectToAction("Carrinho", "Home");
        }
    }
}
