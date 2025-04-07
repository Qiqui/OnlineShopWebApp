using OnlineShop.Domain.Entities;

namespace OnlineShop.Domain.Interfaces
{
    public interface IFavouritesRepository
    {
        Task<Favourites?> GetByIdAsync(string userId);
        Task<Product?> GetProductByIdAsync(Guid Id);
        Task AddAsync(Guid id, string userId);
        Task RemoveAsync(Guid id, string userId);
    }
}
