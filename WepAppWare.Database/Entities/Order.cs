using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAppWare.Database.Entities;
using WepAppWare.Database.Entities.Base;

namespace WepAppWare.Database.Entities;

public class Order : BaseEntity
{
    public string Document { get; set; }
    public int SupplierId { get; set; }
    public Supplier Supplier { get; set; }
    public DateTime CreationDate { get; set; } = DateTime.Now;
    public OrderStatus Status { get; set; }
    public string? Remarks { get; set; }

    public IEnumerable<OrderItem> Items { get; set; } = new HashSet<OrderItem>();
}
