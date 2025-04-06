namespace OnlineShop.Domain.Entities
{
    public class Favourites
    {
        public Guid Id { get; set; }
        public string UserId { get; set; }
        public List<Product> Products { get; }

        public Favourites()
        {
            Products = new List<Product>();
        }
    }
}
