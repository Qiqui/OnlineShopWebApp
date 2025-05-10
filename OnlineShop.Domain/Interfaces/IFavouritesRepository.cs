using OnlineShop.Domain.Entities;

namespace OnlineShop.Domain.Interfaces
{
    public interface IFavouritesRepository
    {
        Task<Favourites?> GetByIdAsync(string userId);
        Task<Product?> GetProductByIdAsync(Guid Id);
        Task Update(Favourites favourites);
        Task<Favourites> CreateFavouritesAsync(string userId);
    }
}
