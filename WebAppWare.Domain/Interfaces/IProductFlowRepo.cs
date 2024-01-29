using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAppWare.Infrastructure;

namespace WebAppWare.Domain.Interfaces;

public interface IProductFlowRepo
{
    Task<List<ProductFlowModel>> GetAll();
}
