using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAppWare.Domain.Interfaces;
using WebAppWare.Infrastructure.BaseContext;

namespace WebAppWare.Infrastructure.Repositories;

public class SupplierRepo : ISupplierRepo
{
    private readonly WarehouseBaseContext _warehouseBaseContext;
    public SupplierRepo(WarehouseBaseContext warehouseBaseContext)
    {
        _warehouseBaseContext = warehouseBaseContext;
    }

	public async Task Create(Supplier supplier)
	{
		_warehouseBaseContext.Suppliers.Add(supplier);
		await _warehouseBaseContext.SaveChangesAsync();
	}

	public async Task Delete(Supplier supplier)
	{
		_warehouseBaseContext.Suppliers.Remove(supplier);
		await _warehouseBaseContext.SaveChangesAsync();
	}

	public async Task<List<Supplier>> GetAll()
    {
        return await _warehouseBaseContext.Suppliers.ToListAsync();
    }

	public async Task<Supplier> GetById(int id)
	{
		return await _warehouseBaseContext.Suppliers.FirstOrDefaultAsync(x => x.Id == id);
	}

	public async Task Update(Supplier supplier)
	{
		_warehouseBaseContext.Suppliers.Update(supplier);
        await _warehouseBaseContext.SaveChangesAsync();
	}
}
