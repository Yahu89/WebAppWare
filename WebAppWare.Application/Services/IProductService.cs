using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAppWare.Infrastructure;

namespace WebAppWare.Application.Services;

public interface IProductService
{
	Task<List<Product>> GetAll();
	Task Add(Product product);
	Task Update(Product product);
	Task<Product> GetById(int id);
	Task Delete(Product product);
}
