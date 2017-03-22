class Carrinho {
    getItemPedidoEl(el) {
        return $(el).parents('.item-pedido');
    }

    getItemPedidoId(el) {
        return this.getItemPedidoEl(el).attr('item-id');
    }

    getItemPedidoQuantidade(el) {
        return this.getItemPedidoEl(el).find('input').val();
    }

    setItemPedidoSubtotal(el, itemPedido) {
        return this.getItemPedidoEl(el).find('.subtotal').html(itemPedido.subtotal.duasCasas());
    }

    setItemPedidoQuantidade(el, itemPedido) {
        return this.getItemPedidoEl(el).find('input').val(itemPedido.quantidade);
    }

    setCarrinhoQuantidade(el, carrinho) {
        return $('.quantidade-itens-pedido').html(carrinho.itensCarrinho.length);
    }

    setCarrinhoTotal(el, carrinho) {
        $('.total-carrinho').html(carrinho.total.duasCasas());
    }

    handleIncremento(el) {
        var itemId = this.getItemPedidoId(el);
        var novaQtde = this.getItemPedidoQuantidade(el);

        var data = {
            Id: itemId,
            PedidoId: parseInt(Cookies.get("pedidoId")),
            Quantidade: parseInt(novaQtde) + 1
        }

        this.postCarrinho(el, data);
    }

    handleDecremento(el) {
        var itemId = this.getItemPedidoId(el);
        var novaQtde = this.getItemPedidoQuantidade(el);

        var data = {
            Id: itemId,
            PedidoId: parseInt(Cookies.get("pedidoId")),
            Quantidade: parseInt(novaQtde) - 1
        }

        this.postCarrinho(el, data);
    }

    handleAlterarQuantidade(input) {
        var itemId = this.getItemPedidoId(input);
        var novaQtde = $(input).val();

        var data = {
            Id: itemId,
            PedidoId: parseInt(Cookies.get("pedidoId")),
            Quantidade: parseInt(novaQtde)
        }

        this.postCarrinho(input, data);
    }

    postCarrinho(el, data) {
        $.ajax({
            dataType: 'json',
            data: JSON.stringify(data),
            contentType: 'application/json',
            type: 'POST',
            url: '/api/Carrinho'
        }).done(function (carrinho) {
                if (carrinho.itemPedido
                && carrinho.itemPedido.quantidade > 0) {
                    this.setItemPedidoSubtotal(el, carrinho.itemPedido);
                    this.setItemPedidoQuantidade(el, carrinho.itemPedido);
                }
                else {
                    this.getItemPedidoEl(el).remove();
                    if (carrinho.carrinhoViewModel.itensCarrinho.length == 0) {
                        $('#btnFinalizar').remove();
                    }
                }
                this.setCarrinhoQuantidade(el, carrinho.carrinhoViewModel);
                this.setCarrinhoTotal(el, carrinho.carrinhoViewModel);
        }.bind(this));
    }
}

var carrinho;
(function ($) {
    carrinho = new Carrinho();

    $(document).ajaxStart(function () {
        $('#ajax_loader').show();
    })

    $(document).ajaxStop(function () {
        $('#ajax_loader').hide();
    })
}(jQuery));

Number.prototype.duasCasas = function () {
    return this.toFixed(2).replace('.', ',');
}

