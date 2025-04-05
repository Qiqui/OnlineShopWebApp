using OnlineShop.Domain.Entities;

namespace OnlineShop.Domain.Interfaces
{
    public interface IProductsRepository
    {
        Task Add(Product product);
        Task<bool> TryToRemove(Guid id);
        Task Update(Guid id, Product product);
        Task<List<Product>> GetAll();
        Task<Product?> TryGetById(Guid id);
        Task<List<Product>> Search(string keyWords);
    }
}
