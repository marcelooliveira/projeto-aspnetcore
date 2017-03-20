using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Aula.Models
{
    public class Pedido : BaseModel
    {
        public List<ItemPedido> Itens { get; private set; }
        public decimal Total { get; private set; }
    }
}
