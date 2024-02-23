using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAppWare.Database.Entities;

namespace WepAppWare.Database.Entities;

public class Order
{
    public int Id { get; set; }
    public string Document { get; set; }
    public Supplier Supplier { get; set; }
    public int SupplierId { get; set; }
    public DateTime CreationDate { get; set; } = DateTime.Now;
    public string Status { get; set; }
    public string Remarks { get; set; }
    public ICollection<OrderDetails> OrderDetails { get; set; } = new List<OrderDetails>();
}
