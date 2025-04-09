using OnlineShop.Domain.Entities;
using OnlineShop.Domain.Enums;

namespace OnlineShop.Domain.Interfaces
{
    public interface IOrdersRepository
    {
        Task<Order?> GetByIdAsync(Guid id);
        Task<Order?> GetLastByUserIdAsync(string userId);
        Task<List<CartPosition>> GetCartPositionsAsync(Guid id);
        Task<List<Order>> GetByUserIdAsync(string id);
        Task<List<Order>> GetAllAsync();
        Task<bool> AddAsync(Guid cartId, string userId, ContactInfo contactInfo);
        Task<bool> UpdateStatusAsync(Guid id, OrderStatusEnum status);
        Task<int> IncreaseNumberAsync();
    }
}