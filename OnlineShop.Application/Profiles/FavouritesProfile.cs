using AutoMapper;
using OnlineShop.Application.DTOs;
using OnlineShop.Domain.Entities;

namespace OnlineShop.Application.Profiles
{
    public class FavouritesProfile : Profile
    {
        public FavouritesProfile()
        {
            CreateMap<Favourites, FavouritesDTO>().ReverseMap();
        }
    }
}
