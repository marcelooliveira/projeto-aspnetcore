using Aluno.Models;
using Aluno.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Aluno.WebApi
{
    public class PostCarrinhoResponse
    {
        public CarrinhoViewModel CarrinhoViewModel { get; private set; }
        public ItemPedido ItemPedido { get; private set; }

        public PostCarrinhoResponse(CarrinhoViewModel carrinhoViewModel,
            ItemPedido itemPedido)
        {
            this.CarrinhoViewModel = carrinhoViewModel;
            this.ItemPedido = itemPedido;
        }
    }
}
