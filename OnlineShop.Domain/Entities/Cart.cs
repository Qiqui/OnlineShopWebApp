namespace OnlineShop.Domain.Entities
{
    public class Cart
    {
        public Guid Id { get; set; }
        public string UserId { get; set; }
        public List<CartPosition> Positions { get; set; }

        public Cart()
        {
            Positions = new List<CartPosition>();
        }
    }
}
