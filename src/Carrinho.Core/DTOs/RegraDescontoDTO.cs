using System;

namespace Carrinho.Core.DTOs
{
    public class RegraDescontoDTO
    {
        public decimal MinValor { get; set; }
        public decimal MaxValor { get; set; }
        public decimal Taxa { get; set; }
        public decimal DescontoCalculado { get; set; }

        public RegraDescontoDTO(decimal minValor, decimal maxValor, decimal taxa)
        {
            this.MinValor = minValor;
            this.MaxValor = maxValor;
            this.Taxa = taxa;
        }

        public bool CheckRange(decimal subtotal)
        {
            var resultado = subtotal >= MinValor && subtotal <= MaxValor;
            if (resultado)
                DescontoCalculado = Math.Round(subtotal * Taxa, 2);
            return resultado;
        }
    }
}