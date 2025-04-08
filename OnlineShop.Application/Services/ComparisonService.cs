using AutoMapper;
using OnlineShop.Application.DTOs;
using OnlineShop.Application.Interfaces;
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

        public async Task<ComparisonDTO> GetByNameAsync(string userName)
        {
            try
            {
                var userId = await _usersService.GetCurrentUserIdAsync(userName);
                var comparison = await _comparisonRepositoty.GetByIdAsync(userId);


            }
        }
    }
}
