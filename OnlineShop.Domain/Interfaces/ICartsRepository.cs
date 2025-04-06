using OnlineShop.Domain.Entities;

namespace OnlineShop.Domain.Interfaces
{
    public interface ICartsRepository
    {
        Task<Cart?> GetById(string userId);
        Task Add(string userId);
        Task Add(Product product, string userId);
        Task Remove(Product product, string userId);
        Task<bool> Clear(string userId);
    }
}
