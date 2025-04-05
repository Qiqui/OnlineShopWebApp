using OnlineShop.Domain.Entities;

namespace OnlineShop.Domain.Interfaces
{
    public interface IProductsRepository
    {
        Task Add(Product product);
        Task<bool> Remove(Guid id);
        Task Update(Guid id, Product product);
        Task<List<Product>> GetAll();
        Task<Product?> GetById(Guid id);
        Task<List<Product>> Search(string keyWords);
    }
}
