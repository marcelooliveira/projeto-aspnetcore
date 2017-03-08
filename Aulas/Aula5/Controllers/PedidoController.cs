using Aula;
using Aula.Models;
using Aula.Models.PedidoViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace WebApplication.Controllers
{
    public class PedidoController : Controller
    {
        private readonly IDataService _dataService;

        public PedidoController(IDataService dataService)
        {
            this._dataService = dataService;
        }

        public IActionResult Carrossel()
        {
            List<Produto> produtos = this._dataService.GetProdutos();
            return View(produtos);
        }

        [ResponseCache(NoStore=true)]
        public IActionResult Carrinho()
        {
            List<ItemPedido> itensPedido = this._dataService.GetItensPedido();
            return View(new CarrinhoViewModel(itensPedido));
        }

        [ResponseCache(NoStore = true)]
        public IActionResult Resumo()
        {
            List<ItemPedido> itensPedido = this._dataService.GetItensPedido();
            return View(itensPedido);
        }

        [HttpPost]
        public IActionResult AdicionarAoCarrinho(int id)
        {
            this._dataService.AddItemPedido(id);
            return RedirectToAction("Carrinho", "Pedido");
        }
    }
}
