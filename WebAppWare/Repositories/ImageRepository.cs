using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using WebAppWare.Database;
using WebAppWare.Models;
using WebAppWare.Models.BaseModels;
using WebAppWare.Repositories.Interfaces;
using WepAppWare.Database.Entities;

namespace WebAppWare.Repositories
{
	public class ImageRepository : IImageRepository
	{
		private readonly WarehouseDbContext _dbContext;
		private readonly IProductRepo _productRepo;
		private readonly IWebHostEnvironment _webHostEnvironment;

		public ImageRepository(
			WarehouseDbContext dbContext,
			IProductRepo productRepo,
			IWebHostEnvironment webHostEnvironment
			)
        {
			_dbContext = dbContext;
			_productRepo = productRepo;
			_webHostEnvironment = webHostEnvironment;
		}

        public async Task Create(ProductModel product)
		{
			int? id = null;

			if (product.ImageFile != null)
			{
				var localPath = CreateLocalPath(product);

				var image = new Image()
				{
					Name = $"{product.ImageFile.Name}",
					Path = localPath,
					AbsolutePath = Path.Combine(_webHostEnvironment.WebRootPath, localPath)
				};

				using var stream = new FileStream(image.AbsolutePath, FileMode.Create);
				await product.ImageFile.CopyToAsync(stream);

				await _dbContext.Images.AddAsync(image);
				await _dbContext.SaveChangesAsync();

				id = image.Id;
			}
			
			await _productRepo.Add(product, id);
		}

		public async Task<string> GetLogoPath()
		{
				var path = (await _dbContext.Images.FirstOrDefaultAsync(x => x.Id == 1)).Path;
				return path;			
		}

		private string CreateLocalPath(BaseImageModel? model)
		{
			string? fileName = Path.GetFileNameWithoutExtension(model.ImageFile.FileName);
			string? extension = Path.GetExtension(model.ImageFile.FileName);
			fileName = fileName + DateTime.Now.ToString("yymmssffff") + extension;
			fileName = $"images/{fileName}";

			return fileName;
		}

		
	}
}
