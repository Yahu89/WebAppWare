using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Primitives;
using System.Collections;
using WebAppWare.Database.Entities;
using WebAppWare.Models;
using WebAppWare.Repositories.Interfaces;

namespace WebAppWare.Controllers
{
	public class MovementController : Controller
	{
		private readonly IMovementRepo _movementRepo;
		private readonly IProductRepo _productRepo;
		private readonly IWarehouseRepo _warehouseRepo;
		private readonly ISupplierRepo _supplierRepo;
		private readonly IProductFlowRepo _productFlowRepo;
		public MovementController(
			IMovementRepo movementRepo,
			IProductRepo productRepo,
			IWarehouseRepo warehouseRepo,
			ISupplierRepo supplierRepo,
			IProductFlowRepo productFlowRepo
			)
		{
			_movementRepo = movementRepo;
			_productRepo = productRepo;
			_warehouseRepo = warehouseRepo;
			_supplierRepo = supplierRepo;
			_productFlowRepo = productFlowRepo;
		}
		public async Task<IActionResult> Index()
		{
			var movements = await _movementRepo.GetAll();
			return View(movements);
		}

		public async Task<ProductFlowMovementModel> SetComboBoxForMovement(MovementType moveType)
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

		[HttpPost]
		public async Task<IActionResult> CreatePz(ProductFlowMovementModel obj)
		{
			if (string.IsNullOrEmpty(obj.Document) || obj.WarehouseId == 0)
				return RedirectToAction(nameof(CreatePz));

			if (!await _movementRepo.IsDocumentNameUnique(obj.Document))
				return RedirectToAction(nameof(CreatePz));

			//var itemCodes = obj.ProductFlowModels
			//	.Where(x => x != null && x.ProductId != null && x.SupplierId != null)
			//	.Select(x => new ProductFlowModel()
			//	{
			//		ProductId = x.ProductId,
			//		ProductItemCode = x.ProductItemCode,
			//		Supplier = x.Supplier,
			//		SupplierId = x.SupplierId,
			//		Warehouse = x.Warehouse,
			//		Quantity = x.Quantity,
			//		WarehouseId = obj.WarehouseId,
			//		MovementType = x.MovementType,
			//	})
			//	.ToList();

			var itemCodes = _movementRepo.GetProductFlowsFromForm(obj);



			//if (itemCodes
			//		.GroupBy(x => x.ProductId)
			//		.Any(x => x.Count() > 1))
			//	return RedirectToAction(nameof(CreatePz));

			//if (itemCodes
			//		.Any(x => x.Quantity <= 0))
			//	return RedirectToAction(nameof(CreatePz));

			if (_movementRepo.IsUniqueAndQtyCorrectForPzWz(itemCodes))
			{
				MovementModel warMove = new MovementModel()
				{
					Document = obj.Document,
					MovementType = MovementType.PZ
				};

				var moveId = await _movementRepo.Create(warMove);

				itemCodes.ForEach(x =>
				{
					x.MovementId = moveId;
					//x.Quantity = -x.Quantity;
				});

				await _productFlowRepo.CreateRange(itemCodes, moveId);

				return RedirectToAction(nameof(Index));
			}

			return RedirectToAction(nameof(BadRequest));

			//var warMove = new MovementModel()
			//{
			//	Document = obj.Document,
			//	MovementType = MovementType.PZ
			//};

			//var moveId = await _movementRepo.Create(warMove);

			//await _productFlowRepo.CreateRange(itemCodes, moveId);

			//return RedirectToAction(nameof(Index));
		}

		public async Task<IActionResult> CreateWz()
		{
			return View(await SetComboBoxForMovement(MovementType.WZ));
		}

		[HttpPost]
		public async Task<IActionResult> CreateWz(ProductFlowMovementModel obj)
		{
			if (string.IsNullOrEmpty(obj.Document) || obj.WarehouseId == 0)
				return RedirectToAction(nameof(CreateWz));

			if (!await _movementRepo.IsDocumentNameUnique(obj.Document))
				return RedirectToAction(nameof(CreateWz));

			var itemCodes = _movementRepo.GetProductFlowsFromForm(obj);

			if (_movementRepo.IsUniqueAndQtyCorrectForPzWz(itemCodes))
			{
				MovementModel warMove = new MovementModel()
				{
					Document = obj.Document,
					MovementType = MovementType.WZ
				};

				var moveId = await _movementRepo.Create(warMove);

				itemCodes.ForEach(x =>
				{
					x.MovementId = moveId;
					x.Quantity = -x.Quantity;
				});

				await _productFlowRepo.CreateRange(itemCodes, moveId);

				return RedirectToAction(nameof(Index));
			}

			return RedirectToAction(nameof(BadRequest));
		}

		public async Task<IActionResult> CreateMm()
		{
			MovementType mm = MovementType.MM;
			return View(await SetComboBoxForMovement(MovementType.MM));
		}

		[HttpPost]
		public async Task<IActionResult> CreateMm(ProductFlowMovementModel obj)
		{
			if (string.IsNullOrEmpty(obj.Document) || obj.WarehouseId == 0 || obj.WarehouseToId == 0)
				return RedirectToAction(nameof(CreateMm));

			if (!await _movementRepo.IsDocumentNameUnique(obj.Document))
				return RedirectToAction(nameof(CreateMm));

			if (obj.WarehouseId == obj.WarehouseToId)
			{
				return RedirectToAction(nameof(CreateMm));
			}

			var itemCodes = obj.ProductFlowModels
									.Where(x => x != null && x.ProductId != null)
									.Select(x => new ProductFlowModel()
									{
										ProductId = x.ProductId,
										ProductItemCode = x.ProductItemCode,
										Warehouse = x.Warehouse,
										Quantity = x.Quantity,
										WarehouseId = obj.WarehouseId,
										MovementType = x.MovementType,
									})
									.ToList();

			if (itemCodes
					.GroupBy(x => x.ProductId)
					.Any(x => x.Count() > 1))
				return RedirectToAction(nameof(CreateMm));

			if (itemCodes
					.Any(x => x.Quantity <= 0))
				return RedirectToAction(nameof(CreateMm));

			MovementModel warMove = new MovementModel()
			{
				Document = obj.Document,
				MovementType = MovementType.MM
			};

			foreach (var item in itemCodes)
			{
				var cumulativeValueList = await _productFlowRepo.GetAllCumulative((int)item.ProductId, (int)item.WarehouseId);
				int actualQty = cumulativeValueList.Last().Cumulative;

				if (actualQty < item.Quantity)
				{
					return RedirectToAction(nameof(CreateMm));
				}
			}

			var moveId = await _movementRepo.Create(warMove);

			itemCodes.ForEach(x =>
			{
				x.MovementId = moveId;
				x.Quantity = -x.Quantity;
			});

			int am = itemCodes[0].Quantity;

			await _productFlowRepo.CreateRange(itemCodes, moveId);

			itemCodes.ForEach(x =>
			{
				x.Quantity = -x.Quantity;
				x.WarehouseId = obj.WarehouseToId;
			});

			await _productFlowRepo.CreateRange(itemCodes, moveId);

			return RedirectToAction("Index");
		}

		public async Task<IActionResult> Delete(int id)
		{
			var movement = await _movementRepo.GetById(id);
			var productFlows = await _productFlowRepo.GetProductFlowsByMoveId(id);
			string warehouse = productFlows.FirstOrDefault(x => x.MovementId == id).Warehouse;

			ProductFlowMovementModel obj = new ProductFlowMovementModel()
			{
				Document = movement.Document,
				Warehouse = warehouse
			};

			//for (int i = 0; i < productFlows.Count; i++)
			//{
			//	obj.ProductFlowModels[i] = productFlows[i];
			//}

			obj.ProductFlowModels = productFlows.Select(x => new ProductFlowModel()
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
				DocumentNumber = x.DocumentNumber

			}).ToList();

			return View(obj);
		}

		[HttpPost]
		public async Task<IActionResult> DeletePost(int id)
		{
			var movement = await _movementRepo.GetById(id);

			if (movement.MovementType == MovementType.WZ)
			{
				await _movementRepo.DeleteById(id);
				return RedirectToAction(nameof(Index));
			}

			if (await _movementRepo.IsPossibleToDeletePzWz(id))
			{
				await _movementRepo.DeleteById(id);
				return RedirectToAction(nameof(Index));
			}

			//var productFlowsWithinMove = await _productFlowRepo.GetProductFlowsByMoveId(id);
			//int wareId = (int)productFlowsWithinMove.FirstOrDefault().WarehouseId;
			//DateTime insertDate = movement.CreationDate;

			//foreach (var item in productFlowsWithinMove)
			//{
			//	int qty = item.Quantity;
			//	var prodFlows = await _productFlowRepo.GetAllCumulative((int)item.ProductId, wareId);
			//	var prodFlowsLimited = prodFlows.Where(x => x.CreationDate > insertDate).ToList();

			//	int counter = prodFlowsLimited.Count;
			//	int minValue;

			//	if (counter == 0)
			//	{
			//		minValue = item.Cumulative;
			//	}
			//	else
			//	{
			//		minValue = prodFlowsLimited.Min(x => x.Cumulative);
			//	}

			//	if (minValue < qty)
			//	{
			//		return BadRequest();
			//	}
			//}

			//await _movementRepo.DeleteById(id);

			return BadRequest();
		}

		public async Task<IActionResult> DeleteMmM(int id)
		{
			var movement = await _movementRepo.GetById(id);
			var productFlows = await _productFlowRepo.GetProductFlowsByMoveId(id);
			var productFlowsOut = productFlows.Where(x => x.Quantity < 0).ToList();
			var productFlowsIn = productFlows.Where(x => x.Quantity > 0).ToList();
			string warehouse = productFlowsOut.FirstOrDefault().Warehouse;
			string warehouseTo = productFlows.Where(x => x.Quantity > 0).FirstOrDefault().Warehouse;

			ProductFlowMovementModel obj = new ProductFlowMovementModel()
			{
				Document = movement.Document,
				Warehouse = warehouse,
				WarehouseTo = warehouseTo
			};

			//for (int i = 0; i < productFlows.Count; i++)
			//{
			//	obj.ProductFlowModels[i] = productFlows[i];
			//}

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
			}).ToList();

			return View(obj);
		}

		public async Task<IActionResult> PdfGenerate(int id)
		{
			//var movement = _movementRepo.GetById(id);
			var productFlows = await _productFlowRepo.GetProductFlowsByMoveId(id);

			MovementPdfReport report = new MovementPdfReport();
			byte[] bytes = report.PrepareReport(productFlows);

			return File(bytes, "application/pdf");
		}

		[HttpPost]
		public async Task<IActionResult> TestPz(IFormCollection collection)
		{
			var model = _movementRepo.FromCollectionToMovementModel(collection, MovementType.PZ);
			var list = _productFlowRepo.FromCollectionToProductFlowModel(collection, 0);

			if (await _movementRepo.IsDocumentNameUnique(model.Document))
			{
				if (_movementRepo.IsUniqueAndQtyCorrectForPzWz(list))
				{
					int moveId = await _movementRepo.Create(model);					

					await _productFlowRepo.CreateRange(list, moveId);

					return RedirectToAction(nameof(Index));
				}
			}

			return BadRequest();
		}

		[HttpPost]
		public async Task<IActionResult> TestWz(IFormCollection collection)
		{
			var model = _movementRepo.FromCollectionToMovementModel(collection, MovementType.WZ);
			var list = _productFlowRepo.FromCollectionToProductFlowModel(collection, 0);

			if (await _movementRepo.IsDocumentNameUnique(model.Document))
			{
				if (_movementRepo.IsUniqueAndQtyCorrectForPzWz(list))
				{
					if (await _movementRepo.IsPossibleToCreateWz(collection))
					{
						int moveId = await _movementRepo.Create(model);

						list.ForEach(x =>
						{
							x.MovementId = moveId;
							x.Quantity = -x.Quantity;
						});

						await _productFlowRepo.CreateRange(list, moveId);

						return RedirectToAction(nameof(Index));
					}
				}
			}

			return BadRequest();
		}



	}
}
