
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

var builder = WebApplication.CreateBuilder(args);

// Database
builder.Services.AddDbContext<WarehouseBaseContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString(nameof(WarehouseBaseContext))));

// Add services to the container.
builder.Services.AddControllersWithViews();

// Repos
builder.Services.AddScoped<IWarehouseRepo, WarehouseRepo>();
builder.Services.AddScoped<IProductRepo, ProductRepo>();
builder.Services.AddScoped<ISupplierRepo, SupplierRepo>();
builder.Services.AddScoped<IProductFlowRepo, ProductFlowRepo>();
builder.Services.AddScoped<IMovementRepo, MovementRepo>();
builder.Services.AddScoped<IOrderRepo, OrderRepo>();
builder.Services.AddScoped<IOrderDetailsRepo, OrderDetailsRepo>();
builder.Services.AddScoped<IImageRepository, ImageRepository>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
	app.UseExceptionHandler("/Home/Error");
	// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
	app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
//app.UseStaticFiles(new StaticFileOptions()
//{
//	ContentTypeProvider = new FileExtensionContentTypeProvider(new Dictionary<string, string>
//	{
//	 {
//		 ".apk",
//		 "application/vnd.android.package-archive"
//	   }
//	})
//});

//var provider = new FileExtensionContentTypeProvider();

//provider.Mappings[".jpg"] = "image/jpeg";


//app.UseStaticFiles(new StaticFileOptions
//{

//	FileProvider = new PhysicalFileProvider(
//		Path.Combine(env.ContentRootPath, "public")),
//	RequestPath = "/public"
//});

//provider.Mappings.Add(".appx", "application/appx");
//provider.Mappings.Add(".msix", "application/msix");
//provider.Mappings.Add(".appxbundle", "application/appxbundle");
//provider.Mappings.Add(".msixbundle", "application/msixbundle");
//provider.Mappings.Add(".appinstaller", "application/appinstaller");
//app.UseStaticFiles(new StaticFileOptions
//{
//	ContentTypeProvider = provider
//});

//var provider = new FileExtensionContentTypeProvider();
//// Add new mappings
//provider.Mappings[".myapp"] = "application/x-msdownload";




app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
	name: "default",
	pattern: "{controller=Product}/{action=Index}/{id?}");



var scope = app.Services.CreateScope();
var dbContext = scope.ServiceProvider.GetRequiredService<WarehouseBaseContext>();
var dbIntializer = new WarehouseDbInitializer(dbContext);

// seeding data
await dbIntializer.SeedData();

app.Run();