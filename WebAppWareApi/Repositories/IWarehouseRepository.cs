using WebAppWare.Api.Dto;
using WebAppWare.Database.Entities;

namespace WebAppWare.Api.Repositories;

public interface IWarehouseRepository
{
    Task Create(WarehouseDto dto);
    Task<IEnumerable<Warehouse>> GetAll();
    Task<Warehouse> GetById(int id);
    Task Edit(int id, WarehouseDto dto);
    Task Delete(int id);
}
