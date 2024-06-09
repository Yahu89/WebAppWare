using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using WebAppWare.Database.Entities.Enums;
using WepAppWare.Database.Entities;

namespace WebAppWare.Models;

public class OrderModel
{
	public int Id { get; set; }

	[Required(ErrorMessage = "Nazwa dokumentu jest wymagana")]
	public string Document { get; set; }
	public int SupplierId { get; set; }
	public string SupplierName { get; set; }
	public string SupplierEmail { get; set; }
	public DateTime CreationDate { get; set; } = DateTime.Now;

	public OrderStatus Status { get; set; }
	public string StatusName { get; set; }
	public string Remarks { get; set; }

	public bool IsEdit { get; set; }

	public List<OrderDetailsModel> OrderDetails { get; set; } = new List<OrderDetailsModel>();

	#region combo boxes
	public IEnumerable<SelectListItem> Products { get; set; }
	public IEnumerable<SelectListItem> Suppliers { get; set; }
	public IEnumerable<SelectListItem> StatusList { get; set; }
    #endregion

    public IEnumerable<SelectListItem> ComboList { get; set; }
}
