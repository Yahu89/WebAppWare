using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAppWare.Database;
using WebAppWare.Models;
using WebAppWare.Repositories.Interfaces;

namespace WebAppWare.Repositories;

public class ProductRepo : IProductRepo
{
	private readonly WarehouseDbContext _dbContext;
	public ProductRepo(WarehouseDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task Add(ProductModel product)
    {
        //_dbContext.Products.Add(product);
        //await _dbContext.SaveChangesAsync();
    }


	public async Task Delete(ProductModel product)
	{
		//    _dbContext.Products.Remove(product);
		//    await _dbContext.SaveChangesAsync();
	}

	public async Task<List<ProductModel>> GetAll()
	{
		//return _dbContext.Products.ToList();
		return new List<ProductModel>();
	}

    public async Task<ProductModel> GetById(int id)
    {
		//return await _dbContext.Products.FirstOrDefaultAsync(p => p.Id == id);
		return new ProductModel();
    }

    public async Task Update(ProductModel product)
    {
        //_dbContext.Products.Update(product);
        //await _dbContext.SaveChangesAsync();
    }
}
