using Microsoft.AspNetCore.Mvc;
using WebAppWare.Application.Services;
using WebAppWare.Domain.Interfaces;
using WebAppWare.Infrastructure;

namespace WebAppWare.Controllers
{
	public class ProductController : Controller
	{
		private readonly IProductService _productService;
		public ProductController(IProductService productService)
        {
			_productService = productService;
        }
        public async Task<IActionResult> Index()
		{
			var products = await _productService.GetAll();
			return View(products);
		}

		public IActionResult Create()
		{
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> Create(Product product)
		{
			if (ModelState.IsValid)
			{
                await _productService.Add(product);
                return RedirectToAction("Index");
            }
			
			return View();
		}

		public async Task<IActionResult> Edit(int id)
		{
			var product = await _productService.GetById(id);
			return View(product);
		}

		[HttpPost]
		public async Task<IActionResult> Edit(Product product)
		{
			if (ModelState.IsValid)
			{
                await _productService.Update(product);
                return RedirectToAction("Index");
            }

			return View();
		}

		public async Task<IActionResult> Delete(int id)
		{
			var product = await _productService.GetById(id);
			return View(product);
		}

		[HttpPost]
		public async Task<IActionResult> Delete(Product product)
		{
			await _productService.Delete(product);
			return RedirectToAction("Index");
		}
	}
}
