using AutoMapper;
using OnlineShop.Application.DTOs;
using OnlineShop.Domain.Entities;

namespace OnlineShop.Application.Profiles
{
    public class CartPositionProfile : Profile
    {
        public CartPositionProfile()
        {
            CreateMap<CartPosition, CartPositionDTO>()
            .ForMember(cartPositionDTO => cartPositionDTO.Cart, opt => opt.Ignore())
            .ReverseMap()
            .ForMember(CartPosition => CartPosition.Cart, opt => opt.Ignore());
        }
    }
}
