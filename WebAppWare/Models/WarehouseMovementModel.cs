using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using WebAppWare.Database.Entities;

namespace WebAppWare.Models;

public class WarehouseMovementModel
{
    public int Id { get; set; }
	public int WarehouseId { get; set; }
    public Warehouse Warehouse { get; set; }
    public int WarehouseToId { get; set; }

    [Required(ErrorMessage = "Pole wymagane")]
    public string Document { get; set; }
    public MovementType MovementType { get; set; }
    public DateTime CreationDate { get; set; } = DateTime.Now;
	public IEnumerable<SelectListItem> Products { get; set; }
	public IEnumerable<SelectListItem> Suppliers { get; set; }
	public IEnumerable<SelectListItem> Warehouses { get; set; }

	public IEnumerable<ProductFlowModel> ProductFlowModels { get; set; } = new HashSet<ProductFlowModel>();
}
