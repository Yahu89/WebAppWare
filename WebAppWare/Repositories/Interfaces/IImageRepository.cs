using WebAppWare.Models;
using WebAppWare.Models.BaseModels;

namespace WebAppWare.Repositories.Interfaces
{
	public interface IImageRepository
	{
		public Task Create(ProductModel product);
	}
}
