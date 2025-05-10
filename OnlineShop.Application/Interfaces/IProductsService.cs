using OnlineShop.Application.DTOs;
using OnlineShop.Domain.Entities;

namespace OnlineShop.Application.Interfaces
{
    public interface IProductsService
    {
        Task<Product> GetByIdAsync(Guid productId);
        Task<ProductDTO> GetProductDtoAsync(Guid productId);
        Task<List<ProductDTO>> GetAllAsync();
    }
}
