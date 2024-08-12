
using WebAppWare.Database.Entities;
using WepAppWare.Database.Entities.Base;

namespace WebAppWare.Database;

public partial class WarehouseMovement : BaseEntity
{
    public string Document { get; set; } = null!;
    public DateTime CreationDate { get; set; }
    public int MovementType { get; set; }
    public virtual ICollection<ProductsFlow> ProductsFlows { get; set; } = new List<ProductsFlow>();
}
