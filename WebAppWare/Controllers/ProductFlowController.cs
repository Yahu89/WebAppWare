using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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

		[HttpGet]
        public async Task<IActionResult> Index(ProductFlowModel model)
        {
			var results = new List<ProductFlowModel>();

			try
			{
				results = (await _productFlowRepo.GetBySearch(model.SearchWarehouse, model.SearchItemCode, model.SearchSupplier))
												.ToList();
			}
			catch (Exception ex)
			{
				return Json(ex.Message.ToString());
			}

			int totalRecords = results.Count();
			int recordsPerPage = 5;
			model.TotalPages = (int)(Math.Ceiling(totalRecords / (double)recordsPerPage));

			model.ProductsFlow = results.Skip((model.CurrentPage - 1) * recordsPerPage)
										.Take(recordsPerPage);
			return View(model);
		}

		[HttpDelete]
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

        [HttpDelete]
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

		[HttpGet]
		public async Task<IActionResult> Search(ProductFlowModel model)
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
	}
}
