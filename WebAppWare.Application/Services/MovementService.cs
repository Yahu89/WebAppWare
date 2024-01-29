using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAppWare.Domain.Interfaces;
using WebAppWare.Infrastructure;

namespace WebAppWare.Application.Services;

public class MovementService : IMovementService
{
	private readonly IMovementRepo _movementRepo;
	public MovementService(IMovementRepo movementRepo)
    {
        _movementRepo = movementRepo;
    }
    public async Task<List<WarehouseMovement>> GetAll()
	{
		return await _movementRepo.GetAll();
	}
}
