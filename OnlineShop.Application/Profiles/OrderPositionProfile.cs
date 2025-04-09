using AutoMapper;
using OnlineShop.Application.DTOs;
using OnlineShop.Domain.Entities;

namespace OnlineShop.Application.Profiles
{
    public class OrderPositionProfile : Profile
    {
        public OrderPositionProfile()
        {
            CreateMap<OrderPosition, OrderPositionDTO>()
            .ForMember(orderPositionDTO => orderPositionDTO.Order, opt => opt.Ignore())
            .ReverseMap();
        }
    }
}
