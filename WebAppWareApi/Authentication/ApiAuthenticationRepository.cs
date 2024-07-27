using WebAppWare.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http.HttpResults;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using WebAppWare.Database;

namespace WebAppWareApi.Authentication;

public class ApiAuthenticationRepository : IApiAuthenticationRepository
{
	private readonly AuthenticationSettings _authenticationSettings;
	private readonly WarehouseBaseContext _dbContext;
	public ApiAuthenticationRepository(AuthenticationSettings authenticationSettings,
										WarehouseBaseContext dbContext)
    {
		_authenticationSettings = authenticationSettings;
		_dbContext = dbContext;
	}
    public string GenerateToken(LoginModelDto model)
	{
		var user = GetUserByName(model);
		var role = GetRoleByUser(user);

		var claims = new List<Claim>()
		{
			new Claim(ClaimTypes.Name, model.UserName),
			new Claim(ClaimTypes.Role, role)
		};

		var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_authenticationSettings.JwtKey));
		var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
		var expiryTime = DateTime.Now.AddMinutes(_authenticationSettings.JwtExpireMinutes);

		var token = new JwtSecurityToken(_authenticationSettings.JwtIssuer,
									_authenticationSettings.JwtIssuer,
									claims,
									expires: expiryTime,
									signingCredentials: credentials);

		var tokenHandler = new JwtSecurityTokenHandler();

		return tokenHandler.WriteToken(token);
	}

	private IdentityUser GetUserByName(LoginModelDto model)
	{
		var user = _dbContext.Users.FirstOrDefault(x => x.UserName == model.UserName);
		return user;
	}

	private string GetRoleByUser(IdentityUser user)
	{
		var roleId = _dbContext.UserRoles.FirstOrDefault(x => x.UserId == user.Id).RoleId;
		var role = _dbContext.Roles.FirstOrDefault(x => x.Id == roleId).Name;

		return role;
	}
}
