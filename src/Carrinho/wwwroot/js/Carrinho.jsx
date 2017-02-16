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
            Quantidade: item.quantidade,
            Subtotal: item.subtotal
        };
    },
    updateState: function (change) {
        this.setState(Object.assign({}, this.state, change))
    },
    handleIncremento: function () {
        this.postQuantidade(this.state.Quantidade + 1);
    },
    handleDecremento: function () {
        this.postQuantidade(this.state.Quantidade - 1);
    },
    removeItem: function () {
        this.postQuantidade(0);
    },
    postQuantidade: function (quantidade, callback) {
        $('.overlay').show();

        var data = {
            SKU: this.props.model.sku,
            Quantidade: quantidade,
            Preco: this.props.model.preco
        }

        $.ajax({
            type: 'POST',
            url: '/api/Carrinho',
            dataType: 'json',
            contentType: 'application/json; charset=utf-8',
            data: JSON.stringify(data)
            //headers: {
            //    'RequestVerificationToken': this.props.TokenHeaderValue
            //},
        }).done(function (data) {
            for (var item of data.itemsCarrinho) {
                if (item.sku == this.props.model.sku) {
                    this.updateState({ Quantidade: item.quantidade, Subtotal: item.subtotal });
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
            <div className="row vertical-align">
                <div className="col col-md-2 justify-left">
                    <div className="row fullwidth">
                        <div className="col col-md-3">
                            <img src={'../' + this.state.ImagemPequena} width="80" height="80" />
                        </div>
                    </div>
                </div>
                <Column md={4} className="justify-left">
                    <div className="row fullwidth">
                        <Column md={9}>
                            <span>{this.state.Descricao}</span>
                        </Column>
                    </div>
                </Column>
                <Column md={2} className="green justify-center">
                    <Dollars val={this.state.Preco} />
                </Column>
                <Column md={2} className="justify-center">
                    <div className="text-center">
                        <ButtonGroup>
                            <input type="button" className="btn btn-default" value="-" onClick={this.handleDecremento} />
                            <input type="text" className="btn" value={this.state.Quantidade} onChange={this.handleQuantidadeChanged } />
                            <input type="button" className="btn btn-default" value="+" onClick={this.handleIncremento} />
                        </ButtonGroup>
                    </div>
                </Column>
                <Column md={2} className="green justify-right">
                    <Dollars val={this.state.Subtotal} />
                </Column>
            </div>
        );
    }
})

class CarrinhoView extends React.Component {

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
                quantidade: item.quantidade,
                subtotal: item.subtotal
            });
        }

        this.state = {
            CanFinishOrder: true,
            Items: items,
            Subtotal: this.props.model.subtotal,
            TaxaDesconto: this.props.model.taxaDesconto,
            ValorDesconto: this.props.model.valorDesconto,
            Total: this.props.model.total
        };
    }

    handleCarrinhoChange(novoCarrinho, itemCarrinho) {
        var change = {
            Items: novoCarrinho.itemsCarrinho,
            Subtotal: novoCarrinho.subtotal,
            TaxaDesconto: novoCarrinho.taxaDesconto,
            ValorDesconto: novoCarrinho.valorDesconto,
            Total: novoCarrinho.total
        }

        this.setState(Object.assign({}, this.state, change));
    }

    render() {
        const header = (
            <div className="row vertical-align">
                <div md={6} className="col col-md-6 justify-center">
                    item(s)
                </div>
                <div md={2} className="col col-md-2 justify-center">
                    preço unitário
                </div>
                <div md={2} className="col col-md-2 justify-center">
                    quantidade
                </div>
                <div md={2} className="col col-md-2 justify-right">
                    subtotal
                </div>
                </div>);

        const body = (this.state.Items
            .filter(item => item.quantidade > 0)
            .map(item => {
                return <ItemCarrinho key={item.sku} model={item}
                                              handleCarrinhoChange={this.handleCarrinhoChange.bind(this)}
                                              TokenHeaderValue={this.props.TokenHeaderValue} />;
            }   
        ));

        const footer = (<div className="row">
                            <div className="col col-md-7"></div>
                            <div className="col col-md-5 my-children-have-dividers">
                                <div className="row vertical-align">
                                    <Column md={8} className="justify-right">
                                        Subtotal ({this.state.Items.length}<Pluralize value={this.state.Items.length} singular="item" plural="items" />):
                                    </Column>
                                    <Column md={4} className="green justify-right">
                                        <span>
                                            <Dollars val={this.state.Subtotal} />
                                        </span>
                                    </Column>
                                </div>
                                { this.state.TaxaDesconto
                                ?
                                    <div className="row vertical-align">
                                        <Column md={8} className="justify-right">
                                            Desconto (<span>{this.state.TaxaDesconto}</span>%):
                                        </Column>
                                    <Column md={4} className="green justify-right">
                                        <span>
                                            <Dollars val={this.state.ValorDesconto} />
                                        </span>
                                    </Column>
                                    </div>
                                    : null
                                }
                                <div className="row vertical-align">
                                    <Column md={12} className="justify-right">
                                    <h3>
                                        Total:&nbsp;
                                        <span className="green">
                                            <Dollars val={this.state.Total} />
                                        </span>
                                    </h3>
                                    </Column>
                                </div>
                            </div>
                        </div>);

        return (<div>
                    <div className="row">
                        <div className="col-md-10 col-md-offset-1">
                            <div className="cart">
                        {
                            this.state.Items.length == 0 ? null :
                            <div>
                            {/* TITLE */}
                            <h3>Meu carrinho ({ this.state.Items.length}<Pluralize value={this.state.Items.length} singular="item" plural="items" />)</h3>
                            {/* NAVIGATION BUTTONS */}
                            <div className="row">
                                <Column md={3}>
                                    <a href={this.props.urlNewProduct}>
                                        <button type="button" className="btn btn-success">Adicionar novo</button>
                                    </a>
                                </Column>
                                <Column md={3} className="pull-right">
                                </Column>
                            </div>
                            {/* NAVIGATION BUTTONS */}
                            <br />
                            {/* CART PANEL */}
                            <Panel header={header} footer={footer}>
                                {body}
                            </Panel>
                            {/* CART PANEL */}

                            {/* NAVIGATION BUTTONS */}
                            <div className="row">
                                <Column md={3}>
                                    <a href={this.props.urlNewProduct}>
                                        <button type="button" className="btn btn-success">Adicionar novo</button>
                                    </a>
                                </Column>
                                <Column md={3} className="pull-right">
                                </Column>
                            </div>
                            {/* NAVIGATION BUTTONS */}
                        </div>
                        }
                        {
                        this.state.Items.length > 0
                        ? null
                        :
                            <div>
                                <h1><br /><br />:(</h1>
                                <div>
                                    <h1>
                                        Ops! Seu carrinho de compras está vazio.
                                    </h1>
                                    <br />
                                    <div className="empty-cart-content-message">
                                        Adicione produtos para continuar a comprar.
                                    </div>
                                    <br />
                                    <div>
                                        {
                                            this.state.CanFinishOrder
                                            ?
                                            <a href={this.props.urlNewProduct}>
                                                <button type="button" className="btn btn-success">Adicione novo produto</button>
                                            </a>
                                            : null
                                        }
                                    </div>
                                </div>
                            </div>
                        }
                    </div>
                        </div>
                    </div>
                    <div className="row">
                        <div className="col-md-6 col-md-offset-3">
                            <form className="form-horizontal" role="form">
                                <fieldset>
                                    <h3>
                                        Endereço de Entrega
                                    </h3>
                                    <div className="form-group">
                                        <label className="col-sm-3 control-label" for="textinput">Endereço</label>
                                        <div className="col-sm-9">
                                            <input type="text" className="form-control"/>
                                        </div>
                                    </div>
                                    <div className="form-group">
                                        <label className="col-sm-3 control-label" for="textinput">Número/Compl.</label>
                                        <div className="col-sm-9">
                                            <input type="text" className="form-control"/>
                                        </div>
                                    </div>
                                    <div className="form-group">
                                        <label className="col-sm-3 control-label" for="textinput">Cidade</label>
                                        <div className="col-sm-9">
                                            <input type="text" className="form-control"/>
                                        </div>
                                    </div>
                                    <div className="form-group">
                                        <label className="col-sm-3 control-label" for="textinput">Estado</label>
                                        <div className="col-sm-3">
                                            <input type="text" className="form-control"/>
                                        </div>
                                        <label className="col-sm-3 control-label" for="textinput">CEP</label>
                                        <div className="col-sm-3">
                                            <input type="text" className="form-control"/>
                                        </div>
                                    </div>
                                </fieldset>
                            </form>
                        </div>
                    </div>
                    <div className="row">
                        <div className="col-md-10 col-md-offset-1">{/* NAVIGATION BUTTONS */}
                            <div className="row">
                                <Column md={3}>
                                </Column>
                                <Column md={3} className="pull-right">
                                    <a href={this.props.urlSucessoPedido}>
                                        <button type="button" className="btn btn-success pull-right">Finalizar pedido</button>
                                    </a>
                                </Column>
                            </div>{/* NAVIGATION BUTTONS */}
                        </div>
                    </div>



                </div>
              );
            }
}
