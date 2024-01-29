using Microsoft.AspNetCore.Mvc;
using WebAppWare.Application.Services;
using WebAppWare.Infrastructure;

namespace WebAppWare.Controllers
{
    public class SupplierController : Controller
    {
        private readonly ISupplierService _supplierService;
        public SupplierController(ISupplierService supplierService)
        {
            _supplierService = supplierService;
        }
        public async Task<IActionResult> Index()
        {
            var suppliers = await _supplierService.GetAll();
            return View(suppliers);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Supplier suplier)
        {
            if (ModelState.IsValid)
            {
                await _supplierService.Create(suplier);
                return RedirectToAction("Index");
            }
            
            return View();
        }

        public async Task<IActionResult> Edit(int id)
        {
            var supplier = await _supplierService.GetById(id);
            return View(supplier);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Supplier supplier)
        {
            if (ModelState.IsValid)
            {
                await _supplierService.Update(supplier);
                return RedirectToAction("Index");
            }

            return View();           
        }

        public async Task<IActionResult> Delete(int id)
        {
            var supplier = await _supplierService.GetById(id);
            return View(supplier);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(Supplier supplier)
        {
            await _supplierService.Delete(supplier);
            return RedirectToAction("Index");
        }
    }
}
