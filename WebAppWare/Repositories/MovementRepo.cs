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

public class MovementRepo : IMovementRepo
{
	private readonly WarehouseDbContext _dbContext;
	public MovementRepo(WarehouseDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task<List<WarehouseMovementModel>> GetAll()
	{
		return new List<WarehouseMovementModel>();
		//return await _dbContext.WarehouseMovements.ToListAsync();
	}
}
