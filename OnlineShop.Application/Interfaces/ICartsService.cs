using OnlineShop.Application.DTOs;
using OnlineShop.Domain.Entities;

namespace OnlineShop.Application.Interfaces
{
    public interface ICartsService
    {
        Task<CartDTO> GetCartDtoAsync(string userName);
        Task<CartDTO> AddPositionAsync(Guid productId, string userName);
        Task<CartDTO> RemovePositionAsync(Guid productId, string userName);
        Task ClearAsync(string userName);
    }
}
