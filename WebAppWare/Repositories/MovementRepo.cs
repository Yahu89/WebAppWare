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
	private readonly WarehouseBaseContext _dbContext;
	private readonly IProductFlowRepo _productFlowRepo;

	private Expression<Func<WarehouseMovement, WarehouseMovementModel>> MapToModel = x => new WarehouseMovementModel()
	{
		Id = x.Id,
		Document = x.Document,
		MovementType = (MovementType)x.MovementType,
		CreationDate = x.CreationDate,
	};

	private Expression<Func<WarehouseMovementModel, WarehouseMovement>> MapToEntity = x => new WarehouseMovement()
	{
		Id = x.Id,
		Document = x.Document,
		MovementType = (int)x.MovementType,
		CreationDate = x.CreationDate,
	};

	public MovementRepo(WarehouseBaseContext dbContext, IProductFlowRepo productFlowRepo)
	{
		_dbContext = dbContext;
		_productFlowRepo = productFlowRepo;
	}

	public async Task Create(WarehouseMovementModel model)
	{
		var movement = MapToEntity.Compile().Invoke(model);

		if (string.IsNullOrEmpty(movement.Document) || model.WarehouseId == 0)
		{
			throw new Exception("Niepoprawne dane");
		}

		if (!await IsDocumentNameUnique(movement.Document))
		{
			throw new Exception("ISTNIEJE JUZ!");
		}

		var items = model.ProductFlowModels;


		if (!IsUniqueAndQtyCorrectForPzWz(items))
		{
			throw new Exception("Niepoprawne dane");
		}

		if (model.MovementType is MovementType.PZ)
		{
			_dbContext.WarehouseMovements.Add(movement);
			items.ToList().ForEach(x => x.WarehouseId = model.WarehouseId);

			try
			{
				await _dbContext.SaveChangesAsync();
			}
			catch (Exception ex)
			{
				throw new Exception($"{ex.Message}", ex);
			}	

			await _productFlowRepo.CreateRange(items, movement.Id);
		}
		else if (model.MovementType is MovementType.WZ)
		{
			items.ToList().ForEach(x => x.WarehouseId = model.WarehouseId);

			if (await IsQtyEnoughToCreateWz(model))
			{
				items.ToList().ForEach(x => x.Quantity = -x.Quantity);
				_dbContext.WarehouseMovements.Add(movement);
				await _dbContext.SaveChangesAsync();

				await _productFlowRepo.CreateRange(items, movement.Id);
			}
			else
			{
				throw new Exception("Coś poszło nie tak...");
			}
		}
		else if (model.MovementType is MovementType.MM)
		{
			if (model.WarehouseId == 0)
			{
				throw new Exception("Niepoprawne dane");
			}

			if (await IsQtyEnoughToCreateWz(model))
			{		
				model.WarehouseId = model.WarehouseId;
				_dbContext.WarehouseMovements.Add(movement);
				await _dbContext.SaveChangesAsync();


				items.ToList().ForEach(x => x.Quantity = -x.Quantity);
				items.ToList().ForEach(x => x.WarehouseId = model.WarehouseId);
				await _productFlowRepo.CreateRange(items, movement.Id);

				items.ToList().ForEach(x => x.Quantity = -x.Quantity);
				items.ToList().ForEach(x => x.WarehouseId = model.WarehouseToId);
				await _productFlowRepo.CreateRange(items, movement.Id);
			}
		}
	}

	public async Task DeleteById(int id)
	{
		var moveToDelete = _dbContext.WarehouseMovements.First(x => x.Id == id);
		_dbContext.WarehouseMovements.Remove(moveToDelete);
		await _dbContext.SaveChangesAsync();
	}

	public async Task<List<WarehouseMovementModel>> GetAll()
	{
		var result = await _dbContext.WarehouseMovements.Select(MapToModel)
												.OrderByDescending(x => x.CreationDate)
												.ToListAsync();

		return result;
	}

	public async Task<WarehouseMovementModel> GetById(int id)
	{
		var movement = await _dbContext.WarehouseMovements.Select(MapToModel).FirstOrDefaultAsync(x => x.Id == id);
		return movement;
	}

	public async Task<WarehouseMovementModel> GetLastMovement()
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

	public async Task<bool> IsQtyEnoughToCreateWz(WarehouseMovementModel model)
	{
		var items = model.ProductFlowModels;

		foreach (var item in items)
		{
			int actualQty = await _productFlowRepo.GetCurrentQtyPerItemAndWarehouse((int)item.ProductId, model.WarehouseId);

			if (actualQty < item.Quantity)
			{
				return false;
			}
		}

		return true;
	}

	public async Task<string> SetMovementNumber(DateTime date, MovementType movementType)
	{
		var recordsPerDate = await _dbContext.WarehouseMovements.Select(MapToModel)
																.Where(x => x.CreationDate.Date == date)
																.ToListAsync();

		string counter = (recordsPerDate.Count + 1).ToString();

		if (counter.Length == 1)
		{
			counter = "0" + counter;
		}

		string docNumber = movementType.ToString() + date.Day.ToString("00") 
						+ date.Month.ToString("00") + date.Year.ToString().Substring(2) + counter;

		return docNumber;
	}

	public bool IsUniqueAndQtyCorrectForPzWz(IEnumerable<ProductFlowModel> itemCodes)
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

		var productFlowsWithinMove = await _productFlowRepo.GetProductFlowsByMoveId(id);
		int wareId = (int)productFlowsWithinMove.FirstOrDefault().WarehouseId;
		int? wareToId = 0;
		DateTime insertDate = movement.CreationDate;

		foreach (var item in productFlowsWithinMove)
		{
			int qty = item.Quantity;
			var prodFlows = await _productFlowRepo.GetAllCumulative((int)item.ProductId, wareId);
			var prodFlowsLimited = prodFlows.Where(x => x.CreationDate > insertDate).ToList();

			if (movement.MovementType is MovementType.MM)
			{
				wareToId = (int)productFlowsWithinMove.FirstOrDefault().WarehouseId;
				productFlowsWithinMove = (await _productFlowRepo.GetProductFlowsByMoveId(id)).Where(x => x.Quantity > 0).ToList();
				prodFlows = await _productFlowRepo.GetAllCumulative((int)item.ProductId, (int)wareToId);
				qty = Math.Abs(item.Quantity);
				prodFlowsLimited = prodFlows.Where(x => x.CreationDate > insertDate).ToList();
			}

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
}
