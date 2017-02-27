using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Aula3.Models
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
                        decimal preco)
        {
            this.Id = id;
            this.Descricao = descricao;
            this.Preco = preco;
        }
    }
}
