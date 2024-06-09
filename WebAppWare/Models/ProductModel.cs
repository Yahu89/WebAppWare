using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Web;
using WebAppWare.Models.BaseModels;

namespace WebAppWare.Models
{
	public class ProductModel : BaseImageModel
	{
		[Required(ErrorMessage = "Pole Indeks jest wymagane")]
		public string? ItemCode { get; set; }

		[Required(ErrorMessage = "Pole Opis jest wymagane")]
		public string? Description { get; set; }
    }
}
