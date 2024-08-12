
using WebAppWare.Database.Entities.Enums;
using WepAppWare.Database.Entities.Base;

namespace WebAppWare.Database.Entities;

public partial class Order : BaseEntity
{
    public string Document { get; set; } = null!;
    public int SupplierId { get; set; }
    public DateTime CreationDate { get; set; }
    public OrderStatus Status { get; set; }
    public string? Remarks { get; set; }
    public virtual ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
    public virtual Supplier Supplier { get; set; } = null!;
}
