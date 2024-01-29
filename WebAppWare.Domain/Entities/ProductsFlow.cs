using System;
using System.Collections.Generic;

namespace WebAppWare.Infrastructure;

public partial class ProductsFlow
{
    public int Id { get; set; }

    public int Quantity { get; set; } = 0;

    public int WarehouseMovementId { get; set; }

    public int? WarehouseId { get; set; }

    public int? ProductId { get; set; } = null;

    public int? SupplierId { get; set; } = null;

    public virtual Product Product { get; set; } = null!;

    public virtual Supplier Supplier { get; set; } = null!;

    public virtual Warehouse Warehouse { get; set; } = null!;

    public virtual WarehouseMovement WarehouseMovement { get; set; } = null!;
}
