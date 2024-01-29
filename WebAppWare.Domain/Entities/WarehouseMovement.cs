using System;
using System.Collections.Generic;
using WebAppWare.Domain.Entities;

namespace WebAppWare.Infrastructure;

public partial class WarehouseMovement
{
    public int Id { get; set; }

    public string Document { get; set; } = string.Empty;

    public DateTime CreationDate { get; set; } = DateTime.Now;

    public MovementType MovementType { get; set; }

    public virtual ICollection<ProductsFlow> ProductsFlows { get; set; } = new List<ProductsFlow>();
}
