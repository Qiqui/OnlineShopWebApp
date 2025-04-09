using AutoMapper;
using OnlineShop.Application.DTOs;

namespace OnlineShopWebApp.Profiles
{
    public class ContactInfoViewModelProfile : Profile
    {
        public ContactInfoViewModelProfile()
        {
            CreateMap<ContactInfoDTO, ContactInfoViewModelProfile>().ReverseMap();
        }
    }
}
