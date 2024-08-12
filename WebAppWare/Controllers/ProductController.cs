using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebAppWare.Models;
using WebAppWare.Repositories.Interfaces;

namespace WebAppWare.Controllers
{
	[Authorize(Roles = "admin")]
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
		public async Task<ActionResult> Index()
		{
			try
			{
				var products = await _productRepo.GetAll();
				return View(products);
			}
			catch (Exception ex)
			{
				return Json(ex.ToString());
			}	
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
				try
				{
					await _imageRepo.Create(product);
					return RedirectToAction(nameof(Index));
				}
				catch (Exception ex)
				{
					return Json(ex.ToString());
				}
			}

			return View();
		}

		public async Task<IActionResult> Edit(int id)
		{
			try
			{
				var product = await _productRepo.GetById(id);
				return View(product);
			}
			catch (Exception ex)
			{
				return Json(ex.ToString());
			}
		}

		[HttpPost]
		public async Task<IActionResult> Edit(ProductModel product)
		{
			if (ModelState.IsValid)
			{
				try
				{
					await _imageRepo.Update(product);
					return RedirectToAction(nameof(Index));
				}
				catch (Exception ex)
				{
					return Json(ex.ToString());
				}		
			}

			return View();
		}

		public async Task<IActionResult> Delete(int id)
		{
			try
			{
				var product = await _productRepo.GetById(id);
				return View(product);
			}
			catch(Exception ex)
			{
				return Json(ex.ToString());
			}		
		}

		[HttpPost]
		public async Task<IActionResult> Delete(ProductModel product)
		{
			try
			{
				await _productRepo.Delete(product);
				return RedirectToAction(nameof(Index));
			}
			catch(Exception ex)
			{
				return Json(ex.ToString());
			}
		}
	}
}
