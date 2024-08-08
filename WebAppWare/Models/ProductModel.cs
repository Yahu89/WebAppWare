using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Web;
using WebAppWare.Models.BaseModels;
using WebAppWare.Database.Entities;

namespace WebAppWare.Models
{
	public class ProductModel : BaseImageModel
	{
		public string ItemCode { get; set; }
		public string Description { get; set; }
        public string? ImageName { get; set; }
        public string? ImagePath { get; set; }
        public Image? Image { get; set; }
        public IFormFile? ImageFile { get; set; }
    }
}
