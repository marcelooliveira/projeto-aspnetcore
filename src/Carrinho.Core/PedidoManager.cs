using Carrinho.Core.DTOs;
using Carrinho.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Diagnostics;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.EntityFrameworkCore;

namespace Carrinho.Core
{
    public class PedidoManager : IPedidoManager
    {
        private readonly string serverFilePath;
        private readonly DbContextOptions<Context> _dbOptions;

        public PedidoManager(DbContextOptions<Context> dbOptions)
        {
            this._dbOptions = dbOptions;
        }

        public PedidoManager(string serverFilePath)
        {
            this.serverFilePath = serverFilePath;
        }

        public ResumoPedidoDTO GetResumoPedido()
        {
            var itemsCarrinho = GetItemsCarrinho();
            var subtotal = itemsCarrinho.Sum(i => i.Subtotal);
            var taxaDesconto = DescontoManager.Instance.GetDesconto(subtotal);
            var valorDesconto = taxaDesconto.DescontoCalculado;
            var total = subtotal - valorDesconto;

            return new ResumoPedidoDTO
            {
                NumeroPedido = "123456789",
                EntregueAteNDias = 4,
                Total = total,
                ClienteInfo = GetDummyClienteInfo(),
                ItemsCarrinho = itemsCarrinho
            };
        }

        public CarrinhoDTO GetCarrinho()
        {
            return GetCarrinho(GetItemsCarrinho());
        }

        public CarrinhoDTO GetCarrinho(List<ItemCarrinhoDTO> itemsCarrinho)
        {
            var subtotal = itemsCarrinho.Sum(i => i.Subtotal);
            var regraDesconto = DescontoManager.Instance.GetDesconto(subtotal);
            var valorDesconto = regraDesconto.DescontoCalculado;
            var total = subtotal - valorDesconto;

            return new CarrinhoDTO
            {
                Subtotal = subtotal,
                TaxaDesconto = regraDesconto.Taxa * 100M,
                ValorDesconto = valorDesconto,
                Total = total,
                ItemsCarrinho = itemsCarrinho
            };
        }

        public void SaveCarrinho(ItemCarrinhoDTO newOrEditItem)
        {
            //try
            //{
                if (newOrEditItem.Quantidade < 0)
                    newOrEditItem.Quantidade = 0;

                using (var db = new Context(this._dbOptions))
                {
                    using (var transaction = db.Database.BeginTransaction())
                    {
                        var product = db.Produto.Where(p => p.SKU == newOrEditItem.SKU).Single();

                        var itemCarrinho =
                            (from ci in db.ItemCarrinho
                             join p in db.Produto on ci.ProdutoId equals p.Id
                             where p.SKU == newOrEditItem.SKU
                             select ci)
                            .SingleOrDefault();

                        if (itemCarrinho != null)
                        {
                            if (newOrEditItem.Quantidade == 0)
                                db.ItemCarrinho.Remove(itemCarrinho);
                            else
                            {
                                itemCarrinho.Quantidade = newOrEditItem.Quantidade;
                                itemCarrinho.Produto = product;
                            }
                        }
                        else
                        {
                            db.ItemCarrinho.Add(new ItemCarrinho
                            {
                                Produto = product,
                                Quantidade = newOrEditItem.Quantidade
                            });
                        }

                        db.SaveChanges();
                        transaction.Commit();
                    }
                }
        }

        public List<ProdutoDTO> GetProdutos()
        {
            using (var db = new Context(this._dbOptions))
            {
                return db.Produto
                    .Select(i =>
                    new ProdutoDTO
                    {
                        Id = i.Id,
                        SKU = i.SKU,
                        ImagemPequena = i.ImagemPequena,
                        Descricao = i.Descricao,
                        Preco = i.Preco
                    }).ToList();
            }
        }

        private List<ItemCarrinhoDTO> GetItemsCarrinho()
        {
            using (var db = new Context(this._dbOptions))
            {
                return
                    (from ci in db.ItemCarrinho
                     from p in db.Produto.Where(p => p.Id == ci.ProdutoId)
                     select new { ci, p })
                    .Select(i =>
                    new ItemCarrinhoDTO
                    {
                        Id = i.ci.Id,
                        SKU = i.p.SKU,
                        ImagemPequena = i.p.ImagemPequena,
                        Descricao = i.p.Descricao,
                        Preco = i.p.Preco,
                        Quantidade = i.ci.Quantidade
                    }).ToList();
            }
        }

        private ClienteInfoDTO GetDummyClienteInfo()
        {
            return new ClienteInfoDTO
            {
                Nome = "John Doe",
                Fone = "(11) 555-12345",
                Email = "johndoe@email.com",
                EnderecoEntrega = "503-250 Ferrand Drive - Toronto Ontario, M3C 3G8 Canada"
            };
        }

        public Context InicializaDB()
        {
            var db = new Context(this._dbOptions);

            bool bancoNovo = db.Database.EnsureCreated();

            if (bancoNovo)
            {
                var produtos = new string[]
                {
                    "Sleep not found|5990",
                    "May the code be with you|5990",
                    "Rollback|5990",
                    "REST|6990",
                    "Design Patterns com Java|6990",
                    "Vire o jogo com Spring Framework|6990",
                    "Test-Driven Development|6990",
                    "iOS: Programe para iPhone e iPad|6990",
                    "Desenvolvimento de Jogos para Android|6990"
                };

                var index = 1;
                foreach (var p in produtos)
                {
                    var descricao = p.Split('|')[0];
                    var preco = decimal.Parse(p.Split('|')[1]) / 100M;

                    var product =
                    db.Produto.Add(new Produto
                    {
                        SKU = Guid.NewGuid().ToString(),
                        ImagemPequena = string.Format("images/products/small_{0}.jpg", index),
                        ImagemGrande = string.Format("images/products/large_{0}.jpg", index),
                        Descricao = descricao,
                        Preco = preco
                    }).Entity;

                    var itemCarrinho =
                    db.ItemCarrinho.Add(new ItemCarrinho
                    {
                        Produto = product,
                        Quantidade = 1
                    }).Entity;

                    index++;
                }
            }

            db.SaveChanges();

            return db;
        }
    }
}
