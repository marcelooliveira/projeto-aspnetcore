using Aula.Models;
using System.Collections.Generic;

namespace Aula
{
    public interface IDataService
    {
        Contexto InicializaDB();
        List<Produto> GetProdutos();
        List<ItemPedido> GetItensPedido();
        void UpdateItemPedido(ItemPedido itemPedido);
        ItemPedido GetItemPedido(int itemPedidoId);
        void DeleteItemPedido(int itemPedidoId);
    }
}