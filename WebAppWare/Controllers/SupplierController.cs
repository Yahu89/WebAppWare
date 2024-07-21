using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebAppWare.Database.Entities;
using WebAppWare.Models;
using WebAppWare.Repositories.Interfaces;

namespace WebAppWare.Controllers
{
    [Authorize(Roles = "purchase,admin")]
    public class SupplierController : Controller
    {
        private readonly ISupplierRepo _supplierRepo;
        public SupplierController(ISupplierRepo supplierRepo)
        {
            _supplierRepo = supplierRepo;
        }
        public async Task<IActionResult> Index()
        {
            try
            {
				var suppliers = await _supplierRepo.GetAll();
				return View(suppliers);
			}
            catch (Exception ex)
            {
                return Json(ex.Message);
            }
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            try
            {
				var supplier = await _supplierRepo.GetById(id);
				return View(supplier);
			}
            catch (Exception ex)
            {
                return Json(ex.Message);
            }   
        }

        [HttpPost]
        public async Task<IActionResult> Upsert(SupplierModel model)
        {
            if (ModelState.IsValid)
            {
                if (model.Id == 0)
                {
                    await _supplierRepo.Create(model);
					return View(nameof(Index));
				}
                else
                {
                    await _supplierRepo.Update(model);
					return View(nameof(Index));
				}             
            }

            return View(nameof(Create));
        }

        public async Task<IActionResult> Delete(int id)
        {
            try
            {
				var supplier = await _supplierRepo.GetById(id);
				return View(supplier);
			}
            catch(Exception ex)
            {
                return Json($"{ex.Message}");
            }         
        }

        [HttpPost]
        public async Task<IActionResult> Delete(SupplierModel supplier)
        {
            try
            {
				await _supplierRepo.Delete(supplier);
				return RedirectToAction(nameof(Index));
			}
            catch (Exception ex)
            {
                return Json(ex.Message);
            }        
        }
    }
}
