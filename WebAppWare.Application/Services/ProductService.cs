using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAppWare.Domain.Interfaces;
using WebAppWare.Infrastructure;

namespace WebAppWare.Application.Services;

public class ProductService : IProductService
{
	private readonly IProductRepo _productRepo;
	public ProductService(IProductRepo productRepo)
    {
        _productRepo = productRepo;
    }

    public async Task Add(Product product)
    {
        await _productRepo.Add(product);
    }

    public async Task Delete(Product product)
    {
        await _productRepo.Delete(product);
    }

    public async Task<List<Product>> GetAll()
	{
		return await _productRepo.GetAll();
	}

    public async Task<Product> GetById(int id)
    {
        return await _productRepo.GetById(id);
    }

    public async Task Update(Product product)
    {
        await _productRepo.Update(product);
    }
}
