using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using WebAppWare.Database;
using WebAppWare.Database.Entities;
using WebAppWare.Models;
using WebAppWare.Repositories.Interfaces;

namespace WebAppWare.Repositories;

public class ProductFlowRepo : IProductFlowRepo
{
    private readonly WarehouseDbContext _dbContext;

    private Expression<Func<ProductsFlow, ProductFlowModel>> MapToModel = x => new ProductFlowModel()
    {
        Id = x.Id,
        MovementId = x.WarehouseMovementId,
        MovementType = x.WarehouseMovement.MovementType,
        Warehouse = x.Warehouse.Name,
        ItemCode = x.Product.ItemCode,
        Quantity = x.Quantity,
        Supplier = x.Supplier.Name,
        CreationDate = x.WarehouseMovement.CreationDate
    };

    private Expression<Func<ProductFlowModel, ProductsFlow>> MapToEntity = x => new ProductsFlow()
    {
        Id = x.Id,
        WarehouseMovementId = x.MovementId,
        Quantity = x.Quantity,
    };

    public ProductFlowRepo(WarehouseDbContext dbContext)
    {
        _dbContext = dbContext;
    }

	public async Task CreateRange(List<ProductsFlow> model)
	{
        _dbContext.ProductsFlows.AddRange(model);
        await _dbContext.SaveChangesAsync();
	}

	public async Task DeleteById(int id)
	{
		var prodFlowToDelete = _dbContext.ProductsFlows.FirstOrDefault(x => x.Id == id);

        if (prodFlowToDelete != null)
        {
			_dbContext.ProductsFlows.Remove(prodFlowToDelete);
			await _dbContext.SaveChangesAsync();
		}    
	}

	public async Task<List<ProductFlowModel>> GetAll()
    {
        var result = await _dbContext.ProductsFlows.Select(MapToModel)
                                            .OrderByDescending(x => x.CreationDate)
                                            .ToListAsync();

        return result;
    }

	public async Task<List<ProductFlowModel>> GetAllCumulative(string itemCode, string warehouse)
	{
		var prodFlowWithCumulative = await _dbContext.ProductsFlows.Select(MapToModel)
                                                            .Where(x => x.ItemCode == itemCode)
                                                            .Where(x => x.Warehouse == warehouse)
                                                            .OrderBy(x => x.CreationDate)
                                                            .ToListAsync();

        for (int i = 0; i < prodFlowWithCumulative.Count; i++)
        {
            if (i == 0)
            {
                prodFlowWithCumulative[i].Cumulative = prodFlowWithCumulative[i].Quantity;
            }
            else
            {
                prodFlowWithCumulative[i].Cumulative = prodFlowWithCumulative[i - 1].Cumulative + prodFlowWithCumulative[i].Quantity;
            }
        }

        return prodFlowWithCumulative;
	}

	public async Task<ProductFlowModel> GetById(int id)
	{
		var productFlow = await _dbContext.ProductsFlows.Select(MapToModel)
                                                        .FirstOrDefaultAsync(x => x.Id == id);
        return productFlow;
	}

	public async Task<List<ProductFlowModel>> GetProductFlowsByMoveId(int id)
	{
		var result = await _dbContext.ProductsFlows.Select(MapToModel).Where(x => x.MovementId == id).ToListAsync();
        return result;
	}
}
