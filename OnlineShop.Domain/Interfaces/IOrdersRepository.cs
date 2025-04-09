using OnlineShop.Domain.Entities;
using OnlineShop.Domain.Enums;

namespace OnlineShop.Domain.Interfaces
{
    public interface IOrdersRepository
    {
        Task<Order?> GetByIdAsync(Guid id);
        Task<Order?> GetLastByUserIdAsync(string userId);
        Task<List<Order>> GetByUserIdAsync(string id);
        Task<List<Order>> GetAllAsync();
        Task<int> GetCountAsync();
        Task AddAsync(Order order);
        Task<bool> UpdateStatusAsync(Guid id, OrderStatusEnum status);
    }
}