using Aula.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Aula
{
    public class DataService : IDataService
    {
        private readonly Contexto _contexto;
        public DataService(Contexto contexto)
        {
            this._contexto = contexto;
        }

        public List<Produto> GetProdutos()
        {
            return this._contexto.Produtos.ToList();
        }

        public List<ItemPedido> GetCarrinho()
        {
            return this._contexto.ItensPedido.Include("Produto").ToList();
        }

        public Contexto InicializaDB()
        {
            bool bancoNovo = this._contexto.Database.EnsureCreated();
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

                    var produto = this._contexto.Produtos.Add(new Produto(descricao, preco)).Entity;
                    var itemCarrinho = this._contexto.ItensPedido.Add(new ItemPedido(produto, 1)).Entity;

                    index++;
                }
            }

            this._contexto.SaveChanges();
            return this._contexto;
        }       
    }
}
