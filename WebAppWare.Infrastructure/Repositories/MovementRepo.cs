using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAppWare.Domain.Interfaces;
using WebAppWare.Infrastructure.BaseContext;

namespace WebAppWare.Infrastructure.Repositories;

public class MovementRepo : IMovementRepo
{
	private readonly WarehouseBaseContext _warehouseBaseContext;
	public MovementRepo(WarehouseBaseContext warehouseBaseContext)
    {
        _warehouseBaseContext = warehouseBaseContext;
    }
    public async Task<List<WarehouseMovement>> GetAll()
	{
		return await _warehouseBaseContext.WarehouseMovements.ToListAsync();
	}
}
