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
        private readonly IPedidoManager _checkoutManager;

        public HomeController(IPedidoManager checkoutManager)
        {
            this._checkoutManager = checkoutManager;
        }

        public ActionResult Index()
        {
            return View(_checkoutManager.GetProdutos());
        }

        public ActionResult Cart()
        {
            return View(_checkoutManager.GetCarrinho());
        }

        public ActionResult CheckoutSuccess()
        {
            return View(_checkoutManager.GetResumoPedido());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddToCart(string SKU)
        {
            _checkoutManager.SaveCarrinho(new Core.DTOs.ItemCarrinhoDTO
            {
                SKU = SKU,
                Quantity = 1
            });
            return RedirectToAction("Cart", "Home");
        }
    }
}
