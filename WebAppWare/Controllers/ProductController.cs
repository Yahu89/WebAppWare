﻿using Microsoft.AspNetCore.Mvc;
using WebAppWare.Models;
using WebAppWare.Repositories.Interfaces;

namespace WebAppWare.Controllers
{
	public class ProductController : Controller
	{
		private readonly IProductRepo _productRepo;
		public ProductController(IProductRepo productRepo)
        {
			_productRepo = productRepo;
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
