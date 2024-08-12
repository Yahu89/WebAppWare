using WebAppWare.Database;
using WebAppWare.Database.Entities;

namespace WebAppWare.Models;

public class ProductFlowModel
{
    // For map from entity
    public int Id { get; set; }
    public int Quantity { get; set; }
    public WarehouseMovement Movement { get; set; }
    public int MovementId { get; set; }
    public string DocumentNumber { get; set; }
    public MovementType MovementType { get; set; }
    public DateTime CreationDate { get; set; }
    public Product Product { get; set; }
    public int ProductId { get; set; }
    public string ProductItemCode { get; set; }
    public Supplier? Supplier { get; set; }
    public int? SupplierId { get; set; }
    public string? SupplierName { get; set; }
    public Warehouse Warehouse { get; set; }
    public string WarehouseName { get; set; }
    public int WarehouseId { get; set; }
    
    // Additionals
    public int Cumulative { get; set; }
    public string? WarehouseToIdName { get; set; }

    // For searching feature
    public IEnumerable<ProductFlowModel> ProductsFlow { get; set; }
    public string SearchWarehouse { get; set; }
    public string SearchItemCode { get; set; }
    public string SearchSupplier { get; set; }
    public int CurrentPage { get; set; } = 1;
    public int TotalPages { get; set; }
    public string WarehouseToId { get; set; }
}
