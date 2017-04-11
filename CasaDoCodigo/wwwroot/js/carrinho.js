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
        }.bind(this));
    }

    setQuantidade(itemPedido) {
        $('[item-id=' + itemPedido.id + ']')
            .find('input').val(itemPedido.quantidade);
    }
}

var carrinho = new Carrinho();



