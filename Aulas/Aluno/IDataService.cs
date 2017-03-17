using Aluno.Models;
using Aluno.Models.ViewModels;
using System.Collections.Generic;

namespace Aluno
{
    public interface IDataService
    {
        void InicializaDB();
        List<Produto> GetProdutos();
        void UpdateItemPedido(ItemPedido itemPedido);
        ItemPedido GetItemPedido(int itemPedido);
        CarrinhoViewModel GetCarrinho();
        void DeleteItemPedido(int itemPedidoId);
    }
}