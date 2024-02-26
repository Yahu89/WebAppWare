using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebAppWare.Models;
using WebAppWare.Repositories;
using WebAppWare.Repositories.Interfaces;

namespace WebAppWare.Controllers
{
    public class OrderController : Controller
    {
		private readonly IOrderRepo _orderRepo;
		private readonly ISupplierRepo _supplierRepo;
		private readonly IProductRepo _productRepo;
		private readonly IOrderDetailsRepo _orderDetailsRepo;
		public OrderController(IOrderRepo orderRepo, 
                                ISupplierRepo supplierRepo, 
                                IProductRepo productRepo,
                                IOrderDetailsRepo orderDetailsRepo)
        {
            _orderRepo = orderRepo;
            _supplierRepo = supplierRepo;
            _productRepo = productRepo;
            _orderDetailsRepo = orderDetailsRepo;
        }

        public async Task<IActionResult> Index()
        {
            var result = await _orderRepo.GetAll();
            return View(result);
        }

        public async Task<OrderDetailsModelView> SetComboboxValue()
        {
			OrderDetailsModelView comboboxes = new OrderDetailsModelView()
			{
				Products = (await _productRepo.GetAll()).Select(x => new SelectListItem()
				{
					Text = x.ItemCode,
					Value = x.Id.ToString()
				}),

				Suppliers = (await _supplierRepo.GetAll()).Select(x => new SelectListItem()
				{
					Text = x.Name,
					Value = x.Id.ToString()
				}),

                StatusList = new List<SelectListItem>()
                {
                    new SelectListItem() { Text = "W przygotowaniu", Value = "W przygotowaniu" },
					new SelectListItem() { Text = "Wysłano", Value = "Wysłano" },
					new SelectListItem() { Text = "Zrealizowano", Value = "Zrealizowano" },
					new SelectListItem() { Text = "Anulowano", Value = "Anulowano" }
				}
			};

            return comboboxes;
		}

        public async Task<IActionResult> Create()
        {
            var comboboxes = await SetComboboxValue();
            comboboxes.Document = await _orderRepo.SetOrderNumber(DateTime.Today);

			return View(comboboxes);
        }

        [HttpPost]
        public async Task<IActionResult> ExecuteCreation(OrderDetailsModelView obj)
        {
            OrderModel orderModel = new OrderModel()
            {
                Document = obj.Document,
                SupplierId = obj.SupplierId,
                Status = obj.Status,
                CreationDate = obj.CreationDate,
                Remarks = obj.Remarks
            };

            if (_orderRepo.IsOrderReadyToInsert(orderModel))
            {
                if (_orderDetailsRepo.IsDataCorrect(obj))
                {
                    int id = (await _orderRepo.Create(orderModel));

					var eachFieldFulfilled = obj.OrderDetails.Where(x => x.ProductId != 0
															&& x.Quantity > 0).ToList();

					eachFieldFulfilled.ForEach(x => x.OrderId = id);
                    await _orderDetailsRepo.CreateRange(eachFieldFulfilled);

					return RedirectToAction(nameof(Index));
				}               
			}

            return BadRequest();
        }

        public async Task<IActionResult> Remove(int id)
        {
            var order = await _orderRepo.GetById(id);
            var details = await _orderDetailsRepo.GetByOrderId(id);

            OrderDetailsModelView modelView = new OrderDetailsModelView()
            {
                OrderId = id,
                Document = order.Document,
                SupplierName = order.SupplierName,
                Status = order.Status,
                Remarks = order.Remarks,
                CreationDate = order.CreationDate,
                OrderDetails = details.Select(x => new OrderDetailsModel()
                {
                    ProductItemCode = x.ProductItemCode,
                    Quantity = x.Quantity
                })
                .ToList()          
            };

            return View(modelView);
        }

        [HttpPost]
		public async Task<IActionResult> ExecuteRemoving(int id)
		{
			await _orderRepo.DeleteById(id);
			return RedirectToAction(nameof(Index));
		}

        public async Task<IActionResult> Edit(int id)
        {
			var order = await _orderRepo.GetById(id);
			var details = await _orderDetailsRepo.GetByOrderId(id);

            var comboboxes = await SetComboboxValue();

			OrderDetailsModelView modelView = new OrderDetailsModelView()
			{
                OrderId = id,
				Document = order.Document,
				SupplierName = order.SupplierName,
				Status = order.Status,
				Remarks = order.Remarks,
                Products = comboboxes.Products,
                Suppliers = comboboxes.Suppliers,
                CreationDate= order.CreationDate,
                StatusList = comboboxes.StatusList,
				OrderDetails = details.Select(x => new OrderDetailsModel()
				{
					ProductItemCode = x.ProductItemCode,
					Quantity = x.Quantity,
                    Id = x.OrderId
				})
				.ToList()
			};

			return View(modelView);
		}

        [HttpPost]
        public async Task<IActionResult> ExecuteEditing(OrderDetailsModelView obj)
        {
			OrderModel orderModel = new OrderModel()
			{
				Document = obj.Document,
				SupplierId = obj.SupplierId,
				Status = obj.Status,
                CreationDate = obj.CreationDate,
                Id = obj.OrderId,
			};

            var details = await _orderDetailsRepo.GetByOrderId(obj.OrderId);

			if (_orderRepo.IsOrderReadyToInsert(orderModel))
            {
                if (_orderDetailsRepo.IsDataCorrect(obj))
                {
                    obj.OrderId = details[0].OrderId;

					for (int i = 0; i < obj.OrderDetails.Count(); i++)
					{
						obj.OrderDetails[i].Id = details[i].Id;
                        obj.OrderDetails[i].OrderId = details[i].OrderId;
					}

					await _orderRepo.Edit(orderModel);
                    await _orderDetailsRepo.EditRange(obj.OrderDetails);

					return RedirectToAction(nameof(Index));
				}
            }

            return BadRequest();
        }

        public async Task<IActionResult> PdfGenerate(int id)
        {
            var order = await _orderRepo.GetById(id);
            var orderDetails = await _orderDetailsRepo.GetByOrderId(id);
            var orderDetailsModelView = new OrderDetailsModelView()
            {
                OrderDetails = orderDetails,
                Document = order.Document,
                SupplierName = order.SupplierName,
                Status = order.Status,
                Remarks = order.Remarks,
                CreationDate = order.CreationDate
            };
           		

			OrderPdfReport report = new OrderPdfReport();
			byte[] bytes = report.PrepareReport(orderDetailsModelView);

			return File(bytes, "application/pdf");
		}


    }
}
