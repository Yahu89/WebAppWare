using WebAppWare.Models;

namespace WebAppWare.Repositories.Interfaces;

public interface IProductFlowRepo
{
    Task<List<ProductFlowModel>> GetAll();
    Task<IEnumerable<ProductFlowModel>> GetAllCumulative(int prodId, int wareId);
    Task<ProductFlowModel> GetById(int id);
    Task<List<ProductFlowModel>> GetProductFlowsByMoveId(int id);
    Task<IEnumerable<ProductFlowModel>> GetBySearch(string warehouse, string itemCode, string supplier);
    Task CreateRange(IEnumerable<ProductFlowModel> model, int id);
	Task DeleteById(int id);
    Task DeleteRange(IEnumerable<ProductFlowModel> model);
    Task<bool> IsReadyToDeleteProductFlow(int id);
    List<ProductFlowModel> FromCollectionToProductFlowModel(IFormCollection collection, int moveId);
    Task<int> GetCurrentQtyPerItemAndWarehouse(int item, int warehouseId);
}
