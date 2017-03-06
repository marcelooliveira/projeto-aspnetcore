using Aula.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Aula
{
    public class Contexto : DbContext
    {
        public DbSet<ItemPedido> ItensPedido { get; set; }
        public DbSet<Produto> Produtos { get; set; }

        readonly IOptions<ConnectionStrings> _connectionStrings;

        public Contexto(DbContextOptions<Contexto> options,
            IOptions<ConnectionStrings> connectionStrings) : base(options)
        {
            this._connectionStrings = connectionStrings;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(this._connectionStrings.Value.Default);
        }
    }
}
