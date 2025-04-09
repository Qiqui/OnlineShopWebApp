using AutoMapper;
using OnlineShop.Application.DTOs;
using OnlineShopWebApp.Models;

namespace OnlineShopWebApp.Profiles
{
    public class FavouritesViewModelProfile : Profile
    {
        public FavouritesViewModelProfile()
        {
            CreateMap<FavouritesDTO, FavouritesViewModel>().ReverseMap();
        }
    }
}
