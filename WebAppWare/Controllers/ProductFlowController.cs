using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebAppWare.Database.Entities;
using WebAppWare.Models;
using WebAppWare.Repositories.Interfaces;

namespace WebAppWare.Controllers
{
	[Authorize(Roles = "admin,warehouse")]
    public class ProductFlowController : Controller
    {
        private readonly IProductFlowRepo _productFlowRepo;
		private readonly IMovementRepo _movementRepo;
		public ProductFlowController(IProductFlowRepo productFlowRepo, IMovementRepo movementRepo)
        {
            _productFlowRepo = productFlowRepo;
            _movementRepo = movementRepo;
        }
        public async Task<IActionResult> Index(ProductFlowModel model)
        {
			IEnumerable<ProductFlowModel> results = new List<ProductFlowModel>();

			try
			{
				results = await _productFlowRepo.GetBySearch(model.SearchWarehouse, model.SearchItemCode, model.SearchSupplier);
			}
			catch (Exception ex)
			{
				return Json(ex.Message.ToString());
			}

			int totalRecords = results.Count();
			int recordsPerPage = 5;
			model.TotalPages = (int)(Math.Ceiling(totalRecords / (double)recordsPerPage));

			model.ProductsFlow = results.Skip((model.CurrentPage - 1) * recordsPerPage).Take(recordsPerPage);
			return View(model);
		}

		[HttpGet]
		public async Task<IActionResult> Delete(int id)
        {
			try
			{
				var result = await _productFlowRepo.GetById(id);
				return View(result);
			}
			catch (Exception ex)
			{
				return Json(ex.ToString());
			}  
        }

        [HttpPost]
        public async Task<IActionResult> DeletePost(int id)
        {
			if (await _productFlowRepo.IsReadyToDeleteItemRecordsForAllMoveTypes(id))
			{
				return RedirectToAction(nameof(Index));
			}
			else
			{
				return BadRequest();
			}
		}

		public async Task<IActionResult> DeleteMmM(int id)
        {
			ProductFlowModel productFlow = new ProductFlowModel();
			int moveId;

			try
			{
				productFlow = await _productFlowRepo.GetById(id);
				moveId = productFlow.MovementId;
			}
			catch(Exception ex)
			{
				return Json(ex.ToString());
			}
         
            int? productId = productFlow.ProductId;
            var movement = await _movementRepo.GetById(moveId);
            string docNumber = movement.Document;

            var productFlowsByMoveId = await _productFlowRepo.GetProductFlowsByMoveId(moveId);
            var pairProductFlows = productFlowsByMoveId.Where(x => x.ProductId == productId).ToList();
            pairProductFlows.ForEach(x => x.DocumentNumber = docNumber);

            return View(pairProductFlows);
        }

   //     public async Task<IActionResult> Search(ProductFlowModel model)
   //     {     
			//IEnumerable<ProductFlowModel> results = new List<ProductFlowModel>();

			//try
			//{
			//	results = await _productFlowRepo.GetBySearch(model.SearchWarehouse, model.SearchItemCode, model.SearchSupplier);
			//}
			//catch (Exception ex)
			//{
			//	return Json(ex.Message.ToString());
			//}

   //         int totalRecords = results.Count();
   //         int recordsPerPage = 5;
   //         model.TotalPages = (int)(Math.Ceiling(totalRecords / (double)recordsPerPage));

   //         model.ProductsFlow = results.Skip((model.CurrentPage - 1) * recordsPerPage).Take(recordsPerPage);
   //         return View(model);
   //     }
    }
}
