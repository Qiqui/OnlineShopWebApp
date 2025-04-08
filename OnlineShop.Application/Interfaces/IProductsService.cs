using OnlineShop.Domain.Entities;

namespace OnlineShop.Application.Interfaces
{
    public interface IProductsService
    {
        Task<Product> GetByIdAsync(Guid productId);
    }
}
