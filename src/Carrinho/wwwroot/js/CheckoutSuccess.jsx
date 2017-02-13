class CheckoutSuccessView extends React.Component {
    constructor(props) {
        super(props);
    }
    render() {
        return (
            <div>
                <br />
                <Row>
                    <span className="fa fa-check-circle"></span>
                </Row>                
                <h3 className="text-center">
                    <span>Seu pedido foi enviado. Obrigado por comprar conosco!</span>
                </h3>
                <h4 className="text-center">
                    <span>Nº do pedido:</span>
                    <span className="green">{this.props.model.orderNumber}</span>
                </h4>
                <br />
                <h3>
                    Dados do pedido
                </h3>

                <Panel>
                    <Row>
                        <Column md={6}>
                            <h4>
                                <span>Nº do pedido:</span>
                                <span className="green">{this.props.model.orderNumber}</span>
                            </h4>
                            <p>
                                Você receberá um e-mail de confirmação com os detalhes do seu pedido. Por favor verifique suas configurações anti-spam do seu provedor de e-mail.
                            </p>
                        </Column>
                        <Column md={6}>
                            <h4>Forma de pagamento</h4>
                            <div className="boleto">
                                <p><i className="fa fa-paypal leading-icon" aria-hidden="true"></i> Paypal</p>
                                <p className="offset30"><Dollars val={this.props.model.total} /></p>
                            </div>
                        </Column>
                    </Row>
                    <Row className="gray row-eq-height border-top border-bottom">
                        <Column md={3}>
                            <h4><span className="fa fa-user leading-icon"></span>Seus dados</h4>
                            <p className="offset30">{this.props.model.clienteInfo.nome}</p>
                            <p className="offset30">{this.props.model.clienteInfo.fone}</p>
                        </Column>
                        <Column md={3} className="border-right">
                            <br />
                            <br />
                            <p>{this.props.model.clienteInfo.Email}</p>
                        </Column>
                        <Column md={6}>
                            <h4><span className="fa fa-home leading-icon"></span>Endereço de entrega</h4>
                            <p className="offset30">{this.props.model.clienteInfo.enderecoEntrega}</p>
                        </Column>
                    </Row>
                    <Row className="gray">
                        <Column md={6}>
                            <h4><span className="fa fa-gift leading-icon"></span>Entrega</h4>
                        </Column>
                        <Column md={6}>
                            <br />
                            <p className="float-right">
                                O tempo de entrega é {this.props.model.deliveryUpTo} dias
                            </p>
                        </Column>
                    </Row>
                    <Row className="gray">
                        <Column md={6}>
                            <p className="offset30"><b>Descrição do produto</b></p>
                        </Column>
                        <Column md={6} className="pull-right">
                            <p><b className="float-right">Quantidade</b></p>
                        </Column>
                    </Row>
                    { this.props.model.itemsCarrinho.map(item =>
                        <Row className="gray">
                            <Column md={6}>
                                <div className="offset30 truncate">
                                    <span>•</span>
                                    <span>{item.descricao}</span>
                                </div>
                            </Column>
                            <Column md={6} className="pull-right">
                                <p className="float-right">{item.quantity}</p>
                            </Column>
                        </Row>
                            )
                    }
                </Panel>

                <Row>
                    <Column md={9}></Column>
                    <a href="/">
                        <Column md={2}>
                            <Button bsStyle="success">Voltar para o catálogo de produtos</Button>
                        </Column>
                    </a>
                </Row>
            </div>
      );
    }
}

CheckoutSuccessView.propTypes = {

};