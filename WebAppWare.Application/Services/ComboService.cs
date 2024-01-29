using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAppWare.Domain.Interfaces;
using WebAppWare.Infrastructure;

namespace WebAppWare.Application.Services;

public class ComboService : IComboService
{
	private readonly IComboRepo _comboRepo;
	public ComboService(IComboRepo comboRepo)
    {
		_comboRepo = comboRepo;
    }

	public async Task CreateMovement(WarehouseMovement warehouseMovement)
	{
		await _comboRepo.CreateMovement(warehouseMovement);
	}

	public async Task CreateProductFlow(ProductsFlow productsFlow)
	{
		await _comboRepo.CreateProductFlow(productsFlow);
	}

	public async Task CreateProductFlowList(List<ProductsFlow> productsFlow)
	{
		await _comboRepo.CreateProductFlowList(productsFlow);
	}

	public async Task<List<ProductFlowModel>> GetAllProductFlowModel(string itemCode, string warehouse)
	{
		return await _comboRepo.GetAllProductFlowModel(itemCode, warehouse);
	}

	public async Task<List<Product>> GetAllProducts()
	{
		return await _comboRepo.GetAllProducts();
	}

	public async Task<List<Supplier>> GetAllSuppliers()
	{
		return await _comboRepo.GetAllSuppliers();
	}

	public async Task<List<Warehouse>> GetAllWarehouses()
	{
		return await _comboRepo.GetAllWarehouses();
	}

	public async Task<WarehouseMovement> GetLastMovement()
	{
		return await _comboRepo.GetLastMovement();
	}
}
