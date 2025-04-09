using AutoMapper;
using OnlineShop.Application.DTOs;
using OnlineShopWebApp.Models;

namespace OnlineShopWebApp.Profiles
{
    public class CartViewModelProfile : Profile
    {
        public CartViewModelProfile()
        {
            CreateMap<CartDTO, CartViewModel>().ReverseMap();
        }
    }
}
