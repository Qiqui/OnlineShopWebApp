using AutoMapper;
using OnlineShop.Application.DTOs;
using OnlineShopWebApp.Models;

namespace OnlineShopWebApp.Profiles
{
    public class OrderViewModelProfile : Profile
    {
        public OrderViewModelProfile()
        {
            CreateMap<OrderDTO, OrderViewModel>()
            .ForMember(orderVM => orderVM.Positions, opt => opt.MapFrom(orderDTO => orderDTO.Positions))
            .ReverseMap();
        }
    }
}
