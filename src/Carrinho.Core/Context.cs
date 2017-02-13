using Carrinho.Core.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.SqlClient;
using Microsoft.IdentityModel.Protocols;

namespace Carrinho.Core
{
    public class Context : DbContext
    {
        public DbSet<ItemCarrinho> ItemCarrinho { get; set; }
        public DbSet<Produto> Produto { get; set; }

        public Context(DbContextOptions<Context> options) : base(options) { }
    }
}
