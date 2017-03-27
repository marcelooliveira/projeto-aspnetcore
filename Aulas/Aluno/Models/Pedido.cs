using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace Aluno.Models
{
    public class Pedido : BaseModel
    {
        public Pedido()
        {
            Itens = new List<ItemPedido>(); 
        }

        public List<ItemPedido> Itens { get; set; }
        public decimal Total
        {
            get
            {
                return Itens
                    .Select(i => i.Quantidade * i.PrecoUnitario).Sum();
            }
        }

        [StringLength(50, MinimumLength = 3, 
            ErrorMessage = "Nome deve ter entre 3 e 50 caracteres")]
        [Required(ErrorMessage = "Nome é obrigatório")]
        public string Nome { get; set; }

        [EmailAddress(ErrorMessage = "Email inválido")]
        [Required(ErrorMessage = "Email é obrigatório")]
        public string Email { get; set; }

        [StringLength(50, MinimumLength = 3,
            ErrorMessage = "Endereço deve ter entre 3 e 50 caracteres")]
        [Required(ErrorMessage = "Endereço é obrigatório")]
        public string Endereco { get; set; }

        [StringLength(50, 
            ErrorMessage = "Complemento deve ter até 50 caracteres")]
        public string Complemento { get; set; }

        [StringLength(50, MinimumLength = 3,
            ErrorMessage = "Bairro deve ter entre 3 e 50 caracteres")]
        [Required(ErrorMessage = "Bairro é obrigatório")]
        public string Bairro { get; set; }

        [StringLength(50, MinimumLength = 3,
            ErrorMessage = "Município deve ter entre 3 e 50 caracteres")]
        [Required(ErrorMessage = "Municipio é obrigatório")]
        public string Municipio { get; set; }

        [RegularExpression("^\\d{2}$",
            ErrorMessage = "UF inválida")]
        [Required(ErrorMessage = "UF é obrigatório")]
        public string UF { get; set; }

        [RegularExpression("^\\d{5}[-]\\d{3}$", 
            ErrorMessage = "CEP inválido")]
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

