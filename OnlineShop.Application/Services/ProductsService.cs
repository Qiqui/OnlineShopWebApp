using OnlineShop.Application.Interfaces;
using OnlineShop.Domain.Entities;
using OnlineShop.Domain.Exceptions;
using OnlineShop.Domain.Interfaces;

namespace OnlineShop.Application.Services
{
    public class ProductsService : IProductsService
    {
        private readonly IProductsRepository _productsRepositoty;

        public ProductsService(IProductsRepository productsRepositoty)
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
