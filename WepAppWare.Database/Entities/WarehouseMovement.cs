using System;
using System.Collections.Generic;
using WepAppWare.Database.Entities.Base;

namespace WebAppWare.Database.Entities;

public partial class WarehouseMovement : BaseEntity
{
    public string Document { get; set; } = string.Empty;

    public DateTime CreationDate { get; set; } = DateTime.Now;

    public MovementType MovementType { get; set; }

    public virtual ICollection<ProductsFlow> ProductsFlows { get; set; } = new List<ProductsFlow>();
}
