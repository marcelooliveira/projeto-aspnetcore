using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
        public virtual List<ItemPedido> Itens { get; set; }
        [DataMember]
        public decimal Total
        {
            get
            {
                return Itens.Select(i => i.Quantidade * i.PrecoUnitario).Sum();
            }
        }
        [DataMember]
        [StringLength(50, MinimumLength = 3,
            ErrorMessage = "Nome deve ter entre 3 e 50 caracteres.")]
        [Required(ErrorMessage = "Nome é obrigatório")]
        public string Nome { get; set; }
        
        [DataMember]
        [EmailAddress]
        [Required(ErrorMessage = "Email é obrigatório")]
        public string Email { get; set; }

        [DataMember]
        [StringLength(50, MinimumLength = 3,
            ErrorMessage = "Endereço deve ter entre 3 e 50 caracteres.")]
        [Required(ErrorMessage = "Endereço é obrigatório")]
        public string Endereco { get; set; }


        [DataMember]
        [StringLength(50, ErrorMessage = "Complemento deve ter até 50 caracteres.")]
        public string Complemento { get; set; }
        
        [DataMember]
        [StringLength(50, MinimumLength = 3,
            ErrorMessage = "Bairro deve ter entre 3 e 50 caracteres.")]
        [Required(ErrorMessage = "Bairro é obrigatório")]
        public string Bairro { get; set; }


        [DataMember]
        [StringLength(50, MinimumLength = 3,
            ErrorMessage = "Município deve ter entre 3 e 50 caracteres.")]
        [Required(ErrorMessage = "Município é obrigatório")]
        public string Municipio { get; set; }
        
        [DataMember]
        [StringLength(2, MinimumLength = 2,
            ErrorMessage = "UF deve ter 2 caracteres.")]
        [Required(ErrorMessage = "UF é obrigatório")]
        public string UF { get; set; }
        
        [DataMember]
        [StringLength(9, MinimumLength = 9,
            ErrorMessage = "CEP deve ter 9 caracteres.")]
        [Required(ErrorMessage = "CEP é obrigatório")]
        public string CEP { get; set; }

        public void AtualizarCadastro(Pedido cadastro)
        {       
            this.Nome = cadastro.Nome;
            this.Email = cadastro.Email;
            this.Endereco = cadastro.Endereco;
            this.Complemento = cadastro.Complemento;
            this.Bairro = cadastro.Bairro;
            this.Municipio = cadastro.Municipio;
            this.UF = cadastro.UF;
            this.CEP = cadastro.CEP;
        }
    }
}
