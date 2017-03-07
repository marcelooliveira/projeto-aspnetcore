Number.prototype.duasCasas = function () {
    return this.toFixed(2).replace('.', ',');
}

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

    setItemPedidoSubtotal(el, itemPedido) {
        var subtotal = itemPedido.subtotal.duasCasas();
        return this.getItemPedidoEl(el).find('.subtotal').html(subtotal);
    }

    setItemPedidoQuantidade(el, itemPedido) {
        return this.getItemPedidoEl(el).find('input').val(itemPedido.quantidade);
    }

    postCarrinho(el, data) {
        $.ajax({
            type: 'POST',
            url: '/api/Carrinho',
            dataType: 'json',
            contentType: 'application/json',
            data: JSON.stringify(data)
        }).done(function (novoItem) {
            this.setItemPedidoSubtotal(el, novoItem);
            this.setItemPedidoQuantidade(el, novoItem);
        }.bind(this));
    }

    handleIncremento(botao) {
        var itemId = this.getItemPedidoId(botao);
        var novaQtde = this.getItemPedidoQuantidade(botao);

        var data = {
            Id: itemId,
            Quantidade: parseInt(novaQtde) + 1
        };

        this.postCarrinho(botao, data);
    }

    handleDecremento(botao) {
        var itemId = this.getItemPedidoId(botao);
        var novaQtde = this.getItemPedidoQuantidade(botao);

        var data = {
            Id: itemId,
            Quantidade: parseInt(novaQtde) - 1
        };

        this.postCarrinho(botao, data);
    }
}

var carrinho;
(function ($) {
    carrinho = new Carrinho();
}(jQuery));
