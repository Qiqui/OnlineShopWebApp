using AutoMapper;
using OnlineShop.Application.DTOs;
using OnlineShopWebApp.Models;

namespace OnlineShopWebApp.Profiles
{
    public class OrderPositionViewModelProfile : Profile
    {
        public OrderPositionViewModelProfile()
        {
            CreateMap<OrderPositionDTO, OrderPositionViewModel>()
                .ForMember(orderPositionVM => orderPositionVM.Order, opt => opt.MapFrom(orderPositionDTO => orderPositionDTO.Order))
                .ReverseMap()
                .ForMember(orderPositionDTO => orderPositionDTO.Order, opt => opt.Ignore());
        }
    }
}
