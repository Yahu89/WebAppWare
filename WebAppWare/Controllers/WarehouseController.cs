using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebAppWare.Database.Entities;
using WebAppWare.Models;
using WebAppWare.Repositories.Interfaces;

namespace WebAppWare.Controllers
{
	[Authorize(Roles = "warehouse,admin")]
	public class WarehouseController : Controller
	{
		private readonly IWarehouseRepo _warehouseRepo;
		public WarehouseController(IWarehouseRepo warehouseRepo)
        {
			_warehouseRepo = warehouseRepo;
        }

		[HttpGet]
        public async Task<ActionResult> Index()
		{
			try
			{
				var warehouses = await _warehouseRepo.GetAll();
				return View(warehouses);
			}
			catch (Exception ex)
			{
				return Json(ex.Message);
			}			
		}

		public IActionResult Create()
		{		
			return View();
		}


		[HttpGet]
		public async Task<IActionResult> Edit(int id)
		{
			try
			{
				var warehouse = await _warehouseRepo.GetById(id);
				return View(warehouse);
			}
			catch (Exception ex)
			{
				return Json(ex.Message);
			}			
		}


		[HttpPost]
		public async Task<IActionResult> Upsert(WarehouseModel model)
		{
			if (model.Id == 0)
			{
                if (ModelState.IsValid)
                {
                    await _warehouseRepo.Add(model);
                    return View(nameof(Index));
                }

				return View(nameof(Create));
            }
			else
			{
                if (ModelState.IsValid)
                {
                    await _warehouseRepo.Update(model);
                    return View(nameof(Index));
                }

				return View(nameof(Edit), model);
            }
		}

		[HttpGet]
		public async Task<IActionResult> Delete(int id)
		{
			try
			{
				var warehouse = await _warehouseRepo.GetById(id);
				return View(warehouse);
			}
			catch(Exception ex)
			{
				return Json(ex.Message);
			}			
		}

		[HttpPost]
		public async Task<IActionResult> Delete(WarehouseModel model)
		{
			try
			{
				await _warehouseRepo.Delete(model);
				return RedirectToAction(nameof(Index));
			}
			catch (Exception ex)
			{
				return Json(ex.Message);
			}			
		}

		[HttpGet]
		public async Task<IActionResult> TotalAmount()
		{
			try
			{
				var totalAmount = await _warehouseRepo.GetProductsAmount();
				return View(totalAmount);
			}
			catch (Exception ex)
			{
				return Json(ex.Message);
			}		
		}
	}
}
