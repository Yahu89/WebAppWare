using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAppWare.Models;

namespace WebAppWare.Repositories.Interfaces;

public interface IWarehouseRepo
{
	Task<IEnumerable<WarehouseModel>> GetAll();
	Task<int> GetWarehouseIdByName(string name);
	Task Add(WarehouseModel model);
	Task Update(WarehouseModel warehouse);
	Task<WarehouseModel> GetById(int id);
	Task Delete(WarehouseModel model);
	Task<List<ProductsAmountModel>> GetProductsAmount();
}
