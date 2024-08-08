using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using WebAppWare.Database;
using WebAppWare.Database.Entities;
using WebAppWare.Models;
using WebAppWare.Models.BaseModels;
using WebAppWare.Repositories.Interfaces;
using WepAppWare.Database.Entities;

namespace WebAppWare.Repositories
{
	public class ImageRepository : IImageRepository
	{
		private readonly WarehouseBaseContext _dbContext;
		private readonly IProductRepo _productRepo;
		private readonly IWebHostEnvironment _webHostEnvironment;

		public ImageRepository(
			WarehouseBaseContext dbContext,
			IProductRepo productRepo,
			IWebHostEnvironment webHostEnvironment
			)
		{
			_dbContext = dbContext;
			_productRepo = productRepo;
			_webHostEnvironment = webHostEnvironment;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="product"></param>
		/// <returns></returns>
		public async Task Create(ProductModel product)
		{
			int? imageId = null;

			if (product.ImageFile != null)
			{
				imageId = await CreateImage(product);
			}

			await _productRepo.Add(product, imageId);
		}

		public async Task Update(ProductModel product)
		{
			int? imageId = null;
			var temp = product.ImageFile;

			if (product.ImageFile != null)
			{
				imageId = await CreateImage(product);
				product.ImageId = imageId;
			}

			await _productRepo.Update(product);
		}

		public async Task<int> CreateImage(ProductModel product)
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

			int id = image.Id;

			return id;
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
