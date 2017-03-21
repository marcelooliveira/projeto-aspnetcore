using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace Aula.Models
{
    [DataContract]
    public class Pedido : BaseModel
    {
        [DataMember]
        public List<ItemPedido> Itens { get; private set; }
        [DataMember]
        public decimal Total { get; private set; }
    }
}
