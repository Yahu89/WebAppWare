using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Linq.Expressions;
using WebAppWare.Database;
using WebAppWare.Database.Entities;
using WebAppWare.Models;
using WebAppWare.Repositories.Interfaces;
using WepAppWare.Database.Entities;

namespace WebAppWare.Repositories;

public class OrderDetailsRepo : IOrderDetailsRepo
{
	private readonly WarehouseBaseContext _dbContext;

	private Expression<Func<OrderItem, OrderDetailsModel>> MapToModel = x => new OrderDetailsModel()
	{
		Id = x.Id,
		OrderId = x.OrderId,
		Document = x.Order.Document,
		SupplierName = x.Order.Supplier.Name,
		SupplierId = x.Order.Supplier.Id,
		SupplierEmail = x.Order.Supplier.Email,
		CreationDate = x.Order.CreationDate,
		ProductId = x.ProductId,
		ProductItemCode = x.Product.ItemCode,
		//Status = x.Order.Status,
		Remarks = x.Order.Remarks,
		Quantity = x.Quantity
	};

	private Expression<Func<OrderDetailsModel, OrderItem>> MapToEntity = x => new OrderItem()
	{
		Id = x.Id,
		OrderId = x.OrderId,
		ProductId = x.ProductId,
		Quantity = x.Quantity
	};
	public OrderDetailsRepo(WarehouseBaseContext dbContext)
	{
		_dbContext = dbContext;
	}

	public async Task CreateRange(IEnumerable<OrderDetailsModel> model, int orderId)
	{

		var results = model.Select(MapToEntity.Compile()).ToList();
		results.ForEach(x => x.OrderId = orderId);
		_dbContext.OrderItems.AddRange(results);
		await _dbContext.SaveChangesAsync();
	}

	public async Task EditRange(IEnumerable<OrderDetailsModel> model, int orderId)
	{
		var results = model.Select(MapToEntity.Compile()).ToList();
		results.ForEach(x => x.OrderId = orderId);
		_dbContext.OrderItems.UpdateRange(results);
		await _dbContext.SaveChangesAsync();
	}

	public async Task<IEnumerable<OrderDetailsModel>> GetAll()
	{
		var results = await _dbContext.OrderItems.Select(MapToModel).ToListAsync();
		return results;
	}

	public async Task<List<OrderDetailsModel>> GetByOrderId(int id)
	{
		var results = await _dbContext.OrderItems
			.Include(x => x.Order)
			.Select(MapToModel)
			.Where(x => x.OrderId == id)
			.ToListAsync();

		if (results.Count == 0)
		{
			throw new NullReferenceException(nameof(results));
		}

		return results;
	}

	public bool IsDataCorrect(List<OrderDetailsModel> model)
	{
		var eachFieldFulfilled = model.Where(x => x.ProductId != 0 && x.Quantity > 0).ToList();

		if (eachFieldFulfilled.Count() > 0)
		{
			return true;
		}

		return false;
	}
}
