using OnlineShop.Domain.Entities;

namespace OnlineShop.Domain.Interfaces
{
    public interface IProductsRepository
    {
        void Add(Product product);
        bool TryToRemove(Guid id);
        void Update(Guid id, Product product);
        List<Product> GetAll();
        Product TryGetById(Guid id);
        List<Product> Search(string keyWords);
    }
}
