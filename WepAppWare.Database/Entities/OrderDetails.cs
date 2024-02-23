using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAppWare.Database.Entities;

namespace WepAppWare.Database.Entities;

public class OrderDetails
{
    public int Id { get; set; }
    public Order Order { get; set; }
    public int OrderId { get; set; }
    public Product Product { get; set; }
    public int ProductId { get; set; }
    public int Quantity { get; set; }
}
