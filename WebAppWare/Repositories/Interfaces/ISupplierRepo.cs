using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAppWare.Models;

namespace WebAppWare.Repositories.Interfaces;

public interface ISupplierRepo
{
    Task<IEnumerable<SupplierModel>> GetAll();
    Task<int> GetSupplierIdByName(string supplierName);
    Task<SupplierModel> GetById(int id);
    Task Update(SupplierModel supplier);
    Task Create(SupplierModel supplier);
    Task Delete(SupplierModel supplier);
}
