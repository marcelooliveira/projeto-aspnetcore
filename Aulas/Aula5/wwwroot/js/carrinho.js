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

    setCarrinhoQuantidade(el, carrinho) {
        $('.quantidade-itens-pedido').html(carrinho.itensCarrinho.length);
    }

    setCarrinhoTotal(el, carrinho) {
        $('.total-carrinho').html(carrinho.total.duasCasas());
    }

    postCarrinho(el, data) {
        $('#modal-loading').modal('show');

        $.ajax({
            type: 'POST',
            url: '/api/Carrinho',
            dataType: 'json',
            contentType: 'application/json',
            data: JSON.stringify(data)
        }).done(function (carrinho) {
            if (carrinho.itemPedido
                && carrinho.itemPedido.quantidade > 0) {
                this.setItemPedidoSubtotal(el, carrinho.itemPedido);
                this.setItemPedidoQuantidade(el, carrinho.itemPedido);
            }
            else {
                this.getItemPedidoEl(el).remove();
            }
            this.setCarrinhoQuantidade(el, carrinho.carrinhoViewModel);
            this.setCarrinhoTotal(el, carrinho.carrinhoViewModel);
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

    handleAlterarQuantidade(input) {
        var itemId = this.getItemPedidoId(input);
        var novaQtde = $(input).val();

        var data = {
            Id: itemId,
            Quantidade: parseInt(novaQtde)
        };

        this.postCarrinho(input, data);
    }
}

var carrinho;
(function ($) {
    carrinho = new Carrinho();

    $(document).ajaxStart(function () {
        $("#ajax_loader").show();
    });

    $(document).ajaxStop(function () {
        $("#ajax_loader").hide();
    });
}(jQuery));
