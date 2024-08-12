using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Text.Json.Serialization;
using WebAppWare.Api.MappingProfile;
using WebAppWare.Api.Middleware;
using WebAppWare.Api.Repositories;
using WebAppWare.Api.Validation;
using WebAppWare.Database;
using WebAppWare.Models.MappingProfiles;
using WebAppWare.Repositories;
using WebAppWare.Repositories.Interfaces;
using WebAppWareApi;
using WebAppWareApi.Authentication;
using WebAppWareApi.Validation;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<WarehouseBaseContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString(nameof(WarehouseBaseContext))));
builder.Services.AddIdentity<IdentityUser, IdentityRole>()
		.AddEntityFrameworkStores<WarehouseBaseContext>()
		.AddDefaultTokenProviders();



var authenticationSettings = new AuthenticationSettings();
builder.Configuration.GetSection("Authentication").Bind(authenticationSettings);

builder.Services.AddSingleton(authenticationSettings);

builder.Services.AddAuthentication(opt =>
{
	opt.DefaultAuthenticateScheme = "Bearer";
	opt.DefaultScheme = "Bearer";
	opt.DefaultChallengeScheme = "Bearer";
}).AddJwtBearer(cfg =>
{
	cfg.RequireHttpsMetadata = false;
	cfg.SaveToken = true;
	cfg.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters()
	{
		ValidIssuer = authenticationSettings.JwtIssuer,
		ValidAudience = authenticationSettings.JwtIssuer,
		IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(authenticationSettings.JwtKey))
	};
});

//builder.Services.Configure<JsonOptions>(options =>
//{
//	options.SerializerOptions.ReferenceHandler = ReferenceHandler.Preserve;
//});

builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve;
});

builder.Services.AddTransient<IWarehouseRepo, WarehouseRepo>();
builder.Services.AddTransient<IProductFlowRepo, ProductFlowRepo>();
builder.Services.AddScoped<IProductRepo, ProductRepo>();
builder.Services.AddTransient<ISupplierRepo, SupplierRepo>();
builder.Services.AddTransient<IMovementRepo, MovementRepo>();
builder.Services.AddTransient<IOrderRepo, OrderRepo>();
builder.Services.AddTransient<IOrderDetailsRepo, OrderDetailsRepo>();
builder.Services.AddTransient<IImageRepository, ImageRepository>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IWarehouseRepository, WarehouseRepository>();
builder.Services.AddScoped<ISupplierRepository, SupplierRepository>();
builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddTransient<IUserAuthentication,  UserAuthentication>();
builder.Services.AddScoped<IApiAuthenticationRepository, ApiAuthenticationRepository>();
builder.Services.AddScoped<ErrorHandlingService>();
builder.Services.AddAutoMapper(typeof(ProductFlowMappingProfile));
builder.Services.AddAutoMapper(typeof(ProductMappingProfile));

builder.Services.AddValidatorsFromAssemblyContaining<WarehouseModelValidator>()
							.AddFluentValidationAutoValidation()
							.AddFluentValidationClientsideAdapters();
builder.Services.AddValidatorsFromAssemblyContaining<WarehouseDtoValidator>()
                            .AddFluentValidationAutoValidation()
                            .AddFluentValidationClientsideAdapters();
builder.Services.AddValidatorsFromAssemblyContaining<ProductCreateDtoValidator>()
                            .AddFluentValidationAutoValidation()
                            .AddFluentValidationClientsideAdapters();
builder.Services.AddValidatorsFromAssemblyContaining<SupplierDtoValidator>()
                            .AddFluentValidationAutoValidation()
                            .AddFluentValidationClientsideAdapters();
builder.Services.AddValidatorsFromAssemblyContaining<OrderCreateDtoValidator>()
                            .AddFluentValidationAutoValidation()
                            .AddFluentValidationClientsideAdapters();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();



var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
    
}




app.UseRouting();
app.UseAuthorization();
app.UseMiddleware<ErrorHandlingService>();
app.UseHttpsRedirection();

app.MapControllers();



app.Run();
