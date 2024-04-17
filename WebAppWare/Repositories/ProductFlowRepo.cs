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
        ProductItemCode = x.Product.ItemCode,
        Quantity = x.Quantity,
        Supplier = x.Supplier.Name,
        CreationDate = x.WarehouseMovement.CreationDate,
        ProductId = x.Product.Id,
        SupplierId = x.Supplier.Id,
        DocumentNumber = x.WarehouseMovement.Document
    };

	private Expression<Func<ProductsFlow, ProductFlowSearchModel>> MapToSearchModel = x => new ProductFlowSearchModel()
	{

	};

	//private Expression<Func<Form, ProductFlowModel>> MapFromFormToProductFlowModel = x => new ProductFlowModel()
	//{
	//	Id = x.Id,
	//	DocumentNumber = x.Document,
	//	WarehouseId = x.WarehouseId,
	//	ProductId = x.ProductFlowModels
	//}


    public ProductFlowRepo(WarehouseDbContext dbContext)
    {
        _dbContext = dbContext;
    }

	public async Task CreateRange(IEnumerable<ProductFlowModel> model, int id)
	{
        var entities = model
               .Select(x => new ProductsFlow()
               {
                   Quantity = x.Quantity,
                   WarehouseMovementId = id,
                   ProductId = x.ProductId,
                   SupplierId = x.SupplierId,
               });

		_dbContext.ProductsFlows.AddRange(entities);
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

	public async Task DeleteRange(IEnumerable<ProductFlowModel> model)
	{
        var result = model.Select(x => new ProductsFlow()
        {
            Id = x.Id,
			Quantity = x.Quantity,
			WarehouseMovementId = x.MovementId,
			ProductId = x.ProductId,
			SupplierId = x.SupplierId
		})
                        .ToList();

        _dbContext.ProductsFlows.RemoveRange(result);
        await _dbContext.SaveChangesAsync();
	}

	public List<ProductFlowModel> FromCollectionToProductFlowModel(IFormCollection collection, int moveId)
	{
		var itemCodes = collection["items"];
		var suppliers = collection["suppliers"];
		var quantities = collection["quantity"];
		var warehouse = collection["WarehouseId"];

		List<ProductFlowModel> list = new List<ProductFlowModel>();

		for (int i = 0; i < itemCodes.Count(); i++)
		{
            if (string.IsNullOrWhiteSpace(quantities[i]) || !int.TryParse(quantities[i], out _) || !int.TryParse(warehouse, out _))
            {
                throw new FormatException("Data is wrong...");
            }

			list.Add(new ProductFlowModel());
			list[i].ProductId = int.Parse(itemCodes[i]);
			list[i].SupplierId = int.Parse(suppliers[i]);
			list[i].Quantity = int.Parse(quantities[i]);
			list[i].MovementId = moveId;
		}

        return list;
	}

	public async Task<List<ProductFlowModel>> GetAll()
    {
        var result = await _dbContext.ProductsFlows.Select(MapToModel)
                                            .OrderByDescending(x => x.CreationDate)
                                            .ToListAsync();

        return result;
    }

	public async Task<IEnumerable<ProductFlowModel>> GetAllCumulative(int prodId, int wareId)
	{
		// 

		var prodFlowWithCumulative = await _dbContext.ProductsFlows.Select(MapToModel)
                                                            .Where(x => x.ProductId == prodId)
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

	public async Task<IEnumerable<ProductFlowModel>> GetBySearch(string warehouse, string itemCode, string supplier)
	{
        string[] content = new string[] { warehouse, itemCode, supplier };

        var result = await _dbContext.ProductsFlows.Select(MapToModel).ToListAsync();

        for (int i = 0; i < content.Length; i++)
        {
            if (!string.IsNullOrEmpty(content[i]))
            {
                switch (i)
                {
                    //case 0:
                    //    {
                    //        result = result.Where(x => x.Warehouse.Contains(content[i])).ToList();
                    //        break;
                    //    }

					case 1:
						{
							result = result.Where(x => x.ProductItemCode.Contains(content[i])).ToList();
							break;
						}

					case 2:
						{

							result = result.Where(x => !string.IsNullOrEmpty(x.Supplier))
                                            .Where(x => x.Supplier.Contains(content[i])).ToList();
							break;
						}
				}
            }
        }

        return result;
	}

	public async Task<List<ProductFlowModel>> GetProductFlowsByMoveId(int id)
	{
		var result = await _dbContext.ProductsFlows.Select(MapToModel).Where(x => x.MovementId == id).ToListAsync();
        return result;
	}

	public async Task<bool> IsReadyToDeleteProductFlow(int id)
	{
		var productFlowModel = await GetById(id);
		int movementId = productFlowModel.MovementId;

		var prodFlowMoveIdList = await GetProductFlowsByMoveId(movementId);
		int howManyItems = prodFlowMoveIdList.Count;

		int qty = productFlowModel.Quantity;
		DateTime insertDate = productFlowModel.CreationDate;

		IEnumerable<ProductFlowModel> prodFlowCumulative = await GetAllCumulative((int)productFlowModel.ProductId,
			(int)productFlowModel.WarehouseId);

		var prodFlowCumulativeLimited = prodFlowCumulative.Where(x => x.CreationDate > insertDate).ToList();

		int count = prodFlowCumulativeLimited.Count;

		int minValue = 0;

		if (count > 0)
		{
			minValue = prodFlowCumulativeLimited.Min(x => x.Cumulative);
		}
		else
		{
			var list = (List<ProductFlowModel>)prodFlowCumulative;
			minValue = list[0].Cumulative;
		}

		if (minValue >= qty)
		{
            return true;
		}

        return false;
	}
}
