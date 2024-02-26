using Microsoft.AspNetCore.Mvc;
using WebAppWare.Models;
using WebAppWare.Repositories.Interfaces;

namespace WebAppWare.Controllers
{
	public class ProductController : Controller
	{
		private readonly IProductRepo _productRepo;
		private readonly IWebHostEnvironment _webHostEnvironment;
		private readonly IImageRepository _imageRepo;

		public ProductController(
			IProductRepo productRepo,
			IWebHostEnvironment webHostEnvironment,
			IImageRepository imageRepo
			)
		{
			_productRepo = productRepo;
			_webHostEnvironment = webHostEnvironment;
			_imageRepo = imageRepo;
		}
		public async Task<ActionResult<List<ProductModel>>> Index()
		{
			var products = await _productRepo.GetAll();
			return View(products);
		}

		public IActionResult Create()
		{
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> Create(ProductModel product)
		{
			if (ModelState.IsValid)
			{
				await _imageRepo.Create(product);

				return RedirectToAction(nameof(Index));
			}

			return View();
		}

		public async Task<IActionResult> Edit(int id)
		{
			var product = await _productRepo.GetById(id);
			return View(product);
		}

		[HttpPost]
		public async Task<IActionResult> Edit(ProductModel product)
		{
			if (ModelState.IsValid)
			{
				await _productRepo.Update(product);
				return RedirectToAction("Index");
			}

			return View();
		}

		public async Task<IActionResult> Delete(int id)
		{
			var product = await _productRepo.GetById(id);
			return View(product);
		}

		[HttpPost]
		public async Task<IActionResult> Delete(ProductModel product)
		{
			await _productRepo.Delete(product);
			return RedirectToAction("Index");
		}
	}
}
