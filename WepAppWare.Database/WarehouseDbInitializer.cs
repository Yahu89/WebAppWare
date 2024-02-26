using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAppWare.Database;
using WebAppWare.Database.Entities;
using WepAppWare.Database.Entities;

namespace WepAppWare.Database
{
	public class WarehouseDbInitializer
	{
		private readonly WarehouseDbContext _dbContext;

		public WarehouseDbInitializer(WarehouseDbContext dbContext)
		{
			_dbContext = dbContext;
		}

		public async Task SeedData()
		{
			await _dbContext.Database.MigrateAsync();

			var productsCount = await _dbContext.Products.CountAsync();
			if (productsCount > 0)
				return;

			var productImage = new Image()
			{
				Name = "test image",
				Path = "images/test-image.jpg",
				AbsolutePath = @"C:\Users\Yahu\source\repos\WebAppWare\WebAppWare\wwwroot\images\test-image.jpg"
			};

			await _dbContext.Images.AddAsync(productImage);
			await _dbContext.SaveChangesAsync();

			var products = new List<Product>
			{
				new Product
				{
					ItemCode = "42001A",
					Description = "Świeczka zapachowa",
					ImageId = productImage.Id,
				},
				new Product
				{
					ItemCode = "42002A",
					Description = "Odkurzacz",
					ImageId = productImage.Id,
				},
			};

			await _dbContext.Products.AddRangeAsync(products);
			await _dbContext.SaveChangesAsync();

			var warehouses = new List<Warehouse>
			{
				new Warehouse
				{
					Name = "Internal",
					IsActive = true,
				},
				new Warehouse
				{
					Name = "External",
					IsActive = true,
				},
			};

			await _dbContext.Warehouses.AddRangeAsync(warehouses);
			await _dbContext.SaveChangesAsync();

			var suppliers = new List<Supplier>
			{
				new Supplier
				{
					Name = "Elewar sp. z o.o.",
					Email = "elewar@elewar.com",
				},
				new Supplier
				{
					Name = "Almex sp. z o.o.",
					Email = "almex@almex.com",
				},
			};

			await _dbContext.Suppliers.AddRangeAsync(suppliers);
			await _dbContext.SaveChangesAsync();

			var order1 = new Order
			{
				Document = "PO20022401",
				SupplierId = 1,
				Status = OrderStatus.New,
			};
			var order1Items = new List<OrderItem>
			{
				new OrderItem
				{
					Order = order1,
					ProductId = 1,
					Quantity = 10,
				},
				new OrderItem
				{
					Order = order1,
					ProductId = 2,
					Quantity = 25,
				},
			};

			var order2 = new Order
			{
				Document = "PO20022402",
				SupplierId = 2,
				Status = OrderStatus.New,
			};
			var order2Items = new List<OrderItem>
			{
				new OrderItem
				{
					Order = order2,
					ProductId = 2,
					Quantity = 10,
				},
				new OrderItem
				{
					Order = order2,
					ProductId = 1,
					Quantity = 25,
				},
			};

			await _dbContext.Orders.AddAsync(order1);
			await _dbContext.Orders.AddAsync(order2);

			await _dbContext.OrderItems.AddRangeAsync(order1Items);
			await _dbContext.SaveChangesAsync();

			var warehouseMovement = new WarehouseMovement()
			{
				MovementType = MovementType.PZ,
				Document = "PZ09022401",
			};

			var productFlow1 = new ProductsFlow()
			{
				WarehouseMovement = warehouseMovement,
				ProductId = 1,
				WarehouseId = 1,
				SupplierId = 1,
				Quantity = 100,
			};
			var productFlow2 = new ProductsFlow()
			{
				WarehouseMovement = warehouseMovement,
				ProductId = 2,
				WarehouseId = 1,
				SupplierId = 2,
				Quantity = 200,
			};

			await _dbContext.WarehouseMovements.AddAsync(warehouseMovement);
			await _dbContext.ProductsFlows.AddAsync(productFlow1);
			await _dbContext.ProductsFlows.AddAsync(productFlow2);

			await _dbContext.SaveChangesAsync();
		}
	}
}
