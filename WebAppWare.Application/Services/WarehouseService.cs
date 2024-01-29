using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAppWare.Domain.Interfaces;
using WebAppWare.Infrastructure;

namespace WebAppWare.Application.Services
{
	public class WarehouseService : IWarehouseService
	{
		private readonly IWarehouseRepo _warehouseRepo;
		public WarehouseService(IWarehouseRepo warehouseRepo)
        {
			_warehouseRepo = warehouseRepo;
        }

		public async Task Add(Warehouse warehouse)
		{
			await _warehouseRepo.Add(warehouse);
		}

		public async Task Delete(Warehouse warehouse)
		{
			await _warehouseRepo.Delete(warehouse);
		}

		public async Task<List<Warehouse>> GetAll()
		{
			return _warehouseRepo.GetAll();
		}

		public async Task<Warehouse> GetById(int id)
		{
			return await _warehouseRepo.GetById(id);		
		}

		public async Task Update(Warehouse warehouse)
		{
			await _warehouseRepo.Update(warehouse);
		}
	}
}
