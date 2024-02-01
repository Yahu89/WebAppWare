﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using WebAppWare.Database;
using WebAppWare.Database.Entities;
using WebAppWare.Models;
using WebAppWare.Repositories.Interfaces;

namespace WebAppWare.Repositories;

public class ProductRepo : IProductRepo
{
	private readonly WarehouseDbContext _dbContext;

	private Expression<Func<Product, ProductModel>> MapToModel = e => new ProductModel
	{
		Id = e.Id,
		Description = e.Description,
		ImgUrl = e.ImgUrl,
		ItemCode = e.ItemCode,
	};
	private Expression<Func<ProductModel, Product>> MapToEntity = e => new Product()
	{
		Id = e.Id,
		Description = e.Description,
		ImgUrl = e.ImgUrl,
		ItemCode = e.ItemCode,
	};

	public ProductRepo(WarehouseDbContext dbContext)
	{
		_dbContext = dbContext;
	}

	public async Task Add(ProductModel model)
	{
		var entity = MapToEntity.Compile().Invoke(model);
		_dbContext.Products.Add(entity);
		await _dbContext.SaveChangesAsync();
	}


	public async Task Delete(ProductModel model)
	{
		var entity = MapToEntity.Compile().Invoke(model);

		_dbContext.Products.Remove(entity);
		await _dbContext.SaveChangesAsync();
	}

	public async Task<List<ProductModel>> GetAll()
	{
		return await _dbContext.Products
			.Select(MapToModel)
			.ToListAsync();
	}

	public async Task<ProductModel> GetById(int id)
	{
		var result = await _dbContext.Products
			.Select(MapToModel)
			.FirstOrDefaultAsync(p => p.Id == id);

		if (result == null)
			throw new Exception("There is no product with id" + id);

		return result;
	}

	public async Task<int> GetProductIdByCode(string itemCode)
	{
		var product = await _dbContext.Products.FirstOrDefaultAsync(x => x.ItemCode == itemCode);
		int id = product.Id;
		return id;
	}

	public async Task Update(ProductModel model)
	{
		var entity = MapToEntity.Compile().Invoke(model);

		_dbContext.Products.Update(entity);
		await _dbContext.SaveChangesAsync();
	}
}
