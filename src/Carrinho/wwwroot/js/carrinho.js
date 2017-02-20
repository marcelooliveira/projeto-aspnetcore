class Carrinho {
    constructor(model) {
        this.model = model;
    }

    //Obtém o item do pedido a partir do elemento HTML
    getItemFromElement(el) {
        var itemId = $(el).parents('.item-pedido').attr('item-id');
        return this.getItem(itemId);
    }

    //Obtém o item do pedido a partir do id
    getItem(itemId) {
        return this.model.itemsCarrinho
                    .filter(i => i.id == itemId)[0];
    }

    //Obtém o elemento do item HTML a partir do itemId
    getItemPedidoEl(itemId) {
        return $('.item-pedido[item-id=' + itemId + ']');
    }

    //Obtém o elemento da quantidade do item HTML a partir do itemId
    getItemPedidoQtdeEl(itemId) {
        return this.getItemPedidoEl(itemId).find('input.qtde');
    }

    //Obtém o elemento do preço do item HTML a partir do itemId
    getItemPedidoPrecoEl(itemId) {
        return this.getItemPedidoEl(itemId).find('.preco');
    }

    //Obtém o elemento do subtotal do item HTML a partir do itemId
    getItemPedidoSubtotalEl(itemId) {
        return this.getItemPedidoEl(itemId).find('.subtotal');
    }

    getResumoEl() {
        return $('.resumo');
    }

    getResumoQtdeItensEl() {
        return $('.resumo').find('.qtde-itens');
    }

    getResumoSubtotalEl() {
        return $('.resumo').find('.subtotal');
    }

    getResumoLinhaDescontoEl() {
        return $('.resumo').find('.linha-desconto');
    }

    getResumoTaxaDescontoEl() {
        return $('.resumo').find('.taxa-desconto');
    }

    getResumoValorDescontoEl() {
        return $('.resumo').find('.valor-desconto');
    }

    getResumoTotalEl() {
        return $('.resumo').find('.total');
    }

    //Roda quando o usuário decrementa a quantidade do item
    handleDecremento(el) {
        var item = this.getItemFromElement(el);
        this.postQuantidade(item, item.quantidade - 1);
    }

    //Roda quando o usuário incrementa a quantidade do item
    handleIncremento(el) {
        var item = this.getItemFromElement(el);
        this.postQuantidade(item, item.quantidade + 1);
    }

    //Roda quando o usuário altera quantidade do item
    handleQuantidadeChanged(el) {
        var novaQtde = 1;
        var val = $(el).val();
        if (val && !isNaN(val))
            novaQtde = parseInt(val);
        var item = this.getItemFromElement(el);
        this.postQuantidade(item, novaQtde);
    }

    //Faz requisição AJAX para alterar quantidade
    postQuantidade(item, novaQtde, callback) {
        $('.overlay').show();

        var data = {
            SKU: item.sku,
            Quantidade: novaQtde,
            Preco: item.preco
        }

        $.ajax({
            type: 'POST',
            url: '/api/Carrinho',
            dataType: 'json',
            contentType: 'application/json; charset=utf-8',
            data: JSON.stringify(data)
        }).done(function (novoCarrinho) {
            //this.updateState({ Quantidade: item.quantidade, Subtotal: item.subtotal });
            this.handleCarrinhoChange(novoCarrinho, item);
            return;
        }.bind(this))
        .always(function () {
            $('.overlay').hide();
        });
    }

    //Executa quando há alteração no carrinho
    handleCarrinhoChange(novoCarrinho, itemCarrinho) {
        this.model = novoCarrinho;
        this.atualizaItem(novoCarrinho, itemCarrinho);
        this.atualizaResumo(novoCarrinho);
    }

    atualizaItem(novoCarrinho, itemCarrinho) {
        var item = this.getItem(itemCarrinho.id)
        var itemPedidoQtdeEl = this.getItemPedidoQtdeEl(itemCarrinho.id);
        $(itemPedidoQtdeEl).val(item.quantidade);
        if (item.quantidade == 0) {
            this.getItemPedidoEl(itemCarrinho.id).remove();
        }
        var subtotalEl = this.getItemPedidoSubtotalEl(itemCarrinho.id);
        $(subtotalEl).html(item.subtotal.duasCasas());
    }

    atualizaResumo(novoCarrinho) {
        var qtdeItensEl = this.getResumoQtdeItensEl();
        $(qtdeItensEl).html(novoCarrinho
            .itemsCarrinho
            .filter(i => i.quantidade > 0)
            .length);
        var subtotalEl = this.getResumoSubtotalEl();
        $(subtotalEl).html(novoCarrinho.subtotal.duasCasas());
        var linhaDescontoEl = this.getResumoLinhaDescontoEl();
        if (novoCarrinho.valorDesconto > 0) {
            $(linhaDescontoEl).show();
        }
        else {
            $(linhaDescontoEl).hide();
        }
        var taxaDescontoEl = this.getResumoTaxaDescontoEl();
        $(taxaDescontoEl).html(novoCarrinho.taxaDesconto.duasCasas());
        var valorDescontoEl = this.getResumoValorDescontoEl();
        $(valorDescontoEl).html(novoCarrinho.valorDesconto.duasCasas());
        var totalEl = this.getResumoTotalEl();
        $(totalEl).html(novoCarrinho.total.duasCasas());
    }
}

var carrinho;
(function ($) {
    carrinho = new Carrinho(@Html.Raw(Json.Serialize(Model)));
}(jQuery));

Number.prototype.duasCasas = function () {
    return this.toFixed(2).replace('.', ',');
}
