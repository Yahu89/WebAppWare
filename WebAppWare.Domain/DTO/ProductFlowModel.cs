using System;
using System.Collections.Generic;
using WebAppWare.Domain.Entities;

namespace WebAppWare.Infrastructure;

public partial class ProductFlowModel
{
    public int Id { get; set; }

    public int MoveId { get; set; }

    public MovementType MovementType { get; set; }

    public string Warehouse { get; set; } = string.Empty;

    public string ItemCode { get; set; } = string.Empty;

    public int Quantity { get; set; }

    public string Supplier { get; set; } = string.Empty;

    public DateTime CreationDate { get; set; }
    public int Cumulative { get; set; }
}
