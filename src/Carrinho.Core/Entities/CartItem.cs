using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Carrinho.Core.Entities
{
    [Table("ItemCarrinho")]
    public class ItemCarrinho
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [ForeignKey("Product")]
        public int ProductId { get; set; }

        [Required]
        public virtual Produto Product { get; set; }

        public int Quantity { get; set; }
    }
}
