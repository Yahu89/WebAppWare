﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAppWare.Models;

namespace WebAppWare.Repositories.Interfaces;

public interface IProductRepo
{
	Task<IEnumerable<ProductModel>> GetAll();
	Task<int> GetProductIdByCode(string itemCode);
	Task Add(ProductModel product, int? imageId = null);
	Task<ProductModel> GetById(int id);
	Task Update(ProductModel product);
	Task Delete(ProductModel product);
}
