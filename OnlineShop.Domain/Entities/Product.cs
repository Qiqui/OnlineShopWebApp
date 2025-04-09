using OnlineShop.Domain.Enums;

namespace OnlineShop.Domain.Entities
{
    public class Product
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public GenderEnum Gender { get; set; }
        public decimal Cost { get; set; }
        public string Description { get; set; }
        public ColorEnum Color { get; set; }
        public CollectionEnum Collection { get; set; }
        public MaterialEnum Material { get; set; }
        public BrandEnum Brand { get; set; }
        public CategoryEnum Category { get; set; }
        public List<string> ImagePaths { get; set; } = new List<string>();
        public List<CartPosition> CartPositions { get; set; }
        public List<Comparison> Compares{ get; set; }
        public List<Favourites> Favourites { get; set; }

        public Product()
        {
            CartPositions = new List<CartPosition>();
            Compares = new List<Comparison>();
            Favourites = new List<Favourites>();
        }
    }
}