using WebAppWare.Database;
using WebAppWare.Database.Entities;

namespace WebAppWare.Models;

public class ProductFlowModel
{
    public int Id { get; set; }
    public WarehouseMovement Movement { get; set; }
    public int MovementId { get; set; }
    public string DocumentNumber { get; set; }
    public MovementType MovementType { get; set; }

    public string Warehouse { get; set; }
    public int? WarehouseId { get; set; }
    public Product Product { get; set; }

    public string ProductItemCode { get; set; }
	public int? ProductId { get; set; }

	public int Quantity { get; set; }

    public string Supplier { get; set; }
    public int? SupplierId { get; set; }

    public DateTime CreationDate { get; set; }
    public int Cumulative { get; set; }
    public int? WarehouseToId { get; set; }
    public string? WarehouseToIdName { get; set; }
}
