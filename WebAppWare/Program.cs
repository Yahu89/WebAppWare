
using WebAppWare.Repositories.Interfaces;
using WebAppWare.Repositories;
using WebAppWare.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.Extensions.FileProviders;
using Ninject.Activation;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore;
using WepAppWare.Database;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Identity;
using WebAppWare.Middleware;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

var builder = WebApplication.CreateBuilder(args);

// Database
builder.Services.AddDbContext<WarehouseBaseContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString(nameof(WarehouseBaseContext))));
//builder.Services.AddDefaultIdentity<IdentityUser>().AddEntityFrameworkStores<WarehouseBaseContext>();
builder.Services.AddIdentity<IdentityUser, IdentityRole>()
		.AddEntityFrameworkStores<WarehouseBaseContext>()
		.AddDefaultTokenProviders();

builder.Services.ConfigureApplicationCookie(op => op.LoginPath = "/Home/Login");

// Add services to the container.
builder.Services.AddControllersWithViews();

// Repos
builder.Services.AddTransient<IWarehouseRepo, WarehouseRepo>();
builder.Services.AddTransient<IProductRepo, ProductRepo>();
builder.Services.AddTransient<ISupplierRepo, SupplierRepo>();
builder.Services.AddTransient<IProductFlowRepo, ProductFlowRepo>();
builder.Services.AddTransient<IMovementRepo, MovementRepo>();
builder.Services.AddTransient<IOrderRepo, OrderRepo>();
builder.Services.AddTransient<IOrderDetailsRepo, OrderDetailsRepo>();
builder.Services.AddTransient<IImageRepository, ImageRepository>();
builder.Services.AddTransient<IUserAuthentication, UserAuthentication>();
//builder.Services.AddTransient<ErrorHandling>();
//builder.Services.AddSingleton<ITempDataProvider, CookieTempDataProvider>();
//builder.Services.AddSingleton<ITempDataDictionaryFactory, TempDataDictionaryFactory>();




var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
	app.UseExceptionHandler("/Home/Error");
	// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
	app.UseHsts();
}

//app.UseMiddleware<ErrorHandling>();

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
	name: "default",
	pattern: "{controller=Home}/{action=Index}/{id?}");




var scope = app.Services.CreateScope();
var dbContext = scope.ServiceProvider.GetRequiredService<WarehouseBaseContext>();
var dbIntializer = new WarehouseDbInitializer(dbContext);

// seeding data
await dbIntializer.SeedData();

app.Run();