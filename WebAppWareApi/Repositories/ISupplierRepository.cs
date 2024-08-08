using WebAppWare.Database.Entities;

namespace WebAppWare.Api.Repositories;

public interface ISupplierRepository
{
    Task<IEnumerable<Supplier>> GetAll();
    Task<Supplier> GetById(int id);
    Task Create(Supplier supplier);
    Task Edit(Supplier supplier);
    Task Delete(int id);
}
