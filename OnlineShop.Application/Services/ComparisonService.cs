using AutoMapper;
using OnlineShop.Application.DTOs;
using OnlineShop.Application.Interfaces;
using OnlineShop.Domain.Entities;
using OnlineShop.Domain.Exceptions;
using OnlineShop.Domain.Interfaces;

namespace OnlineShop.Application.Services
{
    public class ComparisonService : IComparisonService
    {
        private readonly IProductsService _productsService;
        private readonly IComparisonRepository _comparisonRepositoty;
        private readonly IUsersService _usersService;
        private readonly IMapper _mapper;

        public ComparisonService(IProductsService productsService, IComparisonRepository comparesRepositoty, IUsersService usersService, IMapper mapper)
        {
            _productsService = productsService;
            _comparisonRepositoty = comparesRepositoty;
            _usersService = usersService;
            _mapper = mapper;
        }

        private async Task<Comparison> GetByNameAsync(string userName)
        {
            try
            {
                var userId = await _usersService.GetCurrentUserIdAsync(userName);
                var comparison = await _comparisonRepositoty.GetByIdAsync(userId);

                return comparison;
            }

            catch (NotFoundException ex)
            {
                throw new NotFoundException(ex.Message);
            }

        }

        public async Task<ComparisonDTO> AddProductAsync(Guid productId, string userName)
        {
            try
            {
                var comparison = await GetByNameAsync(userName);

                if (comparison != null)
                {
                    var product = await _productsService.GetByIdAsync(productId);

                    if (comparison.Products.Contains(product))
                        return GetComparisonDTO(comparison); ;

                    if (comparison.Products.Count > 1)
                    {
                        var lastProduct = comparison.Products.Last();
                        comparison.Products.Remove(lastProduct);
                        comparison.Products.Add(product);
                        await _comparisonRepositoty.UpdateAsync(comparison);

                        return GetComparisonDTO(comparison);
                    }

                    comparison.Products.Add(product);

                    await _comparisonRepositoty.UpdateAsync(comparison);

                    return GetComparisonDTO(comparison);
                }

                else
                {
                    var userId = await _usersService.GetCurrentUserIdAsync(userName);
                    var product = await _productsService.GetByIdAsync(productId);
                    comparison = await _comparisonRepositoty.CreateComparisonAsync(userId);
                    comparison.Products.Add(product);

                    await _comparisonRepositoty.UpdateAsync(comparison);

                    return GetComparisonDTO(comparison);

                }
            }

            catch (NotFoundException ex)
            {
                throw new NotFoundException(ex.Message);
            }
        }

        public async Task<ComparisonDTO> RemoveProductAsync(Guid productId, string userName)
        {
            try
            {
                var comparison = await GetByNameAsync(userName);
                var product = await _productsService.GetByIdAsync(productId);
                comparison.Products.Remove(product);

                await _comparisonRepositoty.UpdateAsync(comparison);

                return GetComparisonDTO(comparison);
            }

            catch (NotFoundException ex)
            {
                throw new NotFoundException(ex.Message);
            }
        }

        public async Task<ComparisonDTO> GetComparisonDtoAsync(string userName)
        {
            try
            {
                var comparison = await GetByNameAsync(userName);
                var comparisonDTO = GetComparisonDTO(comparison);

                return comparisonDTO;
            }

            catch (NotFoundException ex)
            {
                throw new NotFoundException(ex.Message);
            }
        }

        private ComparisonDTO GetComparisonDTO(Comparison comparison)
        {
            var comparisonDTO = _mapper.Map<ComparisonDTO>(comparison);

            return comparisonDTO;
        }
    }
}
