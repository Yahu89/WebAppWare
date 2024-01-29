using Microsoft.AspNetCore.Mvc;
using WebAppWare.Application.Services;
using WebAppWare.Infrastructure;

namespace WebAppWare.Controllers
{
	public class WarehouseController : Controller
	{
		private readonly IWarehouseService _warehouseService;
		public WarehouseController(IWarehouseService warehouseService)
        {
			_warehouseService = warehouseService;
        }
        public async Task<IActionResult> Index()
		{
			var result = await _warehouseService.GetAll();
			return View(result);
		}

		public IActionResult Create()
		{		
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> Create(Warehouse warehouse)
		{
			if (ModelState.IsValid)
			{
                await _warehouseService.Add(warehouse);
                return RedirectToAction("Index");
            }

			return View();
		}

		public async Task<IActionResult> Edit(int id)
		{
			Warehouse warehouse = await _warehouseService.GetById(id);
			return View(warehouse);
		}

		[HttpPost]
		public async Task<IActionResult> Edit(Warehouse warehouse)
		{
			if (ModelState.IsValid)
			{
                await _warehouseService.Update(warehouse);
                return RedirectToAction("Index");
            }

			return View();
		}

		public async Task<IActionResult> Delete(int id)
		{
			var warehouse = await _warehouseService.GetById(id);
			return View(warehouse);
		}

		[HttpPost]
		public async Task<IActionResult> Delete(Warehouse warehouse)
		{
			await _warehouseService.Delete(warehouse);
			return RedirectToAction("Index");
		}
	}
}
