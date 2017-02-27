using Aula3.Models;
using Aula3.Models.PedidoViewModels;
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
                new ItemPedido(1, produtos[0], 1, produtos[0].Preco),
                new ItemPedido(1, produtos[1], 2, produtos[1].Preco),
                new ItemPedido(1, produtos[2], 3, produtos[2].Preco),
                new ItemPedido(1, produtos[3], 2, produtos[3].Preco)
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
