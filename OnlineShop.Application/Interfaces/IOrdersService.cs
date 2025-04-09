using OnlineShop.Application.DTOs;
using OnlineShop.Domain.Entities;

namespace OnlineShop.Application.Interfaces
{
    public interface IOrdersService
    {
        Task<OrderDTO> CreateAsync(OrderDTO orderDTO);
        Task<List<OrderDTO>> GetAllOrdersDTO();
    }
}
