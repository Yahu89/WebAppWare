using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
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
		public MovementController(IMovementRepo movementRepo, IProductRepo productRepo, 
			IWarehouseRepo warehouseRepo, ISupplierRepo supplierRepo, IProductFlowRepo productFlowRepo)
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

		public async Task<ProductFlowMovementModel> SetComboBoxForMovement()
		{
			List<ProductModel> products = await _productRepo.GetAll();
			List<SupplierModel> suppliers = await _supplierRepo.GetAll();
			List<WarehouseModel> warehouses = await _warehouseRepo.GetAll();

			IEnumerable<SelectListItem> productList = products.Select(x => new SelectListItem()
			{
				Text = x.ItemCode,
				Value = x.ItemCode
			});

			IEnumerable<SelectListItem> supplierList = suppliers.Select(x => new SelectListItem()
			{
				Text = x.Name,
				Value = x.Name
			});

			IEnumerable<SelectListItem> warehouseList = warehouses.Select(x => new SelectListItem()
			{
				Text = x.Name,
				Value = x.Name
			});

			ProductFlowMovementModel model = new ProductFlowMovementModel()
			{
				Products = productList,
				Suppliers = supplierList,
				Warehouses = warehouseList
			};

			return model;
		}

		public async Task<IActionResult> CreatePz()
		{
			return View(await SetComboBoxForMovement());
		}

		[HttpPost]
		public async Task<IActionResult> CreatePz(ProductFlowMovementModel obj)
		{
			if (string.IsNullOrEmpty(obj.Document) || string.IsNullOrEmpty(obj.Warehouse))
			{
				return RedirectToAction("CreatePz");
			}
			else if (!await _movementRepo.IsDocumentNameUnique(obj.Document))
			{
				return RedirectToAction("CreatePz");
			}

			List<string> productItems = new List<string>();

			for (int i = 0; i < obj.ProductFlowModels.Length; i++)
			{
				if (obj.ProductFlowModels[i] != null && obj.ProductFlowModels[i].ItemCode != null && obj.ProductFlowModels[i].Supplier != null)
				{
					productItems.Add(obj.ProductFlowModels[i].ItemCode);
				}
			}

			int? itemsBeforeDistinct = productItems.Count();
			int? itemsAfterDistinct = productItems.Distinct().Count();

			if (itemsBeforeDistinct != itemsAfterDistinct)
			{
				return RedirectToAction("CreatePz");
			}

			for (int i = 0; i < productItems.Count(); i++)
			{
				if (obj.ProductFlowModels[i].Quantity <= 0)
				{
					return RedirectToAction("CreatePz");
				}
			}

			MovementModel warMove = new MovementModel()
			{
				Document = obj.Document,
				MovementType = MovementType.PZ
			};

			await _movementRepo.Create(warMove);
			var lastMovement = await _movementRepo.GetLastMovement();

			int movement = lastMovement.Id;

			List<ProductsFlow> products = new List<ProductsFlow>();

			int zm = productItems.Count;

			for (int i = 0; i < productItems.Count(); i++)
			{
				products.Add(new ProductsFlow()
				{
					Quantity = obj.ProductFlowModels[i].Quantity,
					WarehouseMovementId = movement,
					WarehouseId = await _warehouseRepo.GetWarehouseIdByName(obj.Warehouse),
					ProductId = await _productRepo.GetProductIdByCode(obj.ProductFlowModels[i].ItemCode),
					SupplierId = await _supplierRepo.GetSupplierIdByName(obj.ProductFlowModels[i].Supplier)
				});				
			}

			var zm2 = products;

			await _productFlowRepo.CreateRange(products);

			return RedirectToAction("Index");
		}

		public async Task<IActionResult> CreateWz()
		{
			return View(await SetComboBoxForMovement());
		}

		[HttpPost]
		public async Task<IActionResult> CreateWz(ProductFlowMovementModel obj)
		{
			if (string.IsNullOrEmpty(obj.Document) || string.IsNullOrEmpty(obj.Warehouse))
			{
				return RedirectToAction("CreateWz");
			}
			else if (!await _movementRepo.IsDocumentNameUnique(obj.Document))
			{
				return RedirectToAction("CreateWz");
			}

			List<string> productItems = new List<string>();

			for (int i = 0; i < obj.ProductFlowModels.Length; i++)
			{
				if (obj.ProductFlowModels[i] != null && obj.ProductFlowModels[i].ItemCode != null && obj.ProductFlowModels[i].Supplier != null)
				{
					productItems.Add(obj.ProductFlowModels[i].ItemCode);
				}
			}

			int? itemsBeforeDistinct = productItems.Count();
			int? itemsAfterDistinct = productItems.Distinct().Count();

			if (itemsBeforeDistinct != itemsAfterDistinct)
			{
				return RedirectToAction("CreateWz");
			}

			for (int i = 0; i < productItems.Count(); i++)
			{
				if (obj.ProductFlowModels[i].Quantity <= 0)
				{
					return RedirectToAction("CreateWz");
				}
			}

			MovementModel warMove = new MovementModel()
			{
				Document = obj.Document,
				MovementType = MovementType.WZ
			};

			var insertDate = DateTime.Now;
			string warehouse = obj.Warehouse;

			List<ProductsFlow> productsFlowToAdd = new List<ProductsFlow>();

			for (int i = 0; i < productItems.Count; i++)
			{
				string itemCode = obj.ProductFlowModels[i].ItemCode;
				int qty = obj.ProductFlowModels[i].Quantity;
				string supplierName = obj.ProductFlowModels[i].Supplier;

				if (qty > 0 && itemCode != null)
				{
					List<ProductFlowModel> cumList = await _productFlowRepo.GetAllCumulative(itemCode, warehouse);

					int actualAmount = cumList.Last().Cumulative;

					if (actualAmount >= qty)
					{
						productsFlowToAdd.Add(new ProductsFlow()
						{
							ProductId = await _productRepo.GetProductIdByCode(itemCode),
							Quantity = -qty,
							WarehouseId = await _warehouseRepo.GetWarehouseIdByName(warehouse),
							SupplierId = await _supplierRepo.GetSupplierIdByName(supplierName)
						});
					}
					else
					{
						return RedirectToAction("CreateWz");
					}
				}
			}

			await _movementRepo.Create(warMove);

			var lastMovement = await _movementRepo.GetLastMovement();
			int lastMovementId = lastMovement.Id;

			foreach (var item in productsFlowToAdd)
			{
				item.WarehouseMovementId = lastMovementId;
			}

			await _productFlowRepo.CreateRange(productsFlowToAdd);

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

			for (int i = 0; i < productFlows.Count; i++)
			{
				obj.ProductFlowModels[i] = productFlows[i];
			}

			return View(obj);
		}

		public async Task<IActionResult> DeletePost(int id)
		{
			var movement = await _movementRepo.GetById(id);
			
			if (movement.MovementType == MovementType.WZ)
			{
				await _movementRepo.DeleteById(id);
				return RedirectToAction("Index");
			}

			var productFlowsWithinMove = await _productFlowRepo.GetProductFlowsByMoveId(id);
			string warehouse = productFlowsWithinMove.FirstOrDefault().Warehouse;
			DateTime insertDate = movement.CreationDate;

			foreach (var item in productFlowsWithinMove)
			{
				string itemCode = item.ItemCode;
				int qty = item.Quantity;
				var prodFlows = await _productFlowRepo.GetAllCumulative(itemCode, warehouse);
				var prodFlowsLimited = prodFlows.Where(x => x.CreationDate > insertDate).ToList();

				int counter = prodFlowsLimited.Count;
				int minValue = 0;

				if (counter == 0)
				{
					minValue = item.Cumulative;
				}
				else
				{
					minValue = prodFlowsLimited.Min(x => x.Cumulative);
				}

				if (minValue < qty)
				{
					return BadRequest();
				}
			}

			await _movementRepo.DeleteById(id);

			return RedirectToAction("Index");
		}
	}
}
