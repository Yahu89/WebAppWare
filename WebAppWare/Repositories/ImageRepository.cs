using Microsoft.AspNetCore.Hosting;
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
			var absolutePath = CreateImageAbsolutePath(product);

			using var stream = new FileStream(absolutePath, FileMode.Create);

			// zapisyjemy do folderu wwwroot
			await product.ImageFile.CopyToAsync(stream);

			// zapisujem w bazie dane zapisanego pliku
			var image = new Image()
			{
				Name = $"{product.ImageFile.Name}",
				AbsolutePath = absolutePath,
				Path = $"images/{product.ImageFile.Name}"
			};

			await _dbContext.Images.AddAsync(image);
			await _dbContext.SaveChangesAsync();

			await _productRepo.Add(product, image.Id);
		}

		private string CreateImageAbsolutePath(BaseImageModel model)
		{
			string fileName = Path.GetFileNameWithoutExtension(model.ImageFile.FileName);
			string extension = Path.GetExtension(model.ImageFile.FileName);
			fileName = fileName + DateTime.Now.ToString("yymmssffff") + extension;
			fileName = Path.Combine(_webHostEnvironment.ContentRootPath, "images", fileName);

			return fileName;
		}
	}
}
