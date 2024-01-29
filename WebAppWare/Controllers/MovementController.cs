using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebAppWare.Repositories.Interfaces;

namespace WebAppWare.Controllers
{
	public class MovementController : Controller
	{
		private readonly IMovementRepo _movementService;
		public MovementController(IMovementRepo movementService)
        {
			_movementService = movementService;
        }
        public async Task<IActionResult> Index()
		{
			var movements = await _movementService.GetAll();
			return View(movements);
		}

		//public async Task<ProductFlowMovementModel> SetComboBoxForMovement()
		//{
		//	List<Product> products = await _comboService.GetAllProducts();
		//	List<Supplier> suppliers = await _comboService.GetAllSuppliers();
		//	List<Warehouse> warehouses = await _comboService.GetAllWarehouses();

		//	IEnumerable<SelectListItem> productList = products.Select(x => new SelectListItem()
		//	{
		//		Text = x.ItemCode,
		//		Value = x.Id.ToString()
		//	});

		//	IEnumerable<SelectListItem> supplierList = suppliers.Select(x => new SelectListItem()
		//	{
		//		Text = x.Name,
		//		Value = x.Id.ToString()
		//	});

		//	IEnumerable<SelectListItem> warehouseList = warehouses.Select(x => new SelectListItem()
		//	{
		//		Text = x.Name,
		//		Value = x.Id.ToString()
		//	});

		//	ProductFlowMovementModel model = new ProductFlowMovementModel()
		//	{
		//		Products = productList,
		//		Suppliers = supplierList,
		//		Warehouses = warehouseList
		//	};

		//	return model;
		//}

		//public async Task<IActionResult> CreatePz()
		//{
		//	return View(await SetComboBoxForMovement());
		//}

		//[HttpPost]
		//public async Task<IActionResult> CreatePz(ProductFlowMovementModel obj)
		//{
		//	if (string.IsNullOrEmpty(obj.WarehouseMovement.Document) || obj.ProductsFlow.WarehouseId == null)
		//	{
		//		return RedirectToAction("CreatePz");
		//	}

		//	List<string> productItems = new List<string>();

		//	for (int i = 0; i < obj.ProductsFlows.Length; i++)
		//	{
		//		if (!string.IsNullOrEmpty(obj.ProductsFlows[i].ProductId.ToString()))
		//		{
		//			productItems.Add(obj.ProductsFlows[i].ProductId.ToString());
		//		}				
		//	}

		//	int? itemsBeforeDistinct = productItems.Count();
		//	int? itemsAfterDistinct = productItems.Distinct().Count();

		//	if (itemsBeforeDistinct != itemsAfterDistinct)
		//	{
		//		return RedirectToAction("CreatePz");
		//	}

		//	for (int i = 0; i < productItems.Count(); i++)
		//	{
		//		if (obj.ProductsFlows[i].Quantity <= 0)
		//		{
		//			return RedirectToAction("CreatePz");
		//		}
		//	}

		//	WarehouseMovement warMove = new WarehouseMovement()
		//	{
		//		Document = obj.WarehouseMovement.Document,
		//		MovementType = MovementType.PZ
		//	};

		//	await _comboService.CreateMovement(warMove);
		//	var lastMovement = await _comboService.GetLastMovement();

		//	int warehouse = (int)obj.ProductsFlow.WarehouseId;
		//	int movement = lastMovement.Id;

		//	for (int i = 0; i < productItems.Count(); i++)
		//	{
		//		ProductsFlow productFlowToAdd = new ProductsFlow()
		//		{
		//			Quantity = obj.ProductsFlows[i].Quantity,
		//			WarehouseMovementId = movement,
		//			WarehouseId = warehouse,
		//			ProductId = obj.ProductsFlows[i].ProductId,
		//			SupplierId = obj.ProductsFlows[i].SupplierId
		//		};

		//		await _comboService.CreateProductFlow(productFlowToAdd);
		//	}

		//	return RedirectToAction("Index");
		//}

		//public async Task<IActionResult> CreateWz()
		//{
		//	return View(await SetComboBoxForMovement());
		//}

		//[HttpPost]
		//public async Task<IActionResult> CreateWz(ProductFlowMovementModel obj)
		//{
		//	if (string.IsNullOrEmpty(obj.WarehouseMovement.Document) || obj.Warehouse.Id != null)
		//	{
		//		return RedirectToAction("CreateWz");
		//	}

		//	List<string> productItems = new List<string>();

		//	for (int i = 0; i < obj.ProductsFlows.Length; i++)
		//	{
		//		if (obj.ProductsFlows[i].ProductId != null)
		//		{
		//			productItems.Add(obj.ProductsFlows[i].ProductId.ToString());
		//		}
		//	}

		//	int? itemsBeforeDistinct = productItems.Count();
		//	int? itemsAfterDistinct = productItems.Distinct().Count();

		//	if (itemsBeforeDistinct != itemsAfterDistinct)
		//	{
		//		return RedirectToAction("CreateWz");
		//	}

		//	for (int i = 0; i < productItems.Count(); i++)
		//	{
		//		if (obj.ProductsFlows[i].Quantity <= 0)
		//		{
		//			return RedirectToAction("CreateWz");
		//		}
		//	}

		//	WarehouseMovement warMove = new WarehouseMovement()
		//	{
		//		Document = obj.WarehouseMovement.Document,
		//		MovementType = MovementType.WZ
		//	};

		//	var insertDate = DateTime.Now;
		//	string warehouse = obj.Warehouse.Name;
		//	List<ProductsFlow> productsFlowToAdd = new List<ProductsFlow>();

		//	foreach (var item in obj.ProductsFlows)
		//	{
		//		if (item.Quantity > 0 && item.ProductId != null)
		//		{
		//			var productsFlow = await _comboService.GetAllProductFlowModel(item.Product.ItemCode, obj.Warehouse.Name);
		//			productsFlow = productsFlow.Where(x => x.CreationDate > insertDate).ToList();

		//			int howManyRecords = productsFlow.Count;
		//			int minValue = (howManyRecords > 1) ? productsFlow.Min(x => x.Cumulative) : item.Quantity;

		//			if (minValue < item.Quantity)
		//			{
		//				return RedirectToAction("CreateWz");
		//			}

		//			item.WarehouseId = obj.Warehouse.Id;
		//			item.ProductId = obj.Product.Id;
		//			item.SupplierId = obj.Supplier.Id;
		//			item.Quantity = -item.Quantity;

		//			productsFlowToAdd.Add(item);
		//		}				
		//	}

		//	await _comboService.CreateMovement(warMove);

		//	var lastMovement = await _comboService.GetLastMovement();
		//	string docNo = lastMovement.Document;
		//	int lastMovementId = lastMovement.Id;

		//	foreach (var item in productsFlowToAdd)
		//	{
		//		item.WarehouseMovementId = lastMovementId;
		//	}

		//	int? wareId = productsFlowToAdd[0].Warehouse.Id;

		//	await _comboService.CreateProductFlowList(productsFlowToAdd);

		//	return RedirectToAction("Index");
		//}
	}
}
