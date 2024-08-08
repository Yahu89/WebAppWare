using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using WebAppWare.Api.Dto;
using WebAppWare.Api.Middleware;
using WebAppWare.Database;
using WebAppWare.Database.Entities;
using WebAppWare.Models;
using WebAppWare.Models.BaseModels;

namespace WebAppWare.Api.Repositories;

public class ProductRepository : IProductRepository
{
    private readonly WarehouseBaseContext _dbContext;
    private readonly IMapper _mapper;
    private readonly IWebHostEnvironment _webHostEnvironment;

    public ProductRepository(WarehouseBaseContext dbContext,
                                IMapper mapper,
                                IWebHostEnvironment webHostEnvironment)
    {
        _dbContext = dbContext;
        _mapper = mapper;
        _webHostEnvironment = webHostEnvironment;
    }

    public async Task<Product> GetById(int id)
    {
        var entity = await _dbContext.Products.Include(x => x.Image)
            .FirstOrDefaultAsync(p => p.Id == id);

        if (entity is null)
        {
            throw new NoContentException();
        }

        //var result = _mapper.Map<Product>(entity);

        return entity;
    }

    public async Task Create(ProductCreateDto model)
    {
        var entity = _mapper.Map<Product>(model);
        _dbContext.Add(entity);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<int> CreateImage(IFormFile file)
    {
        var localPath = CreateLocalPath(file);

        var image = new Image()
        {
            Name = $"{file.Name}",
            Path = localPath,
            AbsolutePath = Path.Combine(_webHostEnvironment.WebRootPath, localPath)
        };

        using var stream = new FileStream(image.AbsolutePath, FileMode.Create);
        await file.CopyToAsync(stream);

        await _dbContext.Images.AddAsync(image);
        await _dbContext.SaveChangesAsync();

        int id = image.Id;

        return id;
    }

    private string CreateLocalPath(IFormFile file)
    {
        string? fileName = Path.GetFileNameWithoutExtension(file.FileName);
        string? extension = Path.GetExtension(file.FileName);
        fileName = fileName + DateTime.Now.ToString("yymmssffff") + extension;
        fileName = $"images/{fileName}";

        return fileName;
    }

    public async Task Delete(Product model)
    {
        if (model is null)
        {
            throw new NoContentException();
        }

        _dbContext.Products.Remove(model);
        await _dbContext.SaveChangesAsync();
    }
}
