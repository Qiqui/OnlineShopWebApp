using OnlineShop.Application.Enums;


namespace OnlineShopWebApp.Models
{
    public class OrderViewModel
    {
        public Guid Id { get; set; }
        public string UserId { get; set; }
        public int Number { get; set; }
        public ContactInfoViewModel ContactInfo { get; set; }
        public DateTime CreateDate { get; set; } = DateTime.Now;
        public OrderStatusEnum Status { get; set; }
        public List<CartPositionViewModel> Positions { get; set; } = new List<CartPositionViewModel>();

        public decimal? TotalPrice
        {
            get => Positions?.Sum(position => position.Quantity * position.Product.Cost);
        }

        public OrderViewModel(string userId)
        {
            UserId = userId;
        }

        public OrderViewModel() { }
    }
}