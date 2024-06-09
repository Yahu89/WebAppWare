using WebAppWare.Models;

namespace WebAppWare.Repositories.Interfaces;

public interface IOrderRepo
{
	Task<IEnumerable<OrderModel>> GetAll();
	Task<OrderModel> GetById(int id);
	Task<int> Create(OrderModel order);
	Task ValidateModel(OrderModel model);
	Task<string> SetOrderNumber(DateTime date);
	Task DeleteById(int id);
	Task Edit(OrderModel model);
}
