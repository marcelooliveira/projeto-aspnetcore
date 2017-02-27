using Aula.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Aula4
{
    public class Contexto : DbContext
    {
        public DbSet<ItemPedido> ItemCarrinho { get; set; }
        public DbSet<Produto> Produto { get; set; }

        public Contexto()
        {
        }

        public Contexto(DbContextOptions<Contexto> options) : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            builder.UseSqlServer(
                @"Server=(localdb)\mssqllocaldb;Database=CasaDoCodigo;Trusted_Connection=True;");
        }
    }
}
