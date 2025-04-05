using OnlineShop.Domain.Entities;
using OnlineShop.Domain.Enums;

namespace OnlineShop.Domain.Interfaces
{
    public interface IOrdersRepository
    {
        Task<Order?> GetById(Guid id);
        Task<Order?> GetLastByUserId(string userId);
        Task<List<CartPosition>> GetCartPositions(Guid id);
        Task<List<Order>> GetByUserId(string id);
        Task<List<Order>> GetAll();
        Task<bool> Add(Guid cartId, string userId, ContactInfo contactInfo);
        Task<bool> UpdateStatus(Guid id, OrderStatusEnum status);
        Task<int> IncreaseNumber();
    }
}