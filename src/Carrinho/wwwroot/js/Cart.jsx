var ItemCarrinho = React.createClass({
    getInitialState: function () {
        var item = this.props.model;
        return {
            SKU: item.sku,
            ImagemPequena: item.imagemPequena,
            ImagemGrande: item.imagemGrande,
            Descricao: item.descricao,
            VendidoEEntreguePor: item.vendidoEEntreguePor,
            Preco: item.preco,
            Quantity: item.quantidade,
            Subtotal: item.subtotal
        };
    },
    updateState: function (change) {
        this.setState(Object.assign({}, this.state, change))
    },
    handleIncremento: function () {
        this.postQuantidade(this.state.quantidade + 1);
    },
    handleDecremento: function () {
        this.postQuantidade(this.state.quantidade - 1);
    },
    removeItem: function () {
        this.postQuantidade(0);
    },
    postQuantidade: function (quantidade, callback) {
        $('.overlay').show();

        $.ajax({
            url: '/api/Cart',
            type: 'post',
            data: {
                SKU: this.props.model.SKU,
                Quantidade: quantidade,
                Preco: this.props.model.Preco
            },
            headers: {
                'RequestVerificationToken': this.props.TokenHeaderValue
            },
            dataType: 'json'
        }).done(function (data) {
            for (var item of data.ItemsCarrinho) {
                if (item.SKU == this.props.model.SKU) {
                    this.updateState({ Quantidade: item.Quantidade, Subtotal: item.Subtotal });
                    this.props.handleCarrinhoChange(data, item);
                    return;
                }
            }
        }.bind(this))
        .always(function () {
            $('.overlay').hide();
        });;
    },
    handleQuantidadeChanged: function (event) {
        var newQty = 1;
        var val = event.target.value;
        if (val && !isNaN(val))
            newQty = parseInt(val);
        this.postQuantidade(newQty);
    },
    render: function () {
        return (
            <Row className="vertical-align">
                <Column md={2} className="justify-left">
                    <Row className="fullwidth">
                        <Column md={3}>
                            <img src={'../' + this.state.ImagemPequena} width="80" height="80" />
                        </Column>
                    </Row>
                </Column>
                <Column md={4} className="justify-left">
                    <Row className="fullwidth">
                        <Column md={9}>
                            <span>{this.state.Descricao}</span>
                        </Column>
                    </Row>
                </Column>
                <Column md={2} className="green justify-center">
                    <Dollars val={this.state.Preco } />
                </Column>
                <Column md={2} className="justify-center">
                    <div className="text-center">
                        <ButtonGroup>
                            <input type="button" className="btn btn-default" value="-" onClick={this.handleDecremento} />
                            <input type="text" className="btn" value={this.state.Quantidade} onChange={this.handleQuantidadeChanged } />
                            <input type="button" className="btn btn-default" value="+" onClick={this.handleIncremento} />
                        </ButtonGroup>
                        <a onClick={this.removeItem} className="remove pointer">Remove</a>
                    </div>
                </Column>
                <Column md={2} className="green justify-right">
                    <Dollars val={this.state.Subtotal} />
                </Column>
            </Row>
        );
    }
})

class CartView extends React.Component {

    constructor(props) {
        super(props);
        this.state = {};
        var items = [];

        var item;
        for (var i = 0; i < (this.props.model.itemsCarrinho || []).length; i++) {
            item = this.props.model.itemsCarrinho[i];

            items.push({
                id: item.id,
                sku: item.sku,
                imagemPequena: item.imagemPequena,
                imagemGrande: item.imagemGrande,
                descricao: item.descricao,
                vendidoEEntreguePor: item.vendidoEEntreguePor,
                preco: item.preco,
                quantity: item.quantidade,
                subtotal: item.subtotal
            });
        }

        this.state = {
            canFinishOrder: true,
            items: items,
            Subtotal: this.props.model.Subtotal,
            TaxaDesconto: this.props.model.TaxaDesconto,
            ValorDesconto: this.props.model.ValorDesconto,
            Total: this.props.model.Total
        };
    }

    handleCarrinhoChange(cart, itemCarrinho) {
        var newState = Object.assign({}, this.state, {
            Subtotal: cart.Subtotal,
            TaxaDesconto: cart.TaxaDesconto,
            ValorDesconto: cart.ValorDesconto,
            Total: cart.Total
        });
        if (itemCarrinho.Quantity == 0) {
            newState.items.splice(newState.items.findIndex(i =>
                i.SKU == itemCarrinho.SKU), 1);
        }
        this.setState(newState);
    }

    render() {
        const header = (<Row className="vertical-align">
                                    <Column md={6} className="justify-left">item(s)</Column>
                                    <Column md={2} className="justify-center">preço unitário</Column>
                                    <Column md={2} className="justify-center">quantidade</Column>
                                    <Column md={2} className="justify-right">subtotal</Column>
        </Row>);

        const body = (this.state.items.map(item => {
            return <ItemCarrinho key={item.SKU} model={item}
                             handleCarrinhoChange={this.handleCarrinhoChange.bind(this)}
                             TokenHeaderValue={this.props.TokenHeaderValue} />;
        }
        ));

        const footer = (<Row>
                            <Column md={7}></Column>
                            <Column md={5} className="my-children-have-dividers">
                                <Row className="vertical-align">
                                    <Column md={8} className="justify-right">
                                        Subtotal ({this.state.items.length}<Pluralize value={this.state.items.length} singular="item" plural="items" />):
                                    </Column>
                                    <Column md={4} className="green justify-right">
                                        <span>
                                            <Dollars val={this.state.Subtotal} />
                                        </span>
                                    </Column>
                                </Row>
                                { this.state.taxaDesconto
                                ?
                                    <Row className="vertical-align">
                                        <Column md={8} className="justify-right">
                                            Desconto (<span>{this.state.taxaDesconto}</span>%):
                                        </Column>
                                    <Column md={4} className="green justify-right">
                                        <span>
                                            <Dollars val={this.state.valorDesconto} />
                                        </span>
                                    </Column>
                                    </Row>
                                    : null
                                }
                                <Row className="vertical-align">
                                    <Column md={12} className="justify-right">
                                    <h3>
                                        Total:&nbsp;
                                        <span className="green">
                                            <Dollars val={this.state.total} />
                                        </span>
                                    </h3>
                                    </Column>
                                </Row>
                            </Column>
        </Row>);

        return (
                <div className="cart">
                    {
                        this.state.items.length == 0 ? null :
                        <div>
                        {/* TITLE */}
                        <h3>Meu carrinho ({ this.state.items.length}<Pluralize value={this.state.items.length} singular="item" plural="items" />)</h3>
                        {/* NAVIGATION BUTTONS */}
                        <Row>
                            <Column md={3}>
                                <a href={this.props.urlNewProduct}>
                                    <button type="button" className="btn btn-success">Adicionar novo</button>
                                </a>
                            </Column>
                            <Column md={3} className="pull-right">
                                <a href={this.props.urlCheckoutSuccess}>
                                    <button type="button" className="btn btn-success pull-right">Finalizar pedido</button>
                                </a>
                            </Column>
                        </Row>
                        {/* NAVIGATION BUTTONS */}
                        <br />
                        {/* CART PANEL */}
                        <Panel header={header} footer={footer}>
                            {body}
                        </Panel>
                        {/* CART PANEL */}

                        {/* NAVIGATION BUTTONS */}
                        <Row>
                            <Column md={3}>
                                <a href={this.props.urlNewProduct}>
                                    <button type="button" className="btn btn-success">Adicionar novo</button>
                                </a>
                            </Column>
                            <Column md={3} className="pull-right">
                                <a href={this.props.urlCheckoutSuccess}>
                                    <button type="button" className="btn btn-success pull-right">Finalizar pedido</button>
                                </a>
                            </Column>
                        </Row>
                        {/* NAVIGATION BUTTONS */}
                        </div>
                    }
                    {
                    this.state.items.length > 0
                    ? null
                    :
                        <div>
                            <h1><br /><br />:(</h1>
                            <div>
                                <h1>
                                    Oops! Your shopping cart is empty.
                                </h1>
                                <br />
                                <div className="empty-cart-content-message">
                                    Enter more products and resume shopping.
                                </div>
                                <br />
                                <div>
                                    {
                                        this.state.canFinishOrder
                                        ?
                                        <a href={this.props.urlNewProduct}>
                                            <button type="button" className="btn btn-success">Enter new product</button>
                                        </a>
                                        : null
                                    }
                                </div>
                            </div>
                        </div>
                    }
                </div>
      );
    }
}
