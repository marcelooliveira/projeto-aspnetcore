using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Carrinho.Core.DTOs
{
    public class ProdutoDTO
    {
        public int Id { get; set; }
        public string SKU { get; set; }
        public string Description { get; set; }
        public string ImagemPequena { get; set; }
        public string ImagemGrande { get; set; }
        public decimal Price { get; set; }
    }
}
