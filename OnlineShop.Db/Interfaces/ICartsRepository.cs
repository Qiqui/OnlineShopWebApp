using OnlineShop.Db.Models;


namespace OnlineShop.Db.Interfaces
{
    public interface ICartsRepository
    {
        Cart TryGetById(string userId);

        void Add(User user);

        void Add(Product product, User user);

        void Remove(Product product, User user);

        bool Clear(string userId);
    }
}
