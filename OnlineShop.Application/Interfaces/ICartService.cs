using OnlineShop.Domain.Entities;

namespace OnlineShop.Application.Interfaces
{
    public interface ICartService
    {
        /*Task<Cart?> GetById(string userId);
        Task AddPosition(string userId);*/
        Task<Cart> GetByIdAsync(string userId);
        Task AddPositionAsync(Product product, string userId);
        Task RemovePositionAsync(Product product, string userId);
        /*Task<bool> Clear(string userId);*/
    }
}
