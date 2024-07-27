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
	private readonly WarehouseBaseContext _dbContext;
	private readonly IProductFlowRepo _productFlowRepo;
	private readonly IProductRepo _productRepo;

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

	public WarehouseRepo(WarehouseBaseContext dbContext, 
							IProductFlowRepo productFlowRepo,
							IProductRepo productRepo)
    {
        _dbContext = dbContext;
		_productFlowRepo = productFlowRepo;
		_productRepo = productRepo;
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

	public async Task<IEnumerable<WarehouseModel>> GetAll()
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

	public async Task<List<ProductFlowModel>> GetProductsAmount()
	{
		var allRecords = await _productFlowRepo.GetAll();
		var allProducts = await _productRepo.GetAll();
		var allWarehouses = await GetAll();

		int sum = 0;
		var singleRecord = await _productFlowRepo.GetAll();
		List<ProductFlowModel> productsAmount = new List<ProductFlowModel>();

		foreach (var item in allProducts)
		{
			foreach (var ware in allWarehouses)
			{
				var currentRecord = singleRecord.Where(x => x.Warehouse.Name == ware.Name)
											.Where(x => x.ProductItemCode == item.ItemCode)
											.ToList();

				sum = currentRecord.Sum(x => x.Quantity);

				var newItem = new ProductFlowModel()
				{
					ProductItemCode = item.ItemCode,
					WarehouseName = ware.Name,
					Cumulative = sum
				};

				productsAmount.Add(newItem);
			}
		}

		return productsAmount;
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
