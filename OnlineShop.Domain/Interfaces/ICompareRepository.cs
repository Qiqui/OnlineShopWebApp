using OnlineShop.Domain.Entities;

namespace OnlineShop.Domain.Interfaces
{
    public interface ICompareRepository
    {
        Task<Compare> TryGetById(string userId);
        Task<Product> TryGetProductById(Guid Id);
        Task Add(Guid id, string userId);
        Task Remove(Guid id, string userId);
    }
}
