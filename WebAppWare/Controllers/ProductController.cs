using Microsoft.AspNetCore.Mvc;
using WebAppWare.Models;
using WebAppWare.Repositories.Interfaces;

namespace WebAppWare.Controllers
{
	public class ProductController : Controller
	{
		private readonly IProductRepo _productRepo;
		private readonly IWebHostEnvironment _webHostEnvironment;
		public ProductController(IProductRepo productRepo,
								IWebHostEnvironment webHostEnvironment)
        {
			_productRepo = productRepo;
			_webHostEnvironment = webHostEnvironment;
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
				//string fileName = Path.GetFileNameWithoutExtension(product.ImageFile.FileName);
				//string extension = Path.GetExtension(product.ImageFile.FileName);
				//fileName = fileName + DateTime.Now.ToString("yymmssffff") + extension;
				//fileName = Path.Combine(_webHostEnvironment.ContentRootPath, "Images", fileName);

				var fileName = await _productRepo.CreateProductImgUrl(product);

				var stream = new FileStream(fileName, FileMode.Create);

				await product.ImageFile.CopyToAsync(stream);
				product.ImgUrl = fileName;

				await _productRepo.Add(product);

				return RedirectToAction("Index");
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
