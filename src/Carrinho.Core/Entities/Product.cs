using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Carrinho.Core.Entities
{
    [Table("Produto")]
    public class Produto
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string SKU { get; set; }
        public string Descricao { get; set; }
        public string ImagemPequena { get; set; }
        public string ImagemGrande { get; set; }
        public decimal Preco { get; set; }
    }
}
