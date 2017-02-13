﻿using System;

namespace Carrinho.Core.DTOs
{
    public class RegraDescontoDTO
    {
        public decimal MinValue { get; set; }
        public decimal MaxValue { get; set; }
        public decimal Rate { get; set; }
        public decimal CalculatedDiscount { get; set; }

        public RegraDescontoDTO(decimal minValue, decimal maxValue, decimal rate)
        {
            this.MinValue = minValue;
            this.MaxValue = maxValue;
            this.Rate = rate;
        }

        public bool CheckRange(decimal subtotal)
        {
            var result = subtotal >= MinValue && subtotal <= MaxValue;
            if (result)
                CalculatedDiscount = Math.Round(subtotal * Rate, 2);
            return result;
        }
    }
}