using System;
using System.Collections.Generic;
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

            Nome = "Alura da Silva";
            Email = "alura@alura.com.br";
            Endereco = "Rua Vergueiro, 3185";
            Bairro = "Vila Mariana";
            Municipio = "São Paulo";
        }

        public string Nome { get; private set; }
        public string Email { get; private set; }
        public string Telefone { get; private set; }

        public string Endereco { get; private set; }
        public string Complemento { get; private set; }
        public string Bairro { get; private set; }

        public string Municipio { get; private set; }
        public string UF { get; private set; }
        public string CEP { get; private set; }
    }
}
