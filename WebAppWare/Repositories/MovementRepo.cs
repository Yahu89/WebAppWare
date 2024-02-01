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

public class MovementRepo : IMovementRepo
{
	private readonly WarehouseDbContext _dbContext;

	private Expression<Func<WarehouseMovement, MovementModel>> MapToModel = x => new MovementModel()
	{
		Id = x.Id,
		Document = x.Document,
		MovementType = x.MovementType,
		CreationDate = x.CreationDate
	};

	private Expression<Func<MovementModel, WarehouseMovement>> MapToEntity = x => new WarehouseMovement()
	{
		Id = x.Id,
		Document = x.Document,
		MovementType = x.MovementType,
		CreationDate = x.CreationDate
	};

	public MovementRepo(WarehouseDbContext dbContext)
    {
        _dbContext = dbContext;
    }

	public async Task Create(MovementModel model)
	{
		var movement = MapToEntity.Compile().Invoke(model);
		_dbContext.WarehouseMovements.Add(movement);
		await _dbContext.SaveChangesAsync();
	}

	public async Task DeleteById(int id)
	{
		var moveToDelete = _dbContext.WarehouseMovements.First(x => x.Id == id);
		_dbContext.WarehouseMovements.Remove(moveToDelete);
		await _dbContext.SaveChangesAsync();
	}

	public async Task<List<MovementModel>> GetAll()
	{
		var result = await _dbContext.WarehouseMovements.Select(MapToModel)
												.OrderByDescending(x => x.CreationDate)
												.ToListAsync();

		return result;
	}

	public async Task<MovementModel> GetById(int id)
	{
		var movement = await _dbContext.WarehouseMovements.Select(MapToModel).FirstOrDefaultAsync(x => x.Id == id);
		return movement;
	}

	public async Task<MovementModel> GetLastMovement()
	{
		var lastMove = await _dbContext.WarehouseMovements.Select(MapToModel)
													.OrderBy(x => x.Id)
													.LastAsync();

		return lastMove;
	}



	public async Task<bool> IsDocumentNameUnique(string inputName)
	{
		var isNotUnique = await _dbContext.WarehouseMovements.Select(MapToModel).AnyAsync(x => x.Document == inputName);
		return !isNotUnique;
	}


}
