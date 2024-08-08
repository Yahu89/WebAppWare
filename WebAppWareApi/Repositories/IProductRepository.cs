using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.Blazor;
using WebAppWare.Api.Dto;
using WebAppWare.Database.Entities;
using WebAppWare.Models;

namespace WebAppWare.Api.Repositories;

public interface IProductRepository
{
    Task Create(ProductCreateDto model);
    Task<int> CreateImage(IFormFile file);
    Task<Product> GetById(int id);
    Task Delete(Product model);
}
