using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAppWare.Infrastructure;

namespace WebAppWare.Domain.Interfaces;

public interface ISupplierRepo
{
    Task<List<Supplier>> GetAll();
    Task<Supplier> GetById(int id);
    Task Update(Supplier supplier);
    Task Create(Supplier supplier);
    Task Delete(Supplier supplier);
}
