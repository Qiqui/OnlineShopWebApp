using OnlineShop.Db.Models;

namespace OnlineShop.Db.Interfaces
{
    public interface ICompareRepository
    {
        Compare TryGetById(string userId);
        Product TryGetProductById(Guid Id);
        void Add(Guid id, string userId);
        void Remove(Guid id, string userId);
    }
}
