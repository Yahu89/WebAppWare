using Microsoft.AspNetCore.Mvc;
using WebAppWare.Application.Services;

namespace WebAppWare.Controllers
{
    public class ProductFlowController : Controller
    {
        private readonly IProductFlowService _productFlowService;
        public ProductFlowController(IProductFlowService productFlowService)
        {
            _productFlowService = productFlowService;
        }
        public async Task<IActionResult> Index()
        {
            var prodFlowlist = await _productFlowService.GetAll();
            return View(prodFlowlist);
        }
    }
}
