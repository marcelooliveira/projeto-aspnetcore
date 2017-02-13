using Carrinho.Core.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Carrinho.Core
{
    public class DiscountManager
    {
        public List<RegraDescontoDTO> Descontos { get; private set; }

        static DiscountManager instance;
        public static DiscountManager Instance
        {
            get
            {
                if (instance == null)
                    instance = new DiscountManager();
                return instance;
            }
        }

        public DiscountManager()
        {
            Descontos = new List<RegraDescontoDTO>();
            Descontos.Add(new RegraDescontoDTO(0, 499.99M, 0));
            Descontos.Add(new RegraDescontoDTO(500M, 599.99M, .05M));
            Descontos.Add(new RegraDescontoDTO(600M, 699.99M, .10M));
            Descontos.Add(new RegraDescontoDTO(700M, decimal.MaxValue, .15M));
        }

        public RegraDescontoDTO GetDesconto(decimal subtotal)
        {
            return Descontos.Where(d => d.CheckRange(subtotal)).FirstOrDefault();
        }
    }
}
