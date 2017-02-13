namespace Carrinho.Core.DTOs
{
    public class ItemCarrinhoDTO
    {
        public int Id { get; set; }
        public string SKU { get; set; }
        public string ImagemPequena { get; set; }
        public string Descricao { get; set; }
        public string SoldAndDeliveredBy { get; set; }
        public decimal Price { get; set; }
        public decimal? OldPrice { get; set; }
        public int Quantity { get; set; }
        public decimal Subtotal
        {
            get
            {
                return Quantity * Price;
            }
        }
    }
}