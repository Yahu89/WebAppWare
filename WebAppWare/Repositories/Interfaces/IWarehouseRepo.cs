using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAppWare.Models;

namespace WebAppWare.Repositories.Interfaces;

public interface IWarehouseRepo
{
	List<WarehouseModel> GetAll();
	Task Add(WarehouseModel warehouse);
	Task Update(WarehouseModel warehouse);
	Task<WarehouseModel> GetById(int id);
	Task Delete(WarehouseModel warehouse);
}
