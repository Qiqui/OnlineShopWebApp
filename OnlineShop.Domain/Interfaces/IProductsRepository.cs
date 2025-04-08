using OnlineShop.Domain.Entities;

namespace OnlineShop.Domain.Interfaces
{
    public interface IProductsRepository
    {
        Task CreateAsync(Product product);
        Task<bool> RemoveAsync(Guid id);
        Task UpdateAsync(Guid id, Product product);
        Task<List<Product>> GetAllAsync();
        Task<Product?> GetByIdAsync(Guid id);
        Task<List<Product>> SearchAsync(string keyWords);
    }
}
