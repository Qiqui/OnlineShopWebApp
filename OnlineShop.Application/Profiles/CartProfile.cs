using AutoMapper;
using OnlineShop.Application.DTOs;
using OnlineShop.Domain.Entities;

namespace OnlineShop.Application.Profiles
{
    public class CartProfile : Profile
    {
        public CartProfile()
        {
            CreateMap<Cart, CartDTO>()
            .ForMember(cartDTO => cartDTO.Positions, opt => opt.MapFrom(cart => cart.Positions))
            .ReverseMap()
            .ForMember(cart => cart.Positions, opt => opt.Ignore());
        }
    }
}
