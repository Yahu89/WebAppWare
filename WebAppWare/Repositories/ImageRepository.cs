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

				//var absolutePath = Path.Combine(_webHostEnvironment.WebRootPath, localPath);

				using var stream = new FileStream(image.AbsolutePath, FileMode.Create);
				await product.ImageFile.CopyToAsync(stream);

				//var absolutePath = CreateImageAbsolutePath(product);

				// zapisyjemy do folderu wwwroot
				

				// zapisujem w bazie dane zapisanego pliku
				//var image = new Image()
				//{
				//	Name = $"{product.ImageFile.Name}",
				//	AbsolutePath = absolutePath,
				//	Path = $"images/{product.ImageFile.Name}"
				//};

				//image.AbsolutePath = absolutePath;

				

				await _dbContext.Images.AddAsync(image);
				await _dbContext.SaveChangesAsync();

				id = image.Id;
			}
			
			await _productRepo.Add(product, id);
		}

		//private string CreateImageAbsolutePath(BaseImageModel? model)
		//{
		//	//string? fileName = Path.GetFileNameWithoutExtension(model.ImageFile.FileName);
		//	//string? extension = Path.GetExtension(model.ImageFile.FileName);
		//	//fileName = fileName + DateTime.Now.ToString("yymmssffff") + extension;
		//	string fileName = Path.Combine(_webHostEnvironment.WebRootPath, "images");

		//	return fileName;
		//}

		private string CreateLocalPath(BaseImageModel? model)
		{
			string? fileName = Path.GetFileNameWithoutExtension(model.ImageFile.FileName);
			string? extension = Path.GetExtension(model.ImageFile.FileName);
			fileName = fileName + DateTime.Now.ToString("yymmssffff") + extension;
			//fileName = Path.Combine("images", fileName);
			fileName = $"images/{fileName}";

			return fileName;
		}
	}
}
