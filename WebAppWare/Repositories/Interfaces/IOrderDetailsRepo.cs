using WebAppWare.Models;

namespace WebAppWare.Repositories.Interfaces;

public interface IOrderDetailsRepo
{
	Task<IEnumerable<OrderDetailsModel>> GetAll();
	Task<List<OrderDetailsModel>> GetByOrderId(int id);
	Task CreateRange(IEnumerable<OrderDetailsModel> model);
	bool IsDataCorrect(OrderDetailsModelView model);
	Task EditRange(IEnumerable<OrderDetailsModel> model);
}
