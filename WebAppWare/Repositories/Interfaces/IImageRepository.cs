using WebAppWare.Models;
using WebAppWare.Models.BaseModels;

namespace WebAppWare.Repositories.Interfaces
{
	public interface IImageRepository
	{
		Task Create(ProductModel product);
		Task<int> CreateImage(ProductModel product);
		Task Update(ProductModel product);
		Task<string> GetLogoPath();
	}
}
