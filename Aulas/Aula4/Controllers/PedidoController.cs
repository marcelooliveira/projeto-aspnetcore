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
        List<Produto> produtos = new List<Produto>() {
            new Produto(1, "produto 1", 49.90m ),
            new Produto(2, "produto 2", 59.90m ),
            new Produto(3, "produto 3", 39.90m ),
            new Produto(4, "produto 4", 129.90m )
        };

        private readonly IDataService _dataService;

        public PedidoController(IDataService dataService)
        {
            this._dataService = dataService;
        }

        public IActionResult Carrossel()
        {
            produtos = this._dataService.GetProdutos();
            return View(produtos);
        }

        [ResponseCache(NoStore=true)]
        public IActionResult Carrinho()
        {
            List<ItemPedido> itensPedido = this._dataService.GetCarrinho();
            return View(new CarrinhoViewModel(itensPedido));
        }

        [ResponseCache(NoStore = true)]
        public IActionResult Resumo()
        {
            List<ItemPedido> itensPedido = this._dataService.GetCarrinho();
            return View(itensPedido);
        }
    }
}
