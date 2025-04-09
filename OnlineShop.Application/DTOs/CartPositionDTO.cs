namespace OnlineShop.Application.DTOs
{
    public class CartPositionDTO
    {
        public Guid Id { get; set; }
        public int Quantity { get; set; }
        public ProductDTO Product { get; set; }
        public CartDTO Cart { get; set; }
    }
}
