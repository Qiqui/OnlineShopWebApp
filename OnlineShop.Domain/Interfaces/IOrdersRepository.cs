using OnlineShop.Domain.Entities;
using OnlineShop.Domain.Enums;

namespace OnlineShop.Domain.Interfaces
{
    public interface IOrdersRepository
    {
        Order TryGetById(Guid id);
        Order TryGetLastByUserId(string userId);
        List<CartPosition> GetCartPositions(Guid id);
        List<Order> TryGetByUserId(string id);
        List<Order> GetAll();
        bool Add(Guid cartId, string userId, ContactInfo contactInfo);
        bool UpdateStatus(Guid id, OrderStatusEnum status);
        int IncreaseNumber();
    }
}