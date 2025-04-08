using OnlineShop.Application.DTOs;
using OnlineShop.Domain.Entities;

namespace OnlineShop.Application.Interfaces
{
    public interface ICartsService
    {
        Task<Cart> GetByIdAsync(string userId);
        CartDTO GetCartDTO(Cart cart);
        Task<CartDTO> AddPositionAsync(Guid productId, string userName);
        Task IncreasePosition(Cart cart, Guid productId);
        Task<CartDTO> RemovePositionAsync(Guid productId, string userName);
        Task DecreasePositionAsync(Cart cart, Guid productId);
        Task ClearAsync(string userName);
    }
}
