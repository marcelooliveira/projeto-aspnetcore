class Carrinho {
    getItemPedidoEl(el) {
        return $(el).parents('.item-pedido');
    }

    getItemPedidoId(botao) {
        return this.getItemPedidoEl(botao).attr('item-id');
    }

    getItemPedidoQuantidade(botao) {
        return this.getItemPedidoEl(botao).find('input').val();
    }

    handleIncremento(botao) {
        var itemId = this.getItemPedidoId(botao);
        var novaQtde = this.getItemPedidoQuantidade(botao);

        var data = {
            Id: itemId,
            Quantidade: parseInt(novaQtde) + 1
        };

        $.ajax({
            type: 'POST',
            url: '/api/Carrinho',
            dataType: 'json',
            contentType: 'application/json',
            data: JSON.stringify(data)
        });
    }
}

var carrinho;
(function ($) {
    carrinho = new Carrinho();
}(jQuery));
