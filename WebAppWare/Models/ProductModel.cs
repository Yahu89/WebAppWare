using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Web;
using WebAppWare.Models.BaseModels;

namespace WebAppWare.Models
{
	public class ProductModel : BaseImageModel
	{
		public string ItemCode { get; set; }
		public string Description { get; set; }
    }
}
