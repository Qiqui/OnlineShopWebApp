namespace OnlineShopWebApp.Models
{
    public class OrderPositionViewModel
    {
        public Guid Id { get; set; }
        public int Quantity { get; set; }
        public ProductViewModel Product { get; set; }
        public OrderViewModel Cart { get; set; }

        public decimal GetTotalPrice()
        {
            return Quantity * Product.Cost;
        }
    }
}
