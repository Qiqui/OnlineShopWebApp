using AutoMapper;
using OnlineShop.Application.DTOs;
using OnlineShop.Domain.Entities;

namespace OnlineShop.Application.Profiles
{
    public class ComparisonProfile : Profile
    {
        public ComparisonProfile()
        {
            CreateMap<Comparison, ComparisonDTO>().ReverseMap();
        }
    }
}
