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
            var result = await _productFlowRepo.GetAll();
            return View(result);
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
            string warehouse = productFlowModel.Warehouse;
            string itemCode = productFlowModel.ItemCode;
            DateTime insertDate = productFlowModel.CreationDate;

            List<ProductFlowModel> prodFlowCumulative = await _productFlowRepo.GetAllCumulative(itemCode, warehouse);

            var prodFlowCumulativeLimited = prodFlowCumulative.Where(x => x.CreationDate > insertDate).ToList();

            int count = prodFlowCumulativeLimited.Count;

            int minValue;

            if (count > 0)
            {
                minValue = prodFlowCumulativeLimited.Min(x => x.Cumulative);
            }
            else
            {
                minValue = prodFlowCumulative[0].Cumulative;
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
    }
}
