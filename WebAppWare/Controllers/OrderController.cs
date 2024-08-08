using HarfBuzzSharp;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebAppWare.Database.Entities;
using WebAppWare.Database.Entities.Enums;
using WebAppWare.Models;
using WebAppWare.Repositories;
using WebAppWare.Repositories.Interfaces;
using WebAppWare.Utils;
using WepAppWare.Database.Entities;

namespace WebAppWare.Controllers
{
    [Authorize(Roles = "purchase,admin")]
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

        //[HttpGet]
        public async Task<IActionResult> Index()
        {
            try
            {
                var result = await _orderRepo.GetAll();
                return View(result);
            }
            catch (Exception ex)
            {
                return Json(ex.ToString());
            }
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var model = new OrderModel();

            await SetComboboxValue(model);

            model.Document = await _orderRepo.SetOrderNumber(DateTime.Today);

            return View(nameof(Edit), model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(OrderModel model)
        {
            try
            {
                await _orderRepo.ValidateModel(model);
                int id = await _orderRepo.Create(model);

                await _orderDetailsRepo.CreateRange(model.OrderDetails, id);

                return Json(new { redirectToUrl = Url.Action(nameof(Index), "Order") });

            }
            catch (Exception ex)
            {
                return Json(new { redirectToUrl = Url.Action(nameof(Create), "Order") });
            }
        }

        //[HttpGet]
        public async Task<IActionResult> Remove(int id)
        {
            try
            {
                var model = await _orderRepo.GetById(id);

                return View(model);
            }
            catch (NullReferenceException ex)
            {
                // logowanie bledow
                Console.WriteLine(ex.ToString());


                // TODO: wyswietl komunikat na stronie z bledem!
                // tutaj kiedys wyswietlimy komunikat "Brak zamowienia o id: XXXX"

                return Json(new { redirectToUrl = Url.Action(nameof(Index), ex.Message) });
            }
            catch (Exception ex)
            {
                // logowanie bledow
                Console.WriteLine(ex.ToString());

                // TODO: wyswietl komunikat na stronie z bledem!
                // tutaj kiedys wyswietlimy komunikat "Cos poszlo nie tak :("

                return Json(new { redirectToUrl = Url.Action(nameof(Index), "Something went wrong...") });
            }
        }

        [HttpPost]
        public async Task<IActionResult> Remove(OrderModel model)
        {
            try
            {
                await _orderRepo.DeleteById(model.Id);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                return Json(new { redirectToUrl = Url.Action(nameof(Index), "Something went wrong...") });
            }
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var orderItems = await _orderDetailsRepo.GetByOrderId(id);

            var model = await _orderRepo.GetById(id);

            model.OrderDetails = orderItems;

            await SetComboboxValueWhenEdit(model);

            model.IsEdit = true;
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(OrderModel model)
        {
            var orderItems = model.OrderDetails;

            try
            {
                await _orderRepo.ValidateModel(model);

                await _orderRepo.Edit(model);
                await _orderDetailsRepo.EditRange(model.OrderDetails, model.Id);

                return Json(new { redirectToUrl = Url.Action(nameof(Index), "Order") });
            }
            catch (Exception ex)
            {
                return Json(ex.ToString());
            }
        }

        public async Task<IActionResult> PdfGenerate(int id)
        {
            var order = await _orderRepo.GetById(id);
            var report = new OrderPdfReport();
            var bytes = report.PrepareReport(order);

            return File(bytes, "application/pdf");
        }

        private async Task SetComboboxValue(OrderModel model)
        {
            model.Products = (await _productRepo.GetAll()).Select(x => new SelectListItem()
            {
                Text = x.ItemCode,
                Value = x.Id.ToString(),
            });
            model.Suppliers = (await _supplierRepo.GetAll()).Select(x => new SelectListItem()
            {
                Text = x.Name,
                Value = x.Id.ToString(),
            });
            model.StatusList = EnumExtensions.ToSelectList<OrderStatus>(
                    value: x => x,
                    translate: x => x.DisplayName(),
                    selected: null //x => x == model.Status
                );
        }

        private async Task SetComboboxValueWhenEdit(OrderModel model)
        {
            model.Products = (await _productRepo.GetAll()).Select(x => new SelectListItem()
            {
                Text = x.ItemCode,
                Value = x.Id.ToString()
            });

            foreach (var item in model.OrderDetails)
            {
                item.Items = await SetItemComboWhenEdit(model, item.ProductItemCode);
            }

            model.Suppliers = await SetSupplierComboWhenEdit(model);

            var x = EnumExtensions.ToSelectList<OrderStatus>(
                    value: x => x,
                    translate: x => x.DisplayName(),
                    selected: x => x == model.Status
                );

            model.StatusList = EnumExtensions.ToSelectList<OrderStatus>(
                    value: x => x,
                    translate: x => x.DisplayName(),
                    selected: x => x == model.Status
                );
        }

        private async Task<IEnumerable<SelectListItem>> SetSupplierComboWhenEdit(OrderModel model)
        {
            var supplier = model.SupplierName;

            var allSupplierList = await _supplierRepo.GetAll();

            var result = allSupplierList.Select(x => new SelectListItem()
            {
                Value = x.Id.ToString(),
                Text = x.Name,
                Selected = supplier == x.Name ? true : false
            });

            return result;
        }

        private async Task<IEnumerable<SelectListItem>> SetItemComboWhenEdit(OrderModel model, string item)
        {
            var allItems = await _productRepo.GetAll();

            var result = allItems.Select(x => new SelectListItem()
            {
                Value = x.Id.ToString(),
                Text = x.ItemCode,
                Selected = x.ItemCode == item ? true : false
            });

            return result;
        }
    }
}
