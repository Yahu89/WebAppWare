using WebAppWare.Models;

namespace WebAppWareApi.Authentication;

public interface IApiAuthenticationRepository
{
	string GenerateToken(LoginModelDto login);
}
