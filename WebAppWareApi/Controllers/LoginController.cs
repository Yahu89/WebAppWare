using Microsoft.AspNetCore.Mvc;
using WebAppWare.Models;
using WebAppWare.Repositories.Interfaces;
using WebAppWareApi.Authentication;

namespace WebAppWareApi.Controllers
{
	[Route("api/[controller]")]
	public class LoginController : Controller
	{
		private readonly IUserAuthentication _userAuthentication;
		private readonly IApiAuthenticationRepository _apiAuthenticationRepository;
		public LoginController(IUserAuthentication userAuthentication,
								IApiAuthenticationRepository apiAuthenticationRepository)
        {
			_userAuthentication = userAuthentication;
			_apiAuthenticationRepository = apiAuthenticationRepository;
		}

		[HttpPost]
        public async Task<ActionResult> Login([FromBody]LoginModelDto model)
		{
			if (await _userAuthentication.Login(model))
			{
				var token = _apiAuthenticationRepository.GenerateToken(model);
				return Ok($"Login succeeded \nToken: {token}");
			}

			return NotFound("Login failed");
		}

		[HttpPost]
		[Route("logout")]
		public async Task<ActionResult> Logout()
		{
			await _userAuthentication.Logout();
			return Ok("Logout successfully");
		}
	}
}
