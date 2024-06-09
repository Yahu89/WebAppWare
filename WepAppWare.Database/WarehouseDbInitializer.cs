using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WebAppWare.Database;
using WebAppWare.Database.Entities;
using WebAppWare.Database.Entities.Enums;


namespace WepAppWare.Database
{
	public class WarehouseDbInitializer
	{
		private readonly WarehouseBaseContext _dbContext;


		public WarehouseDbInitializer(WarehouseBaseContext dbContext)
		{
			_dbContext = dbContext;
			//_userAuthenticationService = userAuthenticationService;
		}

		public async Task SeedData()
		{
			await _dbContext.Database.MigrateAsync();

			var productsCount = await _dbContext.Products.CountAsync();
			var prodFlowsCount = await _dbContext.ProductsFlows.CountAsync();

			if (productsCount > 0)
				return;

			var productImage = new Image()
			{
				Name = "test image",
				Path = "images/test-image.jpg",
				AbsolutePath = @"C:\Users\Yahu\source\repos\WebAppWare\WebAppWare\wwwroot\images\test-image.jpg"
			};

			var prodImg = new Image();

			_dbContext.Images.Add(productImage);
			await _dbContext.SaveChangesAsync();

			var products = new List<Product>
			{
				new Product
				{
					Id = 1,
					ItemCode = "42001A",
					Description = "Świeczka zapachowa",
					ImageId = productImage.Id,
				},
				new Product
				{
					Id = 2,
					ItemCode = "42002A",
					Description = "Odkurzacz",
					ImageId = productImage.Id,
				},
			};

			_dbContext.Products.AddRange(products);
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

			_dbContext.Warehouses.AddRange(warehouses);
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

			_dbContext.Suppliers.AddRange(suppliers);
			await _dbContext.SaveChangesAsync();

			var order1 = new Order
			{
				Document = "PO20022401",
				SupplierId = 1,
				Status = OrderStatus.InProgress,
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
				Status = OrderStatus.InProgress,
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

			_dbContext.Orders.Add(order1);
			_dbContext.Orders.Add(order2);

			await _dbContext.OrderItems.AddRangeAsync(order1Items);
			await _dbContext.SaveChangesAsync();

			var wareTemp = new WarehouseMovement();
			//var prodFlow = new ProductsFlow();

			//prodFlow.WarehouseMovement = wareTemp;


			var warehouseMovement = new WarehouseMovement()
			{
				MovementType = (int)MovementType.PZ,
				Document = "PZ09022401",
			};

			var productFlow1 = new ProductsFlow()
			{
				WarehouseMovement = warehouseMovement,
				ProductId = 1,
				//WarehouseId = 1,
				SupplierId = 1,
				Quantity = 100,
			};

			var productFlow2 = new ProductsFlow()
			{
				WarehouseMovement = warehouseMovement,
				ProductId = 2,
				//WarehouseId = 1,
				SupplierId = 2,
				Quantity = 200,
			};

			_dbContext.WarehouseMovements.Add(warehouseMovement);
			_dbContext.ProductsFlows.Add(productFlow1);
			_dbContext.ProductsFlows.Add(productFlow2);

			await _dbContext.SaveChangesAsync();
		}

		//public async Task<bool> IsNeededToSetUsersOrRoles()
		//{
		//	//_userManager = new UserManager<IdentityUser>();
		//	var users = await _userManager.Users.ToListAsync();

		//	if (users == null)
		//	{
		//		return true;
		//	}

		//	var roles = await _roleManager.Roles.ToListAsync();

		//	if (roles == null)
		//	{
		//		return true;
		//	}

		//	return false;
		//}

		//public async Task CreateUsersAndRoles()
		//{
		//	var admin = new IdentityUser()
		//	{
		//		SecurityStamp = Guid.NewGuid().ToString(),
		//		UserName = "admin"
		//	};

		//	var purchaseUser = new IdentityUser()
		//	{
		//		SecurityStamp = Guid.NewGuid().ToString(),
		//		UserName = "purchaseUser"
		//	};

		//	var warehouseUser = new IdentityUser()
		//	{
		//		SecurityStamp = Guid.NewGuid().ToString(),
		//		UserName = "warehouseUser"
		//	};

		//	await _userManager.CreateAsync(admin, "admin01");
		//	await _userManager.CreateAsync(purchaseUser, "purchase01");
		//	await _userManager.CreateAsync(warehouseUser, "warehouse01");

		//	await _roleManager.CreateAsync(new IdentityRole("admin"));
		//	await _roleManager.CreateAsync(new IdentityRole("purchase"));
		//	await _roleManager.CreateAsync(new IdentityRole("warehouse"));

		//	await _userManager.AddToRoleAsync(admin, "admin");
		//	await _userManager.AddToRoleAsync(purchaseUser, "purchase");
		//	await _userManager.AddToRoleAsync(warehouseUser, "warehouse");
		//}
	}
}
