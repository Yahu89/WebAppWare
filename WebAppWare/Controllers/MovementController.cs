﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebAppWare.Database;
using WebAppWare.Database.Entities;
using WebAppWare.Models;
using WebAppWare.Repositories.Interfaces;

namespace WebAppWare.Controllers
{
	[Authorize(Roles = "admin,warehouse")]
	public class MovementController : Controller
	{
		private readonly IMovementRepo _movementRepo;
		private readonly IProductRepo _productRepo;
		private readonly IWarehouseRepo _warehouseRepo;
		private readonly ISupplierRepo _supplierRepo;
		private readonly IProductFlowRepo _productFlowRepo;
		private readonly IImageRepository _imageRepository;
		private readonly WarehouseBaseContext _db;
		public MovementController(
			IMovementRepo movementRepo,
			IProductRepo productRepo,
			IWarehouseRepo warehouseRepo,
			ISupplierRepo supplierRepo,
			IProductFlowRepo productFlowRepo,
			IImageRepository imageRepository,
			WarehouseBaseContext db
			)
		{
			_movementRepo = movementRepo;
			_productRepo = productRepo;
			_warehouseRepo = warehouseRepo;
			_supplierRepo = supplierRepo;
			_productFlowRepo = productFlowRepo;
			_imageRepository = imageRepository;
			_db = db;
		}
		public async Task<IActionResult> Index()
		{
			try
			{
				var movements = await _movementRepo.GetAll();

				return View(movements);
			}
			catch ( Exception ex )
			{
				return Json(ex.Message);
			}			
		}

		private async Task<ProductFlowMovementModel> SetComboBoxForMovement(MovementType moveType)
		{
			return new ProductFlowMovementModel()
			{
				Products = (await _productRepo.GetAll()).Select(x => new SelectListItem()
				{
					Text = x.ItemCode,
					Value = x.Id.ToString(),
				}),
				Suppliers = (await _supplierRepo.GetAll()).Select(x => new SelectListItem()
				{
					Text = x.Name,
					Value = x.Id.ToString(),
				}),
				Warehouses = (await _warehouseRepo.GetAll()).Select(x => new SelectListItem()
				{
					Text = x.Name,
					Value = x.Id.ToString(),
				}),
				Document = await _movementRepo.SetMovementNumber(DateTime.Today, moveType)
			};
		}

		public async Task<IActionResult> CreatePz()
		{
			return View(await SetComboBoxForMovement(MovementType.PZ));
		}

		public async Task<IActionResult> CreateWz()
		{
			return View(await SetComboBoxForMovement(MovementType.WZ));
		}

		public async Task<IActionResult> CreateMm()
		{
			return View(await SetComboBoxForMovement(MovementType.MM));
		}

		public async Task<IActionResult> Delete(int id)
		{
			ProductFlowMovementModel obj = new ProductFlowMovementModel();

			var movement = await _movementRepo.GetById(id);
			obj.ProductFlowModels = await _productFlowRepo.GetProductFlowsByMoveId(id);
			string warehouse = obj.ProductFlowModels.FirstOrDefault(x => x.Movement.Id == id).WarehouseName;
			string supplierName = obj.ProductFlowModels.FirstOrDefault().Supplier.Name;

			obj.Document = movement.Document;
			obj.Warehouse = warehouse;
			obj.SupplierName = supplierName;

			return View(obj);
		}

		[HttpPost]
		public async Task<IActionResult> DeletePost(int id)
		{
			var movement = await _movementRepo.GetById(id);

			if (movement.MovementType == MovementType.WZ)
			{
				try
				{
					await _movementRepo.DeleteById(id);
				}
				catch (Exception ex)
				{
					throw new InvalidOperationException(ex.Message);
				}
				
				return RedirectToAction(nameof(Index));
			}

			if (await _movementRepo.IsPossibleToDeletePzWz(id))
			{
				try
				{
					await _movementRepo.DeleteById(id);
				}
				catch (Exception ex)
				{
					throw new InvalidOperationException(ex.Message);
				}
				
				return RedirectToAction(nameof(Index));
			}

			return BadRequest();
		}

		public async Task<IActionResult> DeleteMmM(int id)
		{
			var movement = await _movementRepo.GetById(id);
			var productFlows = await _productFlowRepo.GetProductFlowsByMoveId(id);
			var productFlowsOut = productFlows.Where(x => x.Quantity < 0).ToList();
			var productFlowsIn = productFlows.Where(x => x.Quantity > 0).ToList();
			string warehouse = productFlowsOut.FirstOrDefault().WarehouseName;
			string warehouseTo = productFlowsIn.FirstOrDefault(x => x.Quantity > 0).Warehouse.Name;

			ProductFlowMovementModel obj = new ProductFlowMovementModel()
			{
				Document = movement.Document,
				Warehouse = warehouse,
				WarehouseTo = warehouseTo
			};

			obj.ProductFlowModels = productFlowsIn.Select(x => new ProductFlowModel()
			{
				Id = x.Id,
				Warehouse = x.Warehouse,
				ProductId = x.ProductId,
				ProductItemCode = x.ProductItemCode,
				SupplierId = x.SupplierId,
				Supplier = x.Supplier,
				WarehouseId = x.WarehouseId,
				MovementId = movement.Id,
				Quantity = x.Quantity,
				DocumentNumber = x.DocumentNumber,
				WarehouseToIdName = warehouseTo
			}).ToList();

			return View(obj);
		}

		public async Task<IActionResult> PdfGenerate(int id)
		{
			var productFlows = await _productFlowRepo.GetProductFlowsByMoveId(id);

			MovementPdfReport report = new MovementPdfReport(productFlows);
			byte[] bytes = report.PrepareReport();

			return File(bytes, "application/pdf");
		}

		[HttpPost]
		public async Task<IActionResult> CreateWarehouseMovement(WarehouseMovementModel model)
		{
			try
			{
				await _movementRepo.Create(model);
				return Json(new { redirectToUrl = Url.Action("Index", "Movement") });
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.ToString());
				return Json(new { redirectToUrl = Url.Action("Error", "Movement") });
			}
		}
	}
}
