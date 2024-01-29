using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAppWare.Application.Services;

namespace WebAppWare.Application.Extentions;

public static class ServiceCollectionExtention
{
	public static void AddApplication(this IServiceCollection service)
	{
		service.AddScoped<IWarehouseService, WarehouseService>();
		service.AddScoped<IProductService, ProductService>();
		service.AddScoped<ISupplierService, SupplierService>();
		service.AddScoped<IProductFlowService, ProductFlowService>();
		service.AddScoped<IMovementService, MovementService>();
		service.AddScoped<IComboService, ComboService>();
	}
}
