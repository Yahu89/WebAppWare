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

public class WarehouseRepo : IWarehouseRepo
{
	private readonly WarehouseDbContext _dbContext;
	public WarehouseRepo(WarehouseDbContext dbContext)
    {
        _dbContext = dbContext;
    }

	public async Task Add(WarehouseModel warehouse)
	{
		//_dbContext.Warehouses.Add(warehouse);
		//await _dbContext.SaveChangesAsync();
	}

	public async Task Delete(WarehouseModel warehouse)
	{
		//_dbContext.Warehouses.Remove(warehouse);
		//await _dbContext.SaveChangesAsync();
	}

	public List<WarehouseModel> GetAll()
	{
		//return _dbContext.Warehouses.ToList();
		return new List<WarehouseModel>();
	}

	public async Task<WarehouseModel> GetById(int id)
	{
		//var warehouse = await _dbContext.Warehouses.FirstOrDefaultAsync(x => x.Id == id);
		//return warehouse;
		return new WarehouseModel();
	}

	public async Task Update(WarehouseModel warehouse)
	{
		//_dbContext.Warehouses.Update(warehouse);
		//await _dbContext.SaveChangesAsync();
	}
}
