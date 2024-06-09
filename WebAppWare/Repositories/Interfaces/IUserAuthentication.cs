using WebAppWare.Models;

namespace WebAppWare.Repositories.Interfaces;

public interface IUserAuthentication
{
	Task<bool> Login(LoginModelDto model);
	Task Logout();
	Task<bool> IsNeededToSetUsersOrRoles();
	Task CreateUsersAndRoles();
}
