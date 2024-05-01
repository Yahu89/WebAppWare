using System;
using System.Collections.Generic;
using WepAppWare.Database.Entities.Base;

namespace WebAppWare.Database.Entities;

public partial class WarehouseMovement : BaseEntity
{
    //public int Id { get; set; }

    public string Document { get; set; } = null!;

    public DateTime CreationDate { get; set; }

    public int MovementType { get; set; }

    public int WarehouseId { get; set; }

    public virtual ICollection<ProductsFlow> ProductsFlows { get; set; } = new List<ProductsFlow>();

    public virtual Warehouse Warehouse { get; set; } = null!;
}
