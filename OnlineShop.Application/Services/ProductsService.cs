using AutoMapper;
using OnlineShop.Application.DTOs;
using OnlineShop.Application.Interfaces;
using OnlineShop.Domain.Entities;
using OnlineShop.Domain.Exceptions;
using OnlineShop.Domain.Interfaces;

namespace OnlineShop.Application.Services
{
    public class ProductsService : IProductsService
    {
        private readonly IProductsRepository _productsRepositoty;
        private readonly IMapper _mapper;

        public ProductsService(IProductsRepository productsRepositoty, IMapper mapper)
        {
            _productsRepositoty = productsRepositoty;
            _mapper = mapper;
        }

        public async Task<List<ProductDTO>> GetAllAsync()
        {
            var products = await _productsRepositoty.GetAllAsync();
            var productsDTO = GetProductsDTO(products);

            return productsDTO;
        }

        public async Task<Product> GetByIdAsync(Guid productId)
        {
            var product = await _productsRepositoty.GetByIdAsync(productId);

            return product ?? throw new NotFoundException($"Произошла ошибка. Товар не найден");
        }

        public async Task<ProductDTO> GetProductDtoAsync(Guid productId)
        {
            try
            {
                var product = await GetByIdAsync(productId);
                var productDTO = GetProductDTO(product);

                return productDTO;
            }

            catch (NotFoundException ex)
            {
                throw new NotFoundException(ex.Message);
            }

        }

        private ProductDTO GetProductDTO(Product product)
        {
            var productDTO = _mapper.Map<ProductDTO>(product);

            return productDTO;
        }

        private List<ProductDTO> GetProductsDTO(List<Product> products)
        {
            var productsDTO = _mapper.Map<List<ProductDTO>>(products);

            return productsDTO;
        }
    }
}
