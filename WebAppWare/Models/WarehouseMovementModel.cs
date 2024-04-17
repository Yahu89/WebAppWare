using System.ComponentModel.DataAnnotations;
using WebAppWare.Database.Entities;

namespace WebAppWare.Models;

public class WarehouseMovementModel
{
    public int Id { get; set; }

    public string Document { get; set; }
    public MovementType MovementType { get; set; }
    public DateTime CreationDate { get; set; } = DateTime.Now;

    public IEnumerable<ProductFlowModel> ProductsFlowModels { get; set; } = new List<ProductFlowModel>();
}
