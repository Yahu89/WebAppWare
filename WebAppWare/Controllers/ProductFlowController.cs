using Microsoft.AspNetCore.Mvc;
using WebAppWare.Database.Entities;
using WebAppWare.Models;
using WebAppWare.Repositories.Interfaces;

namespace WebAppWare.Controllers
{
    public class ProductFlowController : Controller
    {
        private readonly IProductFlowRepo _productFlowRepo;
		private readonly IMovementRepo _movementRepo;
		public ProductFlowController(IProductFlowRepo productFlowRepo, IMovementRepo movementRepo)
        {
            _productFlowRepo = productFlowRepo;
            _movementRepo = movementRepo;
        }
        public async Task<IActionResult> Index()
        {
            var allProductFlows = await _productFlowRepo.GetAll();
            ProductFlowSearchModel model = new ProductFlowSearchModel();
            model.ProductsFlow = allProductFlows;
            return View(model);
        }

        [HttpPost]
		public async Task<IActionResult> Paging(PaginationResult obj)
		{
			int resultPerPage = obj.ResultsPerPage;
			var allProductFlows = await _productFlowRepo.GetAll();

			int recordsQty = allProductFlows.Count();
            int pageNumber = obj.CurrentPageNumber;

			decimal operationResult = ((decimal)recordsQty / resultPerPage);
            int howManyPagesTotal = (int)Math.Ceiling(operationResult);

			var takeResults = allProductFlows.Take(resultPerPage).ToList();

			PaginationResult paginationResult = new PaginationResult();
            paginationResult.ProductFlows = takeResults;
            paginationResult.PagesQuantity = howManyPagesTotal;

			return View(paginationResult);
		}

		public async Task<IActionResult> Delete(int id)
        {
            var result = await _productFlowRepo.GetById(id);
            return View(result);
        }

        [HttpPost]
        public async Task<IActionResult> DeletePost(int id)
        {
            var productFlowModel = await _productFlowRepo.GetById(id);
            int movementId = productFlowModel.MovementId;

            var prodFlowMoveIdList = await _productFlowRepo.GetProductFlowsByMoveId(movementId);
            int howManyItems = prodFlowMoveIdList.Count;

            if (howManyItems == 1 && productFlowModel.MovementType == MovementType.WZ)
            {
                await _movementRepo.DeleteById(movementId);
                return RedirectToAction("Index");
            }
            
            if (productFlowModel.MovementType == MovementType.WZ)
            {
                await _productFlowRepo.DeleteById(id);
                return RedirectToAction("Index");
            }

            int qty = productFlowModel.Quantity;
            DateTime insertDate = productFlowModel.CreationDate;

            IEnumerable<ProductFlowModel> prodFlowCumulative = await _productFlowRepo.GetAllCumulative((int)productFlowModel.ProductId, 
                (int)productFlowModel.WarehouseId);

            var prodFlowCumulativeLimited = prodFlowCumulative.Where(x => x.CreationDate > insertDate).ToList();

            int count = prodFlowCumulativeLimited.Count;

            int minValue = 0;

            if (count > 0)
            {
                minValue = prodFlowCumulativeLimited.Min(x => x.Cumulative);
            }
            else
            {
                var list = (List<ProductFlowModel>)prodFlowCumulative;
				minValue = list[0].Cumulative;
            }

            if (minValue >= qty)
            {
                if (howManyItems == 1)
                {
                    await _movementRepo.DeleteById(id);
                    return RedirectToAction("Index");
                }

                await _productFlowRepo.DeleteById(id);
                return RedirectToAction("Index");
            }

            return BadRequest();
        }

        public async Task<IActionResult> DeleteMm(int id)
        {
            var productFlow = await _productFlowRepo.GetById(id);
            int moveId = productFlow.MovementId;
            int? productId = productFlow.ProductId;
            var movement = await _movementRepo.GetById(moveId);
            string docNumber = movement.Document;        

            var productFlowsByMoveId = await _productFlowRepo.GetProductFlowsByMoveId(moveId);
            var pairProductFlows = productFlowsByMoveId.Where(x => x.ProductId == productId).ToList();
            pairProductFlows.ForEach(x => x.DocumentNumber = docNumber);

            return View(pairProductFlows);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteMmPost(int id)
        {
			var productFlow = await _productFlowRepo.GetById(id);
			int moveId = productFlow.MovementId;
			int productId = (int)productFlow.ProductId;
            DateTime whenInserted = productFlow.CreationDate;

			var productFlowsByMoveId = await _productFlowRepo.GetProductFlowsByMoveId(moveId);
			var pairProductFlows = productFlowsByMoveId.Where(x => x.ProductId == productId).ToList();

            int warehouseIdToCheck = (int)pairProductFlows[1].WarehouseId;
            int qtyToCheck = pairProductFlows[1].Quantity;

            var prodFlowsCumulative = await _productFlowRepo.GetAllCumulative(productId, warehouseIdToCheck);
            var prodFlowsCumLimited = prodFlowsCumulative.Where(x => x.CreationDate > whenInserted).ToList();

            int minValue = prodFlowsCumulative.FirstOrDefault(x => x.CreationDate == whenInserted).Cumulative;

            if (prodFlowsCumLimited.Count > 0)
            {
                minValue = prodFlowsCumLimited.Min(x => x.Cumulative);
            }

            if (minValue < qtyToCheck)
            {
                return BadRequest();
            }

            if (productFlowsByMoveId.Count > 2)
            {
                await _productFlowRepo.DeleteRange(pairProductFlows);
                return RedirectToAction(nameof(Index));
			}
            else if (productFlowsByMoveId.Count == 2)
            {
                await _movementRepo.DeleteById(moveId);
                return RedirectToAction(nameof(Index));
            }

			return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> Search(ProductFlowSearchModel model)
        {
            var results = await _productFlowRepo.GetBySearch(model.Warehouse, model.ItemCode, model.Supplier);
            model.ProductsFlow = results;
            return View(model);
        }
    }
}
