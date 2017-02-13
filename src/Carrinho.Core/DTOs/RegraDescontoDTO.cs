using System;

namespace Carrinho.Core.DTOs
{
    public class RegraDescontoDTO
    {
        public decimal MinValue { get; set; }
        public decimal MaxValue { get; set; }
        public decimal Taxa { get; set; }
        public decimal DescontoCalculado { get; set; }

        public RegraDescontoDTO(decimal minValue, decimal maxValue, decimal taxa)
        {
            this.MinValue = minValue;
            this.MaxValue = maxValue;
            this.Taxa = taxa;
        }

        public bool CheckRange(decimal subtotal)
        {
            var result = subtotal >= MinValue && subtotal <= MaxValue;
            if (result)
                DescontoCalculado = Math.Round(subtotal * Taxa, 2);
            return result;
        }
    }
}