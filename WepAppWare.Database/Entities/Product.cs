using System;
using System.Collections.Generic;
using WepAppWare.Database.Entities.Base;

namespace WebAppWare.Database.Entities;

public partial class Product : BaseEntity
{
    public string ItemCode { get; set; } = null!;
    public string Description { get; set; } = null!;
    public int? ImageId { get; set; }
    public virtual Image? Image { get; set; }
    public virtual ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
    public virtual ICollection<ProductsFlow> ProductsFlows { get; set; } = new List<ProductsFlow>();
}
