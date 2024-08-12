using WebAppWare.Api.Dto;
using WebAppWare.Database.Entities;

namespace WebAppWare.Api.Repositories;

public interface IProductRepository
{
    Task Create(ProductCreateDto model);
    Task<int> CreateImage(IFormFile file);
    Task<Product> GetById(int id);
    Task Delete(Product model);
}
