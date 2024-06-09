using Microsoft.AspNetCore.Identity.UI.V5.Pages.Account.Internal;
using Microsoft.AspNetCore.Mvc;
using WebAppWare.Models;
using WebAppWare.Repositories.Interfaces;

namespace WebAppWare.Controllers
{
	public class HomeController : Controller
	{
		private readonly IUserAuthentication _userAuthentication;

		public HomeController(IUserAuthentication userAuthentication)
		{
			_userAuthentication = userAuthentication;
		}
		public IActionResult Index()
		{
			return View();
		}

		public IActionResult Error()
		{
			var exceptionMessage = TempData["ExceptionMessage"] as string;
			ViewBag.ExceptionMessage = exceptionMessage;
			return View();
		}

		public IActionResult Login()
		{
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> Login(LoginModelDto model)
		{
			var loggedUser = await _userAuthentication.Login(model);

			if (loggedUser)
			{
				ViewBag.User = model.UserName;
				return RedirectToAction(nameof(Index));
			}

			return Json("Nieudane logowanie");
		}

		public async Task<IActionResult> Register()
		{
			if (await _userAuthentication.IsNeededToSetUsersOrRoles())
			{
				await _userAuthentication.CreateUsersAndRoles();
				return Json("Utworzono");
			}

			return RedirectToAction(nameof(Index));
		}

		[HttpPost]
		public async Task<IActionResult> Logout()
		{
			await _userAuthentication.Logout();
			return RedirectToAction(nameof(Index));
		}
	}
}
