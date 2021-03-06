﻿using Aluno.Models;
using Aluno.Models.ViewModels;
using System.Collections.Generic;

namespace Aluno
{
    public interface IDataService
    {
        void InicializaDB();
        List<Produto> GetProdutos();
        void UpdateItemPedido(ItemPedido itemPedido);
        void AddItemPedido(int pedidoId, int produtoId);
        ItemPedido GetItemPedido(int itemPedido);
        CarrinhoViewModel GetCarrinho(int pedidoId);
        void DeleteItemPedido(int itemPedidoId);
        Pedido AddPedido();
        Pedido GetPedido(int pedidoId);
        Pedido UpdatePedido(Pedido cadastro);
    }
}