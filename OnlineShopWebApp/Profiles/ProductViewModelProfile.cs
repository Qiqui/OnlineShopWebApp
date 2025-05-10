using AutoMapper;
using OnlineShop.Application.DTOs;
using OnlineShopWebApp.Models;

namespace OnlineShopWebApp.Profiles
{
    public class ProductViewModelProfile : Profile
    {
        public ProductViewModelProfile()
        {
            CreateMap<ProductDTO, ProductViewModel>()
            .ForMember(productVM => productVM.ImagePaths, opt => opt.MapFrom(productDTO => productDTO.ImagePaths))
            .ForMember(productVM => productVM.Gender, opt => opt.MapFrom(productDTO => productDTO.Gender))
            .ForMember(productVM => productVM.Color, opt => opt.MapFrom(productDTO => productDTO.Color))
            .ForMember(productVM => productVM.Collection, opt => opt.MapFrom(productDTO => productDTO.Collection))
            .ForMember(productVM => productVM.Material, opt => opt.MapFrom(productDTO => productDTO.Material))
            .ForMember(productVM => productVM.Brand, opt => opt.MapFrom(productDTO => productDTO.Brand))
            .ForMember(productVM => productVM.Category, opt => opt.MapFrom(productDTO => productDTO.Category))
            .ReverseMap();
        }
    }
}
