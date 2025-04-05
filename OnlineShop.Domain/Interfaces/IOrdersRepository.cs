using OnlineShop.Domain.Entities;
using OnlineShop.Domain.Enums;

namespace OnlineShop.Domain.Interfaces
{
    public interface IOrdersRepository
    {
        Task<Order> TryGetById(Guid id);
        Task<Order> TryGetLastByUserId(string userId);
        Task<List<CartPosition>> GetCartPositions(Guid id);
        Task<List<Order>> TryGetByUserId(string id);
        Task<List<Order>> GetAll();
        Task<bool> Add(Guid cartId, string userId, ContactInfo contactInfo);
        Task<bool> UpdateStatus(Guid id, OrderStatusEnum status);
        Task<int> IncreaseNumber();
    }
}