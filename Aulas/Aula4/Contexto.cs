using Aula.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Aula
{
    public class Contexto : DbContext
    {
        public DbSet<ItemPedido> ItensPedido { get; set; }
        public DbSet<Produto> Produtos { get; set; }

        public Contexto()
        {
        }

        public Contexto(DbContextOptions<Contexto> options) : base(options) { }
    }
}
