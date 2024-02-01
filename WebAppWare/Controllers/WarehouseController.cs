using Microsoft.AspNetCore.Mvc;
using WebAppWare.Models;
using WebAppWare.Repositories.Interfaces;

namespace WebAppWare.Controllers
{
	public class WarehouseController : Controller
	{
		private readonly IWarehouseRepo _warehouseRepo;
		public WarehouseController(IWarehouseRepo warehouseRepo)
        {
			_warehouseRepo = warehouseRepo;
        }
        public async Task<ActionResult<List<WarehouseModel>>> Index()
		{
			var warehouses = await _warehouseRepo.GetAll();
			return View(warehouses);
		}

		public IActionResult Create()
		{		
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> Create(WarehouseModel model)
		{
			if (ModelState.IsValid)
			{
				await _warehouseRepo.Add(model);
				return RedirectToAction("Index");
			}
	
			return View();
		}

		public async Task<IActionResult> Edit(int id)
		{
			var warehouse = await _warehouseRepo.GetById(id);
			return View(warehouse);
		}

		[HttpPost]
		public async Task<IActionResult> Edit(WarehouseModel warehouse)
		{
			if (ModelState.IsValid)
			{
                await _warehouseRepo.Update(warehouse);
                return RedirectToAction("Index");
            }

			return View();
		}

		public async Task<IActionResult> Delete(int id)
		{
			var warehouse = await _warehouseRepo.GetById(id);
			return View(warehouse);
		}

		[HttpPost]
		public async Task<IActionResult> Delete(WarehouseModel model)
		{
			await _warehouseRepo.Delete(model);
			return RedirectToAction("Index");
		}

		public async Task<IActionResult> TotalAmount()
		{
			var totalAmount = await _warehouseRepo.GetProductsAmount();
			return View(totalAmount);
		}
	}
}
