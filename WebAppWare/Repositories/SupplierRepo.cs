using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using WebAppWare.Database;
using WebAppWare.Database.Entities;
using WebAppWare.Models;
using WebAppWare.Repositories.Interfaces;

namespace WebAppWare.Repositories;

public class SupplierRepo : ISupplierRepo
{
    private readonly WarehouseBaseContext _dbContext;

	private Expression<Func<SupplierModel, Supplier>> MapToEntity = x => new Supplier()
	{
		Id = x.Id,
		Name = x.Name,
		Email = x.Email
	};

	private Expression<Func<Supplier, SupplierModel>> MapToModel = x => new SupplierModel()
	{
		Id = x.Id,
		Name = x.Name,
		Email = x.Email
	};
    public SupplierRepo(WarehouseBaseContext warehouseBaseContext)
    {
        _dbContext = warehouseBaseContext;
    }

	public async Task Create(SupplierModel model)
	{
		var entity = MapToEntity.Compile().Invoke(model);
		_dbContext.Suppliers.Add(entity);
		await _dbContext.SaveChangesAsync();
	}

	public async Task Delete(SupplierModel model)
	{
		if (model == null)
		{
			throw new NullReferenceException($"Item under {model} is not exist...");
		}

		var entity = MapToEntity.Compile().Invoke(model);
		_dbContext.Suppliers.Remove(entity);
		await _dbContext.SaveChangesAsync();
	}

	public async Task<IEnumerable<SupplierModel>> GetAll()
    {
		var suppliers = await _dbContext.Suppliers.Select(MapToModel).ToListAsync();
		return suppliers;
	}

	public async Task<SupplierModel> GetById(int id)
	{
		var supplier = await _dbContext.Suppliers.Select(MapToModel).FirstOrDefaultAsync(x => x.Id == id);

		if (supplier == null)
		{
			throw new NullReferenceException($"The item under id {id} is not exsit...");
		}
		
		return supplier;	
	}

	public async Task<int> GetSupplierIdByName(string supplierName)
	{
		var supplier = await _dbContext.Suppliers.FirstOrDefaultAsync(x => x.Name == supplierName);

		if (supplier == null)
		{
			return 0;
		}

		int supplierId = supplier.Id;
		return supplierId;
	}

	public async Task Update(SupplierModel model)
	{
		var entity = MapToEntity.Compile().Invoke(model);
		_dbContext.Suppliers.Update(entity);
		await _dbContext.SaveChangesAsync();
	}
}
