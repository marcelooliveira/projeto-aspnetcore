namespace Carrinho.Core.DTOs
{
    public class ItemCarrinhoDTO
    {
        public int Id { get; set; }
        public string SKU { get; set; }
        public string ImagemPequena { get; set; }
        public string Descricao { get; set; }
        public string VendidoEEntreguePor { get; set; }
        public decimal Preco { get; set; }
        public decimal? PrecoAntigo { get; set; }
        public int Quantidade { get; set; }
        public decimal Subtotal
        {
            get
            {
                return Quantidade * Preco;
            }
        }
    }
}