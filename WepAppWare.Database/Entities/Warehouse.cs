using System;
using System.Collections.Generic;
using WepAppWare.Database.Entities.Base;

namespace WebAppWare.Database.Entities;

public partial class Warehouse : BaseEntity
{
    public string Name { get; set; } = null!;

    public bool IsActive { get; set; }

    //public virtual ICollection<WarehouseMovement> WarehouseMovements { get; set; } = new List<WarehouseMovement>();
	public virtual ICollection<ProductsFlow> ProductsFlows { get; set; } = new List<ProductsFlow>();
}
