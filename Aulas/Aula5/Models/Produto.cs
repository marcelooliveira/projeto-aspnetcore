using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Aula.Models
{
    public class Produto
    {
        public int Id { get; private set; }
        public string Descricao { get; private set; }
        public decimal Preco { get; private set; }

        public Produto()
        {

        }

        public Produto(int id,
                string descricao,
                decimal preco) : this(descricao, preco)
        {
            this.Id = id;
        }

        public Produto(string descricao,
                        decimal preco)
        {
            this.Descricao = descricao;
            this.Preco = preco;
        }
    }
}
