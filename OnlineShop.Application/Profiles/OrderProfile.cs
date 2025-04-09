using AutoMapper;
using OnlineShop.Application.DTOs;
using OnlineShop.Domain.Entities;

namespace OnlineShop.Application.Profiles
{
    public class OrderProfile : Profile
    {
        public OrderProfile()
        {
            CreateMap<Order, OrderDTO>()
            .ForMember(orderDTO => orderDTO.Positions, opt => opt.MapFrom(order => order.Positions))
            .ReverseMap()
            .ForMember(order => order.Positions, opt => opt.Ignore());
        }
    }
}
