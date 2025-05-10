namespace OnlineShop.Application.DTOs
{
    public class OrderPositionDTO
    {
        public Guid Id { get; set; }
        public int Quantity { get; set; }
        public ProductDTO Product { get; set; }
        public OrderDTO Order { get; set; }
    }
}
