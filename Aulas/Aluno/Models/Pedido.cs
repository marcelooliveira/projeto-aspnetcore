using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Aluno.Models
{
    public class Pedido : BaseModel
    {
        public Pedido()
        {
            Itens = new List<ItemPedido>();
        }

        public List<ItemPedido> Itens { get; private set; }
        public decimal Total
        {
            get
            {
                return Itens
                    .Select(i => i.Quantidade * i.PrecoUnitario).Sum();
            }
        }
    }
}
