namespace OnlineShopWebApp.Models
{
    public class CartPositionViewModel
    {
        public Guid Id { get; set; }
        public int Quantity { get; set; }
        public ProductViewModel Product { get; set; }
        public CartViewModel Cart { get; set; }

        public decimal GetTotalPrice()
        {
            return Quantity * Product.Cost;
        }
    }
}
