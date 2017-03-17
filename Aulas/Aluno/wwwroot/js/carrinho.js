class Carrinho {
    getItemPedido(el) {
        return $(el).parents('.item-pedido');
    }

    getItemPedidoId(el) {
        return this.getItemPedido(el).attr('item-id');
    }

    getItemPedidoQuantidade(el) {
        return this.getItemPedido(el).find('input').val();
    }

    handleIncremento(el) {
        var itemId = this.getItemPedidoId(el);
        var novaQtde = this.getItemPedidoQuantidade(el);

        var data = {
            Id: itemId,
            Quantidade: parseInt(novaQtde) + 1
        }

        $.ajax({
            dataType: 'json',
            data: JSON.stringify(data),
            contentType: 'application/json',
            type: 'POST',
            url: '/api/Carrinho'
        });
    }
}

var carrinho;
(function ($) {
    carrinho = new Carrinho();
}(jQuery));