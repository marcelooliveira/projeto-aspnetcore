﻿using Aula.Models;
using Aula.Models.ViewModels;
using System.Collections.Generic;

namespace Aula
{
    public interface IDataService
    {
        void InicializaDB();
        List<Produto> GetProdutos();
        void UpdateItemPedido(ItemPedido itemPedido);
        void AddItemPedido(int pedidoId, int produtoId);
        ItemPedido GetItemPedido(int itemPedido);
        CarrinhoViewModel GetCarrinho(int pedidoId);
        Pedido GetPedido(int pedidoId);
        void DeleteItemPedido(int itemPedidoId);
        Pedido AddPedido();
        Pedido UpdatePedido(Pedido cadastro);
    }
}