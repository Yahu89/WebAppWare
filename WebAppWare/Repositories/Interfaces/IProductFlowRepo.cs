using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAppWare.Database.Entities;
using WebAppWare.Models;

namespace WebAppWare.Repositories.Interfaces;

public interface IProductFlowRepo
{
    Task<List<ProductFlowModel>> GetAll();
    Task<List<ProductFlowModel>> GetAllCumulative(string itemCode, string warehouse);
    Task<ProductFlowModel> GetById(int id);
    Task<List<ProductFlowModel>> GetProductFlowsByMoveId(int id);
    Task CreateRange(List<ProductsFlow> model);
    Task DeleteById(int id);
}
