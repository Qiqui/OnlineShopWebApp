using OnlineShop.Domain.Entities;

namespace OnlineShop.Application.Interfaces
{
    public interface IProductService
    {
        Task<Product> GetByIdAsync(Guid productId);
    }
}
