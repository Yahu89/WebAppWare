using Microsoft.AspNetCore.Mvc;
using WebAppWare.Models;
using WebAppWare.Repositories.Interfaces;

namespace WebAppWare.Controllers
{
    public class SupplierController : Controller
    {
        private readonly ISupplierRepo _supplierRepo;
        public SupplierController(ISupplierRepo supplierRepo)
        {
            _supplierRepo = supplierRepo;
        }
        public async Task<IActionResult> Index()
        {
            var suppliers = await _supplierRepo.GetAll();
            return View(suppliers);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(SupplierModel suplier)
        {
            if (ModelState.IsValid)
            {
                await _supplierRepo.Create(suplier);
                return RedirectToAction("Index");
            }
            
            return View();
        }

        public async Task<IActionResult> Edit(int id)
        {
            var supplier = await _supplierRepo.GetById(id);
            return View(supplier);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(SupplierModel supplier)
        {
            if (ModelState.IsValid)
            {
                await _supplierRepo.Update(supplier);
                return RedirectToAction("Index");
            }

            return View();           
        }

        public async Task<IActionResult> Delete(int id)
        {
            var supplier = await _supplierRepo.GetById(id);
            return View(supplier);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(SupplierModel supplier)
        {
            await _supplierRepo.Delete(supplier);
            return RedirectToAction("Index");
        }
    }
}
