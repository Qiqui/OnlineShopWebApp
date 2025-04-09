namespace OnlineShop.Application.DTOs
{
    public class FavouritesDTO
    {
        public Guid Id { get; set; }
        public string UserId { get; set; }
        public List<ProductDTO> Products { get; }
    }
}
