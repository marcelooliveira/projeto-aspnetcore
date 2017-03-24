using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace Aula.Models
{
    [DataContract]
    public class ItemPedido : BaseModel
    {
        [IgnoreDataMember]
        [Required]
        public Pedido Pedido { get; private set; }
        [DataMember]
        public int PedidoId { get; private set; }
        [IgnoreDataMember]
        [Required]
        public Produto Produto { get; set; }
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

        public ItemPedido(int id, Pedido pedido, Produto produto, int quantidade) 
            : this(pedido, produto, quantidade)
        {
            this.Id = id;
            this.PrecoUnitario = produto.Preco;
        }

        public ItemPedido(Pedido pedido, Produto produto, int quantidade)
        {
            this.Pedido = pedido;
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
