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
            Nome =  "Alura da Silva";
            Email = "alura@gmail.com";
            Endereco = "rua vergueiro";
            Complemento = "3185 8º andar sala 200";
            Bairro = "Vila Mariana";
            Municipio = "São Paulo";
            UF = "SP";
            CEP = "04101-300";
        }

        [DataMember]
        public List<ItemPedido> Itens { get; private set; }
        [DataMember]
        public decimal Total
        {
            get
            {
                return Itens.Select(i => i.Quantidade * i.PrecoUnitario).Sum();
            }
        }
        [DataMember]
        public string Nome { get; private set; }
        [DataMember]
        public string Email { get; private set; }
        [DataMember]
        public string Endereco { get; private set; }
        [DataMember]
        public string Complemento { get; private set; }
        [DataMember]
        public string Bairro { get; private set; }
        [DataMember]
        public string Municipio { get; private set; }
        [DataMember]
        public string UF { get; private set; }
        [DataMember]
        public string CEP { get; private set; }
    }
}
