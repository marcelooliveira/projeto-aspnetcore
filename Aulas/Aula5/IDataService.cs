using Aula.Models;
using Aula.Models.PedidoViewModels;
using System.Collections.Generic;

namespace Aula
{
    public interface IDataService
    {
        Contexto InicializaDB();
        List<Produto> GetProdutos();
        List<ItemPedido> GetItensPedido();
        void UpdateItemPedido(ItemPedido itemPedido);
        void AddItemPedido(int produtoId);
        ItemPedido GetItemPedido(int itemPedidoId);
        CarrinhoViewModel GetCarrinho();
        void DeleteItemPedido(int itemPedidoId);
    }
}