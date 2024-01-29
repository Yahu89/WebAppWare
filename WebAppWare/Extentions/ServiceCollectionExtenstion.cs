using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAppWare.Database;

namespace WebAppWare.Extentions;

public static class ServiceCollectionExtenstion
{
	public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
	{
	}
}
