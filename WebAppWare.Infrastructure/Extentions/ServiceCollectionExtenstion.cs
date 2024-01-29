using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAppWare.Application.Services;
using WebAppWare.Domain.Interfaces;
using WebAppWare.Infrastructure.BaseContext;
using WebAppWare.Infrastructure.Repositories;

namespace WebAppWare.Infrastructure.Extentions;

public static class ServiceCollectionExtenstion
{
	public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
	{
		services.AddDbContext<WarehouseBaseContext>(options => options.UseSqlServer(configuration.GetConnectionString("WarehouseBaseConnection")));

		services.AddScoped<IWarehouseRepo, WarehouseRepo>();

		services.AddScoped<IProductRepo, ProductRepo>();

		services.AddScoped<ISupplierRepo, SupplierRepo>();

		services.AddScoped<IProductFlowRepo, ProductFlowRepo>();

		services.AddScoped<IMovementRepo, MovementRepo>();

		services.AddScoped<IComboRepo, ComboRepo>();
	}
}
