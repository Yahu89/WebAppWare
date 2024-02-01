using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using WebAppWare.Database;
using WebAppWare.Database.Entities;
using WebAppWare.Models;
using WebAppWare.Repositories.Interfaces;

namespace WebAppWare.Repositories;

public class WarehouseRepo : IWarehouseRepo
{
	private readonly WarehouseDbContext _dbContext;

	private Expression<Func<Warehouse, WarehouseModel>> MapToModel = x => new WarehouseModel()
	{
		Id = x.Id,
		Name = x.Name,
		IsActive = x.IsActive
	};

	private Expression<Func<WarehouseModel, Warehouse>> MapToEntity = x => new Warehouse()
	{
		Id = x.Id,
		Name = x.Name,
		IsActive = x.IsActive
	};

	private Expression<Func<ProductSummaryModel, ProductsAmountModel>> ProductAmountMapToModel = x => new ProductsAmountModel()
	{
		ItemCode = x.ItemCode,
		Warehouse = x.Warehouse,
		TotalAmount = (int)x.TotalAmount
	};

	public WarehouseRepo(WarehouseDbContext dbContext)
    {
        _dbContext = dbContext;
    }

	public async Task Add(WarehouseModel model)
	{
		var entity = MapToEntity.Compile().Invoke(model);
		_dbContext.Warehouses.Add(entity);
		await _dbContext.SaveChangesAsync();
	}

	public async Task Delete(WarehouseModel model)
	{
		var entity = MapToEntity.Compile().Invoke(model);
		_dbContext.Warehouses.Remove(entity);
		await _dbContext.SaveChangesAsync();
	}

	public async Task<List<WarehouseModel>> GetAll()
	{
		var entities = await _dbContext.Warehouses.Select(MapToModel).ToListAsync();
		return entities;
	}

	public async Task<WarehouseModel> GetById(int id)
	{
		var warehouse = await _dbContext.Warehouses.Select(MapToModel).FirstOrDefaultAsync(x => x.Id == id);

		if (warehouse == null)
		{
			throw new Exception($"No item under defined id: {id}");
		}

		return warehouse;
	}

	public async Task<List<ProductsAmountModel>> GetProductsAmount()
	{
		var result = await _dbContext.ProductSummaryModels.Select(ProductAmountMapToModel).ToListAsync();
		return result;
	}

	public async Task<int> GetWarehouseIdByName(string name)
	{
		var warehouse = await _dbContext.Warehouses.FirstOrDefaultAsync(x => x.Name == name);
		int warehouseId = warehouse.Id;
		return warehouseId;
	}

	public async Task Update(WarehouseModel model)
	{
		var entity = MapToEntity.Compile().Invoke(model);
		_dbContext.Warehouses.Update(entity);
		await _dbContext.SaveChangesAsync();
	}
}
