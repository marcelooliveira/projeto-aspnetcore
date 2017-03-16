using Aluno.Models;
using Aluno.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace Aluno.Controllers
{
    public class PedidoController : Controller
    {
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

        private static CarrinhoViewModel GetItensDeTeste(List<Produto> produtos)
        {
            return new CarrinhoViewModel(new List<ItemPedido>
            {
                new ItemPedido(1, produtos[0], 1, produtos[0].Preco),
                new ItemPedido(2, produtos[1], 2, produtos[1].Preco),
                new ItemPedido(3, produtos[2], 3, produtos[2].Preco)
            });
        }

        public IActionResult Carrossel()
        {
            List<Produto> modelo = GetProdutosDeTeste();
            return View(modelo);
        }


        public IActionResult Carrinho()
        {
            var produtos = GetProdutosDeTeste();
            CarrinhoViewModel modelo = GetItensDeTeste(produtos);

            return View(modelo);
        }


        public IActionResult Resumo()
        {
            var produtos = GetProdutosDeTeste();
            CarrinhoViewModel modelo = GetItensDeTeste(produtos);

            return View(modelo);
        }
    }
}
