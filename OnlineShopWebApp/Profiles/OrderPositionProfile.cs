using AutoMapper;
using OnlineShop.Application.DTOs;
using OnlineShopWebApp.Models;

namespace OnlineShopWebApp.Profiles
{
    public class OrderPositionProfile : Profile
    {
        public OrderPositionProfile()
        {
            CreateMap<OrderPositionDTO, OrderPositionViewModel>().ReverseMap();
        }
    }
}
