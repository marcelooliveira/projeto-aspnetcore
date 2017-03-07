using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace Aula.Models
{
    [DataContract]
    public class ItemPedido
    {
        [DataMember]
        public int Id { get; private set; }
        [IgnoreDataMember]
        public Produto Produto { get; private set; }
        [DataMember]
        public int Quantidade { get; private set; }
        [DataMember]
        public decimal PrecoUnitario { get; private set; }
        [DataMember]
        public decimal Subtotal
        {
            get
            {
                return Quantidade * PrecoUnitario;
            }
        }

        public ItemPedido()
        {

        }

        public ItemPedido(Produto produto, int quantidade)
        {
            this.Produto = produto;
            this.Quantidade = quantidade;
            this.PrecoUnitario = produto.Preco;
        }

        public void AtualizarQuantidade(int quantidade)
        {
            this.Quantidade = quantidade;
        }
    }
}
