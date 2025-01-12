namespace OnlineShopWebApp.Models
{
    public class CartViewModel
    {
        public Guid Id { get; set; }
        public List<CartPositionViewModel> Positions { get; set; } = new List<CartPositionViewModel>();
        public int Amount
        {
            get => Positions.Sum(position => position.Quantity);
        }

        public decimal GetTotalCost()
        {
            return Positions.Sum(product => product.GetTotalPrice());
        }
    }
}
