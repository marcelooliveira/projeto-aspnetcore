using Carrinho.Core.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Carrinho.Core
{
    public interface IPedidoManager
    {
        CarrinhoDTO GetCarrinho();
        void SaveCarrinho(ItemCarrinhoDTO modifiedItem);
        CarrinhoDTO GetCarrinho(List<ItemCarrinhoDTO> itemsCarrinho);
        ResumoPedidoDTO GetResumoPedido();
        List<ProdutoDTO> GetProdutos();
        Context InicializaDB();
    }
}
