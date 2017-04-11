class Carrinho {
    clickIncremento(btn) {
        var data = this.getData(btn);
        data.Quantidade++;
        this.postQuantidade(data);
    }

    clickDecremento(btn) {
        var data = this.getData(btn);
        data.Quantidade--;
        this.postQuantidade(data);
    }

    updateQuantidade(input) {
        var data = this.getData(input);
        this.postQuantidade(data);
    }

    getData(elemento) {
        var linhaDoItem = $(elemento).parents('[item-id]');
        var itemId = linhaDoItem.attr('item-id');
        var qtde = linhaDoItem.find('input').val();

        return {
            Id: itemId,
            Quantidade: qtde
        };
    }

    postQuantidade(data) {
        $.ajax({
            url: '/pedido/PostQuantidade',
            type: 'POST',
            contentType: 'application/json',
            data: JSON.stringify(data)
        }).done(function (response) {
            this.setQuantidade(response.itemPedido);
            this.setSubtotal(response.itemPedido);
            this.setTotal(response.carrinhoViewModel);
        }.bind(this));
    }

    setQuantidade(itemPedido) {
        this.getLinhaDoItem(itemPedido)
            .find('input').val(itemPedido.quantidade);
    }

    setSubtotal(itemPedido) {
        this.getLinhaDoItem(itemPedido)
            .find('[subtotal]').html(itemPedido.subtotal.duasCasas());
    }

    setTotal(carrinhoViewModel) {
        $('[total]').html(carrinhoViewModel.total.duasCasas());
    }

    getLinhaDoItem(itemPedido) {
        return $('[item-id=' + itemPedido.id + ']');
    }
}

var carrinho = new Carrinho();

Number.prototype.duasCasas = function () {
    return this.toFixed(2).replace('.', ',');
}