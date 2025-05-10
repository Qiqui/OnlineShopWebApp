using AutoMapper;
using OnlineShop.Application.DTOs;
using OnlineShopWebApp.Models;

namespace OnlineShopWebApp.Profiles
{
    public class ComparisonViewModelProfile : Profile
    {
        public ComparisonViewModelProfile()
        {
            CreateMap<ComparisonDTO, ComparisonViewModel>().ReverseMap();
        }
    }
}
