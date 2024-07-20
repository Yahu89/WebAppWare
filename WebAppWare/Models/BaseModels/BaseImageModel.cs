namespace WebAppWare.Models.BaseModels
{
	public class BaseImageModel : BaseModel
	{
        public int? ImageId { get; set; }
        public IFormFile? ImageFile { get; set; }
		public string? ImagePath { get; set; }
	}
}
