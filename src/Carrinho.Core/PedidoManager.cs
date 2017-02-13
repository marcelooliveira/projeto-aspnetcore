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
                OrderNumber = "123456789",
                DeliveryUpToNWorkingDays = 4,
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
                if (newOrEditItem.Quantity < 0)
                    newOrEditItem.Quantity = 0;

                using (var db = new Context(this._dbOptions))
                {
                    using (var transaction = db.Database.BeginTransaction())
                    {
                        var product = db.Produto.Where(p => p.SKU == newOrEditItem.SKU).Single();

                        var itemCarrinho =
                            (from ci in db.ItemCarrinho
                             join p in db.Produto on ci.ProductId equals p.Id
                             where p.SKU == newOrEditItem.SKU
                             select ci)
                            .SingleOrDefault();

                        if (itemCarrinho != null)
                        {
                            if (newOrEditItem.Quantity == 0)
                                db.ItemCarrinho.Remove(itemCarrinho);
                            else
                            {
                                itemCarrinho.Quantity = newOrEditItem.Quantity;
                                itemCarrinho.Product = product;
                            }
                        }
                        else
                        {
                            db.ItemCarrinho.Add(new ItemCarrinho
                            {
                                Product = product,
                                Quantity = newOrEditItem.Quantity
                            });
                        }

                        db.SaveChanges();
                        transaction.Commit();
                    }
                }
            //}
            //catch (DbEntityValidationException dbEx)
            //{
            //    foreach (var validationErrors in dbEx.EntityValidationErrors)
            //    {
            //        foreach (var validationError in validationErrors.ValidationErrors)
            //        {
            //            Trace.TraceInformation("Property: {0} Error: {1}",
            //                                    validationError.PropertyName,
            //                                    validationError.ErrorMessage);
            //        }
            //    }
            //}
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
                        SmallImagePath = i.SmallImagePath,
                        Description = i.Description,
                        Price = i.Price
                    }).ToList();
            }
        }

        private List<ItemCarrinhoDTO> GetItemsCarrinho()
        {
            using (var db = new Context(this._dbOptions))
            {
                return
                    (from ci in db.ItemCarrinho
                     from p in db.Produto.Where(p => p.Id == ci.ProductId)
                     select new { ci, p })
                    .Select(i =>
                    new ItemCarrinhoDTO
                    {
                        Id = i.ci.Id,
                        SKU = i.p.SKU,
                        SmallImagePath = i.p.SmallImagePath,
                        Description = i.p.Description,
                        Price = i.p.Price,
                        Quantity = i.ci.Quantity
                    }).ToList();
            }
        }

        private ClienteInfoDTO GetDummyClienteInfo()
        {
            return new ClienteInfoDTO
            {
                CustomerName = "John Doe",
                PhoneNumber = "(11) 555-12345",
                Email = "johndoe@email.com",
                DeliveryAddress = "503-250 Ferrand Drive - Toronto Ontario, M3C 3G8 Canada"
            };
        }

        public Context InicializaDB()
        {
            var db = new Context(this._dbOptions);

            db.Database.EnsureCreated();

            //var products = new string[]
            //{
            //    "10 Million Member CodeProject T-Shirt|3399",
            //    "Women's T-Shirt|3399",
            //    "CodeProject.com Body Suit|1399",
            //    "CodeProject Mug Mugs|1099",
            //    "RootAdmin Mug|1099",
            //    "Drinking Glass|1099",
            //    "Stein|1399",
            //    "Mousepad|1099",
            //    "Square Sticker|299",
            //};

            var products = new string[]
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
            foreach (var p in products)
            {
                var description = p.Split('|')[0];
                var price = decimal.Parse(p.Split('|')[1]) / 100M;

                var product =
                db.Produto.Add(new Produto
                {
                    SKU = Guid.NewGuid().ToString(),
                    SmallImagePath = string.Format("images/products/small_{0}.jpg", index),
                    LargeImagePath = string.Format("images/products/large_{0}.jpg", index),
                    Description = description,
                    Price = price
                }).Entity;
                
                var itemCarrinho =
                db.ItemCarrinho.Add(new ItemCarrinho
                {
                    Product = product,
                    Quantity = 1
                }).Entity;

                index++;
            }

            db.SaveChanges();

            return db;
        }
    }
}
