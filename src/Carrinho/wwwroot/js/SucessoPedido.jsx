class SucessoPedidoView extends React.Component {
    constructor(props) {
        super(props);
    }
    render() {
        return (
            <div>
                <br />
                <div className="row">
                    <span className="fa fa-check-circle"></span>
                </div>                
                <h3 className="text-center">
                    <span>Seu pedido foi enviado. Obrigado por comprar conosco!</span>
                </h3>
                <h4 className="text-center">
                    <span>Nº do pedido:</span>
                    <span className="green">{this.props.model.numeroPedido}</span>
                </h4>
                <br />
                <h3>
                    Dados do pedido
                </h3>

                <Panel>
                    <div className="row">
                        <div className="col col-md-6">
                            <h4>
                                <span>Nº do pedido:</span>
                                <span className="green">{this.props.model.numeroPedido}</span>
                            </h4>
                            <p>
                                Você receberá um e-mail de confirmação com os detalhes do seu pedido. Por favor verifique suas configurações anti-spam do seu provedor de e-mail.
                            </p>
                        </div>
                        <div className="col col-md-6">
                            <h4>Forma de pagamento</h4>
                            <div className="boleto">
                                <p><i className="fa fa-paypal leading-icon" aria-hidden="true"></i> Paypal</p>
                                <p className="offset30"><Dollars val={this.props.model.total} /></p>
                            </div>
                        </div>
                    </div>
                    <div className="row gray row-eq-height border-top border-bottom">
                        <div className="col col-md-3">
                            <h4><span className="fa fa-user leading-icon"></span>Seus dados</h4>
                            <p className="offset30">{this.props.model.clienteInfo.nome}</p>
                            <p className="offset30">{this.props.model.clienteInfo.fone}</p>
                        </div>
                        <div className="col col-md-3 border-right">
                            <br />
                            <br />
                            <p>{this.props.model.clienteInfo.Email}</p>
                        </div>
                        <div className="col col-md-6">
                            <h4><span className="fa fa-home leading-icon"></span>Endereço de entrega</h4>
                            <p className="offset30">{this.props.model.clienteInfo.enderecoEntrega}</p>
                        </div>
                    </div>
                    <div className="row gray">
                        <div className="col col-md-6">
                            <h4><span className="fa fa-gift leading-icon"></span>Entrega</h4>
                        </div>
                        <div className="col col-md-6">
                            <br />
                            <p className="float-right">
                                O tempo de entrega é {this.props.model.deliveryUpTo} dias
                            </p>
                        </div>
                    </div>
                    <div className="row gray">
                        <div className="col col-md-6">
                            <p className="offset30"><b>Descrição do produto</b></p>
                        </div>
                        <div className="col col-md-6 pull-right">
                            <p><b className="float-right">Quantidade</b></p>
                        </div>
                    </div>
                    { this.props.model.itemsCarrinho.map(item =>
                        <div className="row gray">
                            <div className="col col-md-6">
                                <div className="offset30 truncate">
                                    <span>•</span>
                                    <span>{item.descricao}</span>
                                </div>
                            </div>
                            <div className="col col-md-6 pull-right">
                                <p className="float-right">{item.quantidade}</p>
                            </div>
                        </div>
                            )
                    }
                </Panel>

                <div className="row">
                    <div className="col col-md-9"></div>
                    <a href="/">
                        <div className="col col-md-2">
                            <Button bsStyle="success">Voltar para o catálogo de produtos</Button>
                        </div>
                    </a>
                </div>
            </div>
      );
    }
}

SucessoPedidoView.propTypes = {

};