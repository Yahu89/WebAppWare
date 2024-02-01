using WebAppWare.Database.Entities;

namespace WebAppWare.Models;

public class ProductFlowModel
{
    public int Id { get; set; }
    public int MovementId { get; set; }
    public MovementType MovementType { get; set; }
    public string Warehouse { get; set; }
    public string ItemCode { get; set; }
    public int Quantity { get; set; }
    public string Supplier { get; set; }
    public DateTime CreationDate { get; set; }
    public int Cumulative { get; set; }
    //public Warehouse _Warehouse { get; set; }
}
