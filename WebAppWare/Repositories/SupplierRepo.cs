using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAppWare.Database;
using WebAppWare.Models;
using WebAppWare.Repositories.Interfaces;

namespace WebAppWare.Repositories;

public class SupplierRepo : ISupplierRepo
{
    private readonly WarehouseDbContext _dbContext;
    public SupplierRepo(WarehouseDbContext warehouseBaseContext)
    {
        _dbContext = warehouseBaseContext;
    }

	public async Task Create(SupplierModel supplier)
	{
		//_dbContext.Suppliers.Add(supplier);
		//await _dbContext.SaveChangesAsync();
	}

	public async Task Delete(SupplierModel supplier)
	{
		//_dbContext.Suppliers.Remove(supplier);
		//await _dbContext.SaveChangesAsync();
	}

	public async Task<List<SupplierModel>> GetAll()
    {
		return new List<SupplierModel>();
        //return await _dbContext.Suppliers.ToListAsync();
    }

	public async Task<SupplierModel> GetById(int id)
	{
		//return await _dbContext.Suppliers.FirstOrDefaultAsync(x => x.Id == id);
		return new SupplierModel();
	}

	public async Task Update(SupplierModel supplier)
	{
		//_dbContext.Suppliers.Update(supplier);
  //      await _dbContext.SaveChangesAsync();
	}
}
