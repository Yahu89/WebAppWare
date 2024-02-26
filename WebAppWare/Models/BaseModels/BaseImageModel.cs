namespace WebAppWare.Models.BaseModels
{
	public class BaseImageModel : BaseModel
	{
		public IFormFile? ImageFile { get; set; }
		public string? ImagePath { get; set; }
	}
}
