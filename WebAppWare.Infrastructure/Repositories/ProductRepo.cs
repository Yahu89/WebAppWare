using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAppWare.Domain.Interfaces;
using WebAppWare.Infrastructure.BaseContext;

namespace WebAppWare.Infrastructure.Repositories;

public class ProductRepo : IProductRepo
{
	private readonly WarehouseBaseContext _warehouseBaseContext;
	public ProductRepo(WarehouseBaseContext warehouseBaseContext)
    {
        _warehouseBaseContext = warehouseBaseContext;
    }

    public async Task Add(Product product)
    {
        _warehouseBaseContext.Products.Add(product);
        await _warehouseBaseContext.SaveChangesAsync();
    }

    public async Task Delete(Product product)
    {
        _warehouseBaseContext.Products.Remove(product);
        await _warehouseBaseContext.SaveChangesAsync();
    }

    public async Task<List<Product>> GetAll()
	{
		return _warehouseBaseContext.Products.ToList();
	}

    public async Task<Product> GetById(int id)
    {
        return await _warehouseBaseContext.Products.FirstOrDefaultAsync(p => p.Id == id);
    }

    public async Task Update(Product product)
    {
        _warehouseBaseContext.Products.Update(product);
        await _warehouseBaseContext.SaveChangesAsync();
    }
}
