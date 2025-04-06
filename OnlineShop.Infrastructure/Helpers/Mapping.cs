using OnlineShop.Domain.Entities;

namespace OnlineShop.Infrastructure.Helpers
{
    public static class Mapping
    {
        public static List<OrderPosition> ToOrderPositions(this List<CartPosition> positions)
        {
            var orderPositions = new List<OrderPosition>();
            foreach (var position in positions)
            {
                var orderPosition = new OrderPosition
                {
                    Quantity = position.Quantity,
                    Product = position.Product
                };

                orderPositions.Add(orderPosition);
            }

            return orderPositions;
        }
    }
}
