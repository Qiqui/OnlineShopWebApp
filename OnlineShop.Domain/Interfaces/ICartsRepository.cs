using OnlineShop.Domain.Entities;

namespace OnlineShop.Domain.Interfaces
{
    public interface ICartsRepository
    {
        Task<Cart?> GetById(string userId);
        Task Add(User user);
        Task Add(Product product, User user);
        Task Remove(Product product, User user);
        Task<bool> Clear(string userId);
    }
}
