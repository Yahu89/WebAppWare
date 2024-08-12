using Microsoft.EntityFrameworkCore;
using WebAppWare.Api.Middleware;
using WebAppWare.Database;
using WebAppWare.Database.Entities;

namespace WebAppWare.Api.Repositories;

public class SupplierRepository : ISupplierRepository
{
    private readonly WarehouseBaseContext _dbContext;
    public SupplierRepository(WarehouseBaseContext dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task<IEnumerable<Supplier>> GetAll()
    {
        var entities = await _dbContext.Suppliers.ToListAsync();

        if (!entities.Any())
        {
            throw new NoContentException();
        }

        return entities;
    }

    public async Task<Supplier> GetById(int id)
    {
        var entity = await _dbContext.Suppliers.FirstOrDefaultAsync(x => x.Id == id);

        if (entity is null)
        {
            throw new NoContentException();
        }

        return entity;
    }

    public async Task Create(Supplier dto)
    {
        _dbContext.Suppliers.Add(dto);
        await _dbContext.SaveChangesAsync();
    }

    public async Task Edit(Supplier dto)
    {
        _dbContext.Suppliers.Update(dto);
        await _dbContext.SaveChangesAsync();
    }

    public async Task Delete(int id)
    {
        var entity = await GetById(id);

        if (entity is null)
        {
            throw new NoContentException();
        }

        _dbContext.Suppliers.Remove(entity);
        await _dbContext.SaveChangesAsync();
    }
}
