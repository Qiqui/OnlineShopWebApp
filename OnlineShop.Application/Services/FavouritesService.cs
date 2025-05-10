using AutoMapper;
using OnlineShop.Application.DTOs;
using OnlineShop.Application.Interfaces;
using OnlineShop.Domain.Entities;
using OnlineShop.Domain.Exceptions;
using OnlineShop.Domain.Interfaces;

namespace OnlineShop.Application.Services
{

    public class FavouritesService : IFavouritesService
    {
        private readonly IProductsService _productsService;
        private readonly IFavouritesRepository _favouritesRepository;
        private readonly IUsersService _usersService;
        private readonly IMapper _mapper;

        public FavouritesService(IProductsService productsService, IFavouritesRepository favouritesRepository, IUsersService usersService, IMapper mapper)
        {
            _productsService = productsService;
            _favouritesRepository = favouritesRepository;
            _usersService = usersService;
            _mapper = mapper;
        }

        private async Task<Favourites> GetByNameAsync(string userName)
        {
            try
            {
                var userId = await _usersService.GetCurrentUserIdAsync(userName);
                var favourites = await _favouritesRepository.GetByIdAsync(userId);

                return favourites;
            }

            catch (NotFoundException ex)
            {
                throw new NotFoundException(ex.Message);
            }
        }

        public async Task<FavouritesDTO> GetFavouritesDtoAsync(string userName)
        {
            try
            {
                var favourites = await GetByNameAsync(userName);
                var favouritesDTO = GetFavouritesDTO(favourites);

                return favouritesDTO;
            }

            catch (NotFoundException ex)
            {
                throw new NotFoundException(ex.Message);
            }

        }

        public async Task<FavouritesDTO> AddProductAsync(Guid productId, string userName)
        {
            try
            {
                var userId = await _usersService.GetCurrentUserIdAsync(userName);
                var favorites = await GetByNameAsync(userName);

                if (favorites != null)
                {
                    var product = await _productsService.GetByIdAsync(productId);
                    if (product != null && !favorites.Products.Contains(product))
                        favorites.Products.Add(product);
                }
                else
                {
                    favorites = await _favouritesRepository.CreateFavouritesAsync(userId);
                    var product = await _productsService.GetByIdAsync(productId);
                    favorites.Products.Add(product);
                }

                await _favouritesRepository.Update(favorites);

                var favouritesDTO = GetFavouritesDTO(favorites);

                return favouritesDTO;
            }

            catch (NotFoundException ex)
            {
                throw new NotFoundException(ex.Message);
            }
        }

        public async Task<FavouritesDTO> RemoveProductAsync(Guid productId, string userName)
        {
            try
            {

                var userId = await _usersService.GetCurrentUserIdAsync(userName);
                var favorites = await GetByNameAsync(userName);

                if (favorites != null)
                {
                    var product = await _productsService.GetByIdAsync(productId);
                    favorites.Products.Remove(product);
                }

                await _favouritesRepository.Update(favorites);

                var favouritesDTO = GetFavouritesDTO(favorites);

                return favouritesDTO;
            }

            catch (NotFoundException ex)
            {
                throw new NotFoundException(ex.Message);
            }
        }

        private FavouritesDTO GetFavouritesDTO(Favourites favourites)
        {
            var favouritesDTO = _mapper.Map<FavouritesDTO>(favourites);

            return favouritesDTO;
        }
    }
}
