using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAppWare.Infrastructure;

namespace WebAppWare.Application.Services;

public interface IComboService
{
	Task<List<Product>> GetAllProducts();
	Task<List<Supplier>> GetAllSuppliers();
	Task<List<Warehouse>> GetAllWarehouses();
	Task CreateMovement(WarehouseMovement warehouseMovement);
	Task<WarehouseMovement> GetLastMovement();
	Task CreateProductFlow(ProductsFlow productsFlow);
	Task CreateProductFlowList(List<ProductsFlow> productsFlow);
	Task<List<ProductFlowModel>> GetAllProductFlowModel(string itemCode, string warehouse);
}
