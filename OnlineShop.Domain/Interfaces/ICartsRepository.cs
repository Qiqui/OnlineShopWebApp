using OnlineShop.Domain.Entities;

namespace OnlineShop.Domain.Interfaces
{
    public interface ICartsRepository
    {
        Task<Cart?> GetByIdAsync(string userId);
        Task<Cart> CreateCartAsync(string userId);
        Task UpdateAsync(Cart cart);
    }
}
