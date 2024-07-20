using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using WebAppWare.Models;
using WebAppWare.Repositories.Interfaces;

namespace WebAppWare.Repositories;

public class UserAuthentication : IUserAuthentication
{
	private readonly SignInManager<IdentityUser> _signInManager;
	private readonly UserManager<IdentityUser> _userManager;
	private readonly RoleManager<IdentityRole> _roleManager;

	public UserAuthentication(SignInManager<IdentityUser> signInManager, 
								UserManager<IdentityUser> userManager,
								RoleManager<IdentityRole> roleManager)
    {
		_signInManager = signInManager;
		_userManager = userManager;
		_roleManager = roleManager;
    }

	public async Task<bool> Login(LoginModelDto model)
	{
		var user = await _userManager.FindByNameAsync(model.UserName);

		if (user == null)
		{
			return false;
		}

		if (!await _userManager.CheckPasswordAsync(user, model.Password))
		{
			return false;
		}

		var signInResult = await _signInManager.PasswordSignInAsync(user, model.Password, false, true);

		if (signInResult.Succeeded)
		{
			var userRoles = await _userManager.GetRolesAsync(user);

			var userClaims = new List<Claim>()
			{
				new Claim(ClaimTypes.Name, user.UserName)
			};

			foreach (var userRole in userRoles)
			{
				userClaims.Add(new Claim(ClaimTypes.Role, userRole));
			}

			return true;
		}

		return false;
	}

	public async Task Logout()
	{
		await _signInManager.SignOutAsync();
	}

	public async Task<bool> IsNeededToSetUsersOrRoles()
	{
		var users = await _userManager.Users.ToListAsync();

		if (users.Count == 0)
		{
			return true;
		}

		var roles = await _roleManager.Roles.ToListAsync();

		if (roles.Count == 0)
		{
			return true;
		}

		return false;
	}

	public async Task CreateUsersAndRoles()
	{
		var admin = new IdentityUser()
		{
			SecurityStamp = Guid.NewGuid().ToString(),
			UserName = "admin"
		};

		var purchaseUser = new IdentityUser()
		{
			SecurityStamp = Guid.NewGuid().ToString(),
			UserName = "purchaseUser"
		};

		var warehouseUser = new IdentityUser()
		{
			SecurityStamp = Guid.NewGuid().ToString(),
			UserName = "warehouseUser"
		};

		var result = await _userManager.CreateAsync(admin, "Admin01_");

		if (!result.Succeeded)
		{
			var zm = admin.UserName;
		}

		await _userManager.CreateAsync(purchaseUser, "Purchase01_");
		await _userManager.CreateAsync(warehouseUser, "Warehouse01_");

		await _roleManager.CreateAsync(new IdentityRole("admin"));
		await _roleManager.CreateAsync(new IdentityRole("purchase"));
		await _roleManager.CreateAsync(new IdentityRole("warehouse"));

		await _userManager.AddToRoleAsync(admin, "admin");
		await _userManager.AddToRoleAsync(purchaseUser, "purchase");
		await _userManager.AddToRoleAsync(warehouseUser, "warehouse");
	}


}
