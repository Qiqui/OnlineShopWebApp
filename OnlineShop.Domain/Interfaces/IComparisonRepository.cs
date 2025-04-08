using OnlineShop.Domain.Entities;

namespace OnlineShop.Domain.Interfaces
{
    public interface IComparisonRepository
    {
        Task<Comparison?> GetByIdAsync(string userId);
        Task<Product?> GetProductByIdAsync(Guid Id);
        Task AddAsync(Guid id, string userId);
        Task RemoveAsync(Guid id, string userId);
    }
}
