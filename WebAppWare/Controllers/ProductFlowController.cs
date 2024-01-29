using Microsoft.AspNetCore.Mvc;
using WebAppWare.Repositories.Interfaces;

namespace WebAppWare.Controllers
{
    public class ProductFlowController : Controller
    {
        private readonly IProductFlowRepo _productFlowRepo;
        public ProductFlowController(IProductFlowRepo productFlowRepo)
        {
            _productFlowRepo = productFlowRepo;
        }
        public async Task<IActionResult> Index()
        {
            //var prodFlowlist = await _productFlowRepo.GetAll();
            return View();
        }
    }
}
