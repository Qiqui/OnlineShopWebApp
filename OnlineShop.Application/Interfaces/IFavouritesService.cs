using OnlineShop.Application.DTOs;

namespace OnlineShop.Application.Interfaces
{
    public interface IFavouritesService
    {
        Task<FavouritesDTO> GetFavouritesDtoAsync(string userName);
        Task<FavouritesDTO> AddProductAsync(Guid productId, string userName);
        Task<FavouritesDTO> RemoveProductAsync(Guid productId, string userName);
    }
}
