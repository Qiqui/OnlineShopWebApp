using AutoMapper;
using OnlineShop.Application.DTOs;

namespace OnlineShopWebApp.Profiles
{
    public class OrderViewModelProfile : Profile
    {
        public OrderViewModelProfile()
        {
            CreateMap<OrderDTO, OrderViewModelProfile>().ReverseMap();
        }
    }
}
