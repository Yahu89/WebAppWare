using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAppWare.Domain.Interfaces;
using WebAppWare.Infrastructure.BaseContext;

namespace WebAppWare.Infrastructure.Repositories;

public class ComboRepo : IComboRepo
{
	private readonly WarehouseBaseContext _warehouseBaseContext;
	public ComboRepo(WarehouseBaseContext warehouseBaseContext)
    {
		_warehouseBaseContext = warehouseBaseContext;
    }

	public async Task CreateMovement(WarehouseMovement warehouseMovement)
	{
		_warehouseBaseContext.WarehouseMovements.Add(warehouseMovement);
		await _warehouseBaseContext.SaveChangesAsync();
	}

	public async Task CreateProductFlow(ProductsFlow productsFlow)
	{
		_warehouseBaseContext.ProductsFlows.Add(productsFlow);
		await _warehouseBaseContext.SaveChangesAsync();
	}

	public async Task CreateProductFlowList(List<ProductsFlow> productsFlowList)
	{
		_warehouseBaseContext.ProductsFlows.AddRange(productsFlowList);
		await _warehouseBaseContext.SaveChangesAsync();
	}

	public async Task<List<ProductFlowModel>> GetAllProductFlowModel(int prodId, string warehouse)
	{
		List<ProductFlowModel> list = await _warehouseBaseContext.ProductsFlows.Include(x => x.Warehouse)
											.Include(x => x.Product)
											.Include(x => x.Supplier)
											.Include(x => x.WarehouseMovement)
											.Select(x => new ProductFlowModel()
											{
												Id = x.Id,
												MoveId = x.WarehouseMovement.Id,
												MovementType = x.WarehouseMovement.MovementType,
												Warehouse = x.Warehouse.Name,
												ItemCode = x.Product.ItemCode,
												Quantity = x.Quantity,
												Supplier = x.Supplier.Name,
												CreationDate = x.WarehouseMovement.CreationDate
											})
											.Where(x => x.Pro == prodId)
											.Where(x => x.Warehouse == warehouse)
											.OrderBy(x => x.CreationDate)
											.ToListAsync();

		for (int i = 0; i < list.Count(); i++)
		{
			if (i == 0)
			{
				list[i].Cumulative = list[i].Quantity;
			}
			else
			{
				list[i].Cumulative = list[i - 1].Cumulative + list[i].Quantity;
			}
		}

		return list;
	}

	public async Task<List<Product>> GetAllProducts()
	{
		return await _warehouseBaseContext.Products.OrderBy(x => x.ItemCode).ToListAsync();
	}

	public async Task<List<Supplier>> GetAllSuppliers()
	{
		return await _warehouseBaseContext.Suppliers.OrderBy(x => x.Name).ToListAsync();
	}

	public async Task<List<Warehouse>> GetAllWarehouses()
	{
		return await _warehouseBaseContext.Warehouses.OrderBy(x => x.Name).ToListAsync();
	}

	public async Task<WarehouseMovement> GetLastMovement()
	{
		return await _warehouseBaseContext.WarehouseMovements.OrderBy(x => x.CreationDate).LastAsync();
	}
}
