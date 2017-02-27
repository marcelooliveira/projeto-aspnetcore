using Aula.Models;
using Aula.Models.PedidoViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace WebApplication.Controllers
{
    public class PedidoController : Controller
    {
        readonly List<Produto> produtos = new List<Produto>() {
            new Produto(1, "produto 1", 49.90m ),
            new Produto(2, "produto 2", 59.90m ),
            new Produto(3, "produto 3", 39.90m ),
            new Produto(4, "produto 4", 129.90m )
        };

        readonly List<ItemPedido> itensPedido;

        public PedidoController()
        {
            itensPedido = new List<ItemPedido>()
            {
                new ItemPedido(produtos[0], 1),
                new ItemPedido(produtos[1], 2),
                new ItemPedido(produtos[2], 3),
                new ItemPedido(produtos[3], 2)
            };
        }

        public IActionResult Carrossel()
        {
            return View(produtos);
        }

        [ResponseCache(NoStore=true)]
        public IActionResult Carrinho()
        {
            return View(new CarrinhoViewModel(itensPedido));
        }

        [ResponseCache(NoStore = true)]
        public IActionResult Resumo()
        {
            return View(itensPedido);
        }
    }
}
