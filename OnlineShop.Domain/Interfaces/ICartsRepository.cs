using OnlineShop.Domain.Entities;

namespace OnlineShop.Domain.Interfaces
{
    public interface ICartsRepository
    {
        Task<Cart?> GetByIdAsync(string userId);
        Task<Cart> CreateCartAsync(string userId);
        Task AddPositionAsync(Cart cart, Product product);
        Task RemovePositionAsync(Cart cart, Product product);
        Task<bool> ClearAsync(string userId);
    }
}
