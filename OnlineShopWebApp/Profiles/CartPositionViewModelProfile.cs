using AutoMapper;
using OnlineShop.Application.DTOs;
using OnlineShopWebApp.Models;

namespace OnlineShopWebApp.Profiles
{
    public class CartPositionViewModelProfile : Profile
    {
        public CartPositionViewModelProfile()
        {
            CreateMap<CartPositionDTO, CartPositionViewModel>().ReverseMap();
        }
    }
}
