using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAppWare.Infrastructure;

namespace WebAppWare.Domain.Interfaces;

public interface IProductRepo
{
	Task<List<Product>> GetAll();
	Task Add(Product product);
	Task<Product> GetById(int id);
	Task Update(Product product);
	Task Delete(Product product);
}
