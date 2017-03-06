using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Aula.Models
{
    public class ItemPedido
    {
        public int Id { get; private set; }
        public Produto Produto { get; private set; }
        public int Quantidade { get; private set; }
        public decimal PrecoUnitario { get; private set; }

        public ItemPedido()
        {

        }

        public ItemPedido(Produto produto, int quantidade)
        {
            this.Produto = produto;
            this.Quantidade = quantidade;
            this.PrecoUnitario = produto.Preco;
        }
    }
}
