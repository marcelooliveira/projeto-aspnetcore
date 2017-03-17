using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Aluno.Models
{
    public class ItemPedido : BaseModel
    {
        public Produto Produto { get; private set; }
        public int Quantidade { get; private set; }
        public decimal PrecoUnitario { get; private set; }

        public ItemPedido()
        {

        }

        public ItemPedido(int id, Produto produto, int quantidade) : this(produto, quantidade)
        {
            this.Id = id;
            this.PrecoUnitario = produto.Preco;
        }

        public ItemPedido(Produto produto, int quantidade)
        {
            this.Produto = produto;
            this.Quantidade = quantidade;
            this.PrecoUnitario = produto.Preco;
        }
    }
}
