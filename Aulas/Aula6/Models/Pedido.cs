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
        public Pedido()
        {
            Itens = new List<ItemPedido>();
        }

        [DataMember]
        public List<ItemPedido> Itens { get; set; }
        [DataMember]
        public decimal Total
        {
            get
            {
                return Itens.Select(i => i.Quantidade * i.PrecoUnitario).Sum();
            }
        }
        [DataMember]
        public string Nome { get; set; }
        [DataMember]
        public string Email { get; set; }
        [DataMember]
        public string Endereco { get; set; }
        [DataMember]
        public string Complemento { get; set; }
        [DataMember]
        public string Bairro { get; set; }
        [DataMember]
        public string Municipio { get; set; }
        [DataMember]
        public string UF { get; set; }
        [DataMember]
        public string CEP { get; set; }
    }
}
