using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CasaDoCodigo.Models
{
    public class Pedido : BaseModel
    {
        public List<ItemPedido> Itens { get; private set; }

        public Pedido()
        {
            Itens = new List<ItemPedido>();
        }

        public void UpdateCadastro(Pedido cadastro)
        {
            this.Nome = cadastro.Nome;
            this.Email = cadastro.Email;
            this.Telefone = cadastro.Telefone;
            this.Endereco = cadastro.Endereco;
            this.Complemento= cadastro.Complemento;
            this.Bairro = cadastro.Bairro;
            this.Municipio = cadastro.Municipio;
            this.UF = cadastro.UF;
            this.CEP = cadastro.CEP;
        }

        [Required]
        public string Nome { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Telefone { get; set; }

        [Required]
        public string Endereco { get; set; }
        public string Complemento { get; set; }
        [Required]
        public string Bairro { get; set; }

        [Required]
        public string Municipio { get; set; }
        [Required]
        public string UF { get; set; }
        [Required]
        public string CEP { get; set; }
    }
}
