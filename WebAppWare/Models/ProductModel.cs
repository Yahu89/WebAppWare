using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Web;

namespace WebAppWare.Models
{
	public class ProductModel
	{
		public int Id { get; set; }

		[Required(ErrorMessage = "Pole Indeks jest wymagane")]
		public string ItemCode { get; set; }

		[Required(ErrorMessage = "Pole Opis jest wymagane")]
		public string Description { get; set; }
		public string ImgUrl { get; set; } = string.Empty;
        public IFormFile ImageFile { get; set; }
    }
}
