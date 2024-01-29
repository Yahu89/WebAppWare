using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAppWare.Infrastructure;

namespace WebAppWare.Application.Services;

public interface IWarehouseService
{
	Task<List<Warehouse>> GetAll();
	Task Add(Warehouse warehouse);
	Task Update(Warehouse warehouse);
	Task<Warehouse> GetById(int id);
	Task Delete(Warehouse warehouse);
}
