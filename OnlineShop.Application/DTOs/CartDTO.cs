namespace OnlineShop.Application.DTOs
{
    public class CartDTO
    {
        public Guid Id { get; set; }
        public string UserId { get; set; }
        public List<CartPositionDTO> Positions { get; set; }
    }
}
