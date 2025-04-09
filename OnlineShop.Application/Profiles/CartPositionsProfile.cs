using AutoMapper;
using OnlineShop.Application.DTOs;
using OnlineShop.Domain.Entities;

namespace OnlineShop.Application.Profiles
{
    public class CartPositionsProfile : Profile
    {
        public CartPositionsProfile()
        {
            CreateMap<CartPosition, CartPositionDTO>().ReverseMap();
        }
    }
}
