using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAppWare.Domain.Interfaces;
using WebAppWare.Infrastructure;

namespace WebAppWare.Application.Services;

public class ProductFlowService : IProductFlowService
{
    private readonly IProductFlowRepo _productFlowRepo;
    public ProductFlowService(IProductFlowRepo productFlowRepo)
    {
        _productFlowRepo = productFlowRepo;
    }
    public async Task<List<ProductFlowModel>> GetAll()
    {
        return await _productFlowRepo.GetAll();
    }
}
