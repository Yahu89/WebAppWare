using System;
using System.Collections.Generic;
using WepAppWare.Database.Entities.Base;

namespace WebAppWare.Database.Entities;

public partial class ProductsFlow : BaseEntity
{

    public int Quantity { get; set; }

    public int WarehouseMovementId { get; set; }

    public int ProductId { get; set; }

    public int? SupplierId { get; set; }

    public virtual Product Product { get; set; }

    public virtual Supplier? Supplier { get; set; }

    public virtual WarehouseMovement WarehouseMovement { get; set; } = null!;
    public virtual Warehouse Warehouse { get; set; }
    public int WarehouseId { get; set; }
}
