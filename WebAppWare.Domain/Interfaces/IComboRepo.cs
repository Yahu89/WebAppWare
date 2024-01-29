using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAppWare.Infrastructure;

namespace WebAppWare.Domain.Interfaces;

public interface IComboRepo
{
	Task<List<Product>> GetAllProducts();
	Task<List<Warehouse>> GetAllWarehouses();
	Task<List<Supplier>> GetAllSuppliers();
	Task CreateMovement(WarehouseMovement warehouseMovement);
	Task<WarehouseMovement> GetLastMovement();
	Task CreateProductFlow(ProductsFlow productsFlow);
	Task CreateProductFlowList(List<ProductsFlow> productsFlowList);
	Task<List<ProductFlowModel>> GetAllProductFlowModel(string itemCode, string warehouse);
}
