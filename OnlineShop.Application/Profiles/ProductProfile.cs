using AutoMapper;
using OnlineShop.Application.DTOs;
using OnlineShop.Domain.Entities;

namespace OnlineShop.Application.Profiles
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<Product, ProductDTO>()
                .ForMember(productDTO => productDTO.CartPositions, opt => opt.Ignore())
                .ForMember(productDTO => productDTO.Compares, opt => opt.Ignore())
                .ForMember(productDTO => productDTO.Favourites, opt => opt.Ignore())
                .ReverseMap()
                .ForMember(product => product.CartPositions, opt => opt.Ignore())
                .ForMember(product => product.Compares, opt => opt.Ignore())
                .ForMember(product => product.Favourites, opt => opt.Ignore());
        }
    }
}
