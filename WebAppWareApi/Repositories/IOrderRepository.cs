using WebAppWare.Api.Dto;
using WebAppWare.Database.Entities;

namespace WebAppWare.Api.Repositories;

public interface IOrderRepository
{
    Task<List<OrderDto>> GetAll();
    Task<Order> GetById(int id);
    Task Create(OrderCreateDto orderDto);
    Task Delete(Order order);
}
