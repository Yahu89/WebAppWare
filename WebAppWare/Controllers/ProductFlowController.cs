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

		public async Task<IActionResult> Delete(int id)
        {
            var result = await _productFlowRepo.GetById(id);
            return View(result);
        }

        [HttpPost]
        public async Task<IActionResult> DeletePost(int id)
        {
			#region
			//         var productFlowModel = await _productFlowRepo.GetById(id);
			//         int movementId = productFlowModel.MovementId;

			//         var prodFlowMoveIdList = await _productFlowRepo.GetProductFlowsByMoveId(movementId);
			//         int howManyItems = prodFlowMoveIdList.Count;

			//         if (productFlowModel.MovementType == MovementType.WZ)
			//         {
			//             if (howManyItems == 1)
			//             {
			//		await _movementRepo.DeleteById(movementId);
			//		return RedirectToAction(nameof(Index));
			//	}
			//	else
			//             {
			//		await _productFlowRepo.DeleteById(id);
			//		return RedirectToAction(nameof(Index));
			//	}
			//         }

			//         if (productFlowModel.MovementType == MovementType.PZ)
			//         {
			//             if (howManyItems == 1)
			//             {
			//                 if (await _movementRepo.IsPossibleToDeletePzWz(movementId))
			//                 {
			//			await _movementRepo.DeleteById(movementId);
			//			return RedirectToAction(nameof(Index));
			//		}
			//                 else
			//                 {
			//                     return BadRequest();
			//                 }
			//             }
			//             else
			//             {
			//                 if (await _productFlowRepo.IsReadyToDeleteProductFlow(id))
			//                 {
			//			await _productFlowRepo.DeleteById(id);
			//			return RedirectToAction(nameof(Index));
			//		}
			//                 else
			//                 {
			//                     return BadRequest();
			//                 }
			//             }
			//         }

			//         if (productFlowModel.MovementType is MovementType.MM)
			//         {
			//             var itemCodeToDelete = productFlowModel.ProductId;
			//             var coupleOfItems = prodFlowMoveIdList.Where(x => x.ProductId == itemCodeToDelete).ToList();
			//             var itemCodeToCheck = coupleOfItems.Where(x => x.Quantity > 0).ToList();
			//             var itemCodeIdToCheck = itemCodeToCheck[0].Id;

			//             if (prodFlowMoveIdList.Count == 2)
			//             {
			//                 if (await _productFlowRepo.IsReadyToDeleteProductFlow(itemCodeIdToCheck))
			//                 {
			//			await _movementRepo.DeleteById(movementId);
			//			return RedirectToAction(nameof(Index));
			//		}
			//                 else
			//                 {
			//                     return BadRequest();
			//                 }
			//             }
			//             else
			//             {
			//                 if (await _productFlowRepo.IsReadyToDeleteProductFlow(itemCodeIdToCheck))
			//                 {
			//                     await _productFlowRepo.DeleteRange(coupleOfItems);
			//			return RedirectToAction(nameof(Index));
			//		}
			//                 else
			//                 {
			//                     return BadRequest();
			//                 }
			//             }
			//         }

			//return BadRequest();
			#endregion

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

   //     [HttpPost]
   //     public async Task<IActionResult> DeleteMmPost(int id)
   //     {
			//var productFlow = await _productFlowRepo.GetById(id);
			//int moveId = productFlow.MovementId;
			//int productId = (int)productFlow.ProductId;
   //         DateTime whenInserted = productFlow.CreationDate;

			//var productFlowsByMoveId = await _productFlowRepo.GetProductFlowsByMoveId(moveId);
			//var pairProductFlows = productFlowsByMoveId.Where(x => x.ProductId == productId).ToList();

   //         int warehouseIdToCheck = (int)pairProductFlows[1].WarehouseId;
   //         int qtyToCheck = pairProductFlows[1].Quantity;

   //         var prodFlowsCumulative = await _productFlowRepo.GetAllCumulative(productId, warehouseIdToCheck);
   //         var prodFlowsCumLimited = prodFlowsCumulative.Where(x => x.CreationDate > whenInserted).ToList();

   //         int minValue = prodFlowsCumulative.FirstOrDefault(x => x.CreationDate == whenInserted).Cumulative;

   //         if (prodFlowsCumLimited.Count > 0)
   //         {
   //             minValue = prodFlowsCumLimited.Min(x => x.Cumulative);
   //         }

   //         if (minValue < qtyToCheck)
   //         {
   //             return BadRequest();
   //         }

   //         if (productFlowsByMoveId.Count > 2)
   //         {
   //             await _productFlowRepo.DeleteRange(pairProductFlows);
   //             return RedirectToAction(nameof(Index));
			//}
   //         else if (productFlowsByMoveId.Count == 2)
   //         {
   //             await _movementRepo.DeleteById(moveId);
   //             return RedirectToAction(nameof(Index));
   //         }

			//return RedirectToAction(nameof(Index));
   //     }

        //[HttpPost]
        public async Task<IActionResult> Search(ProductFlowSearchModel model)
        {        
            var results = await _productFlowRepo.GetBySearch(model.Warehouse, model.ItemCode, model.Supplier);
            int totalRecords = results.Count();
            int recordsPerPage = 5;
            model.TotalPages = (int)(Math.Ceiling(totalRecords / (double)recordsPerPage));

            model.ProductsFlow = results.Skip((model.CurrentPage - 1) * recordsPerPage).Take(recordsPerPage);
            return View(model);
        }
    }
}
