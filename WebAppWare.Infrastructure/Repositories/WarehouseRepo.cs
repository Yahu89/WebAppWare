using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAppWare.Domain.Interfaces;
using WebAppWare.Infrastructure.BaseContext;

namespace WebAppWare.Infrastructure.Repositories;

public class WarehouseRepo : IWarehouseRepo
{
	private readonly WarehouseBaseContext _warehouseBaseContext;
	public WarehouseRepo(WarehouseBaseContext warehouseBaseContext)
    {
        _warehouseBaseContext = warehouseBaseContext;
    }

	public async Task Add(Warehouse warehouse)
	{
		_warehouseBaseContext.Warehouses.Add(warehouse);
		await _warehouseBaseContext.SaveChangesAsync();
	}

	public async Task Delete(Warehouse warehouse)
	{
		_warehouseBaseContext.Warehouses.Remove(warehouse);
		await _warehouseBaseContext.SaveChangesAsync();
	}

	public List<Warehouse> GetAll()
	{
		return _warehouseBaseContext.Warehouses.ToList();
	}

	public async Task<Warehouse> GetById(int id)
	{
		var warehouse = await _warehouseBaseContext.Warehouses.FirstOrDefaultAsync(x => x.Id == id);
		return warehouse;
	}

	public async Task Update(Warehouse warehouse)
	{
		_warehouseBaseContext.Warehouses.Update(warehouse);
		await _warehouseBaseContext.SaveChangesAsync();
	}
}
