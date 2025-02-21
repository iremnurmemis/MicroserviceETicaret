namespace MultiShop.Basket.Dtos
{
    public class BasketItemDto
    {
        public string ProductId { get; set; } //product id ler mongodan gelicek string
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }  
    }
}
