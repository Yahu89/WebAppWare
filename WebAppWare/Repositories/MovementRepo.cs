using Microsoft.AspNetCore.Http.HttpResults;
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

	public async Task<int> Create(MovementModel model)
	{
		var movement = MapToEntity.Compile().Invoke(model);
		_dbContext.WarehouseMovements.Add(movement);

		await _dbContext.SaveChangesAsync();

		return movement.Id;
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
		if (inputName == null)
		{
			throw new NullReferenceException(nameof(inputName));
		}

		var isNotUnique = await _dbContext.WarehouseMovements.Select(MapToModel).AnyAsync(x => x.Document == inputName);
		return !isNotUnique;
	}

	public List<ProductFlowModel> GetProductFlowsFromForm(ProductFlowMovementModel obj)
	{
		var itemCodes = obj.ProductFlowModels
									.Where(x => x != null && x.ProductId != null && x.SupplierId != null)
									.Select(x => new ProductFlowModel()
									{
										ProductId = x.ProductId,
										ProductItemCode = x.ProductItemCode,
										Supplier = x.Supplier,
										SupplierId = x.SupplierId,
										Warehouse = x.Warehouse,
										Quantity = x.Quantity,
										WarehouseId = obj.WarehouseId,
										MovementType = x.MovementType,
									})
									.ToList();

		return itemCodes;
	}

	public async Task<bool> IsPossibleToCreateWz(IFormCollection collection)
	{
		IProductFlowRepo _productFlowRepo = new ProductFlowRepo(_dbContext);

		var movement = FromCollectionToMovementModel(collection, MovementType.WZ);
		var prodFlow = _productFlowRepo.FromCollectionToProductFlowModel(collection, 0);
		var warehouseId = prodFlow.First().WarehouseId;

		if (string.IsNullOrEmpty(movement.Document) || warehouseId == 0)
			return false;

		if (!await IsDocumentNameUnique(movement.Document))
			return false;

		//itemCodes = obj.ProductFlowModels
		//									.Where(x => x != null && x.ProductId != null && x.SupplierId != null)
		//									.Select(x => new ProductFlowModel()
		//									{
		//										ProductId = x.ProductId,
		//										ProductItemCode = x.ProductItemCode,
		//										Supplier = x.Supplier,
		//										SupplierId = x.SupplierId,
		//										Warehouse = x.Warehouse,
		//										Quantity = x.Quantity,
		//										WarehouseId = obj.WarehouseId,
		//										MovementType = x.MovementType,
		//									})
		//									.ToList();

		if (!IsUniqueAndQtyCorrectForPzWz(prodFlow))
		{
			return false;
		}

		//if (prodFlow
		//			.GroupBy(x => x.ProductId)
		//			.Any(x => x.Count() > 1))
		//	return false;

		//if (prodFlow
		//		.Any(x => x.Quantity <= 0))
		//	return false;

		

		foreach (var item in prodFlow)
		{
			var cumulativeValueList = await _productFlowRepo.GetAllCumulative((int)item.ProductId, (int)item.WarehouseId);
			int actualQty = cumulativeValueList.Count() == 0 ? 0 : cumulativeValueList.Last().Cumulative;

			if (actualQty < item.Quantity)
			{
				return false;
			}
		}

		return true;
	}

	public async Task<string> SetMovementNumber(DateTime date, MovementType movementType)
	{
		var recordsPerDate = await _dbContext.WarehouseMovements.Select(MapToModel).Where(x => x.CreationDate.Date == date).ToListAsync();
		string counter = (recordsPerDate.Count + 1).ToString();

		if (counter.Length == 1)
		{
			counter = "0" + counter;
		}

		string docNumber = movementType.ToString() + date.Day.ToString("00") + date.Month.ToString("00") + date.Year.ToString().Substring(2) + counter;
		return docNumber;
	}

	public bool IsUniqueAndQtyCorrectForPzWz(List<ProductFlowModel> itemCodes)
	{
		if (itemCodes
				.GroupBy(x => x.ProductId)
				.Any(x => x.Count() > 1))
			return false;

		if (itemCodes
				.Any(x => x.Quantity <= 0 || string.IsNullOrEmpty(x.Quantity.ToString())))
			return false;

		return true;
	}

	public async Task<bool> IsPossibleToDeletePzWz(int id)
	{
		var movement = await GetById(id);

		IProductFlowRepo _productFlowRepo = new ProductFlowRepo(_dbContext);
		var productFlowsWithinMove = await _productFlowRepo.GetProductFlowsByMoveId(id);
		int wareId = (int)productFlowsWithinMove.FirstOrDefault().WarehouseId;
		DateTime insertDate = movement.CreationDate;

		foreach (var item in productFlowsWithinMove)
		{
			int qty = item.Quantity;
			var prodFlows = await _productFlowRepo.GetAllCumulative((int)item.ProductId, wareId);
			var prodFlowsLimited = prodFlows.Where(x => x.CreationDate > insertDate).ToList();

			int counter = prodFlowsLimited.Count;
			int minValue;

			if (counter == 0)
			{
				minValue = prodFlows.Last().Cumulative;
			}
			else
			{
				minValue = prodFlowsLimited.Min(x => x.Cumulative);
			}

			if (minValue < qty)
			{
				return false;
			}
		}

		return true;
	}

	public MovementModel FromCollectionToMovementModel(IFormCollection collection, MovementType type)
	{
		var document = collection["Document"];

		MovementModel movement = new MovementModel()
		{
			Document = document,
			CreationDate = DateTime.Now,
			MovementType = type
		};

		return movement;
	}
}
