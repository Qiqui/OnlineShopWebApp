namespace OnlineShop.Db.Models
{
    public class Favourites
    {
        public Guid Id { get; set; }
        public User User { get; set; }
        public List<Product> Products { get; }

        public Favourites()
        {
            Products = new List<Product>();
        }
    }
}
