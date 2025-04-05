using OnlineShop.Domain.Entities;

namespace OnlineShop.Domain.Interfaces
{
    public interface IFavouritesRepository
    {
        Favourites TryGetById(string userId);
        Product TryGetProductById(Guid Id);
        void Add(Guid id, string userId);
        void Remove(Guid id, string userId);
    }
}
