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

public class ProductFlowRepo : IProductFlowRepo
{
    private readonly WarehouseDbContext _dbContext;
    public ProductFlowRepo(WarehouseDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task<List<ProductFlowModel>> GetAll()
    {
        //List<ProductFlowModel> list = await _dbContext.ProductsFlows.Include(x => x.Warehouse)
        //                                    .Include(x => x.Product)
        //                                    .Include(x => x.Supplier)
        //                                    .Include(x => x.WarehouseMovement)
        //                                    .Select(x => new ProductFlowModel()
        //                                    {
        //                                        Id = x.Id,
        //                                        MoveId = x.WarehouseMovement.Id,
        //                                        MovementType = x.WarehouseMovement.MovementType,
        //                                        Warehouse = x.Warehouse.Name,
        //                                        ItemCode = x.Product.ItemCode,
        //                                        Quantity = x.Quantity,
        //                                        Supplier = x.Supplier.Name,
        //                                        CreationDate = x.WarehouseMovement.CreationDate
        //                                    })
        //                                    .OrderByDescending(x => x.CreationDate)
        //                                    .ToListAsync();
        //return list;

        return new List<ProductFlowModel>();
    }
}
