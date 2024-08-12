using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using WebAppWare.Database;
using WebAppWare.Database.Entities;
using WebAppWare.Models;
using WebAppWare.Repositories.Interfaces;
using WebAppWare.Utils;

namespace WebAppWare.Repositories;

public class OrderRepo : IOrderRepo
{
	private readonly WarehouseBaseContext _dbContext;
	private readonly IOrderDetailsRepo _orderDetailsRepo;

	private Expression<Func<Order, OrderModel>> MapToModel = x => new OrderModel()
	{
		Id = x.Id,
		Document = x.Document,
		SupplierId = x.SupplierId,
		SupplierName = x.Supplier.Name,
		SupplierEmail = x.Supplier.Email,
		CreationDate = x.CreationDate,
		Status = x.Status,
		StatusName = x.Status.DisplayName(),
		Remarks = x.Remarks,		
	};

	private Expression<Func<OrderModel, Order>> MapToEntity = x => new Order()
	{
		Id = x.Id,
		Document = x.Document,
		SupplierId = x.SupplierId,
		CreationDate = x.CreationDate,
		Status = x.Status,
		Remarks = x.Remarks
	};
	public OrderRepo(
					WarehouseBaseContext dbContext,
					IOrderDetailsRepo orderDetailsRepo
					)
    {
        _dbContext = dbContext;
		_orderDetailsRepo = orderDetailsRepo;
	}

	public async Task<int> Create(OrderModel model)
	{
		var entity = MapToEntity.Compile().Invoke(model);

		_dbContext.Orders.Add(entity);

		await _dbContext.SaveChangesAsync();

		return entity.Id;
	}

	public async Task DeleteById(int id)
	{
		var orderToDelete = _dbContext.Orders.FirstOrDefault(o => o.Id == id);
		_dbContext.Orders.Remove(orderToDelete);
		await _dbContext.SaveChangesAsync();
	}

	public async Task Edit(OrderModel model)
	{
		var order = MapToEntity.Compile().Invoke(model);
		_dbContext.Orders.Update(order);

		await _dbContext.SaveChangesAsync();
	}

	public async Task<IEnumerable<OrderModel>> GetAll()
	{
		var result = await _dbContext.Orders
			.Select(MapToModel)
			.OrderByDescending(x => x.CreationDate)
			.ToListAsync();

		return result;
	}

	public async Task<OrderModel> GetById(int id)
	{
		var order = await _dbContext.Orders
			.AsNoTracking()
			.Include(x => x.OrderItems)
			.Select(MapToModel)		
			.FirstOrDefaultAsync(x => x.Id == id);

		if (order == null)
		{
			throw new NullReferenceException(nameof(order));
		}

		order.OrderDetails = await _orderDetailsRepo.GetByOrderId(id);

		return order;
	}

	public async Task ValidateModel(OrderModel model)
	{
		if (model.SupplierId == 0 || string.IsNullOrEmpty(model.Document))
		{
			throw new Exception("Nieprawidłowe dane");
		}

		if (!_orderDetailsRepo.IsDataCorrect(model.OrderDetails))
		{
			throw new Exception("Nieprawidłowe dane");
		}
	}

	public async Task<string> SetOrderNumber(DateTime date)
	{
		var recordsPerDate = await _dbContext.Orders
			.Select(MapToModel)
			.Where(x => x.CreationDate.Date == date)
			.ToListAsync();

		string counter = (recordsPerDate.Count + 1).ToString();

		if (counter.Length == 1)
		{
			counter = "0" + counter;
		}

		string docNumber = "PO" + date.Day.ToString("00") + date.Month.ToString("00") + date.Year.ToString().Substring(2) + counter;
		return docNumber;
	}
}
