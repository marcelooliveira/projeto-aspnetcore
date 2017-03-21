using Aula.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Aula.Models.ViewModels;

namespace Aula
{
    public class DataService : IDataService
    {
        private readonly Contexto _contexto;

        public DataService(Contexto contexto)
        {
            this._contexto = contexto;
        }

        public void AddItemPedido(int pedidoId, int produtoId)
        {
            if (!this._contexto
                .ItensPedido.Include("Produto")
                .Any(i => 
                i.Pedido.Id == pedidoId
                && i.Produto.Id == produtoId))
            {
                var produto =
                    this._contexto
                    .Produtos
                    .Where(p => p.Id == produtoId).Single();

                var pedido =
                    this._contexto
                    .Pedidos
                    .Where(p => p.Id == pedidoId).Single();

                var novoItem = new ItemPedido(pedido, produto, 1);
                this._contexto
                .ItensPedido
                .Add(novoItem);
                this._contexto.SaveChanges();
            }
        }

        public Pedido AddPedido()
        {
            Pedido pedido = this._contexto.Pedidos.Add(new Pedido()).Entity;
            this._contexto.SaveChanges();
            return pedido;
        }

        public void DeleteItemPedido(int itemPedidoId)
        {
            ItemPedido itemParaExcluir = this._contexto.ItensPedido
                .Where(i => i.Id == itemPedidoId).SingleOrDefault();

            if (itemParaExcluir != null)
            {
                this._contexto.Remove(itemParaExcluir);
                this._contexto.SaveChanges();
            }
        }

        public ItemPedido GetItemPedido(int itemPedidoId)
        {
            return this._contexto.ItensPedido
                .Include("Pedido")
                .Where(i => i.Id == itemPedidoId).SingleOrDefault();
        }

        public List<Produto> GetProdutos()
        {
            return this._contexto.Produtos.ToList();
        }

        public void InicializaDB()
        {
            this._contexto.Database.EnsureCreated();
            if (this._contexto.Produtos.Count() == 0)
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
                    var produto = this._contexto.Produtos.Add(new Produto(descricao, preco)).Entity;
                    index++;
                }
                this._contexto.SaveChanges();
            }

        }

        public void UpdateItemPedido(ItemPedido itemPedido)
        {
            ItemPedido itemParaAtualizar = this._contexto.ItensPedido
                .Where(i => i.Id == itemPedido.Id).SingleOrDefault();

            if (itemParaAtualizar != null)
            {
                itemParaAtualizar.AtualizarQuantidade(itemPedido.Quantidade);
            }

            this._contexto.SaveChanges();
        }

        CarrinhoViewModel IDataService.GetCarrinho(int pedidoId)
        {
            var itensPedido =
                this._contexto
                .ItensPedido
                .Where(i => i.Pedido.Id == pedidoId)
                .Include("Produto").ToList();
            return new CarrinhoViewModel(pedidoId, itensPedido);
        }
    }
}
