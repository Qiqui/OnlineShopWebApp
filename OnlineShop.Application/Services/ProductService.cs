using OnlineShop.Application.Interfaces;
using OnlineShop.Domain.Entities;
using OnlineShop.Domain.Exceptions;
using OnlineShop.Domain.Interfaces;

namespace OnlineShop.Application.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductsRepository _productsRepositoty;

        public ProductService(IProductsRepository productsRepositoty)
        {
            _productsRepositoty = productsRepositoty;
        }

        public async Task<Product> GetByIdAsync(Guid productId)
        {
            var product = await _productsRepositoty.GetByIdAsync(productId);

            return product ?? throw new NotFoundException($"Произошла ошибка. Товар не найден");
        }
    }
}
