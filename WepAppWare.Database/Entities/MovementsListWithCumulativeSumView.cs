using System;
using System.Collections.Generic;

namespace WebAppWare.Database.Entities;

public partial class MovementsListWithCumulativeSumView
{
    public int Id { get; set; }

    public int MoveId { get; set; }

    public string Warehouse { get; set; } = null!;

    public string ItemCode { get; set; } = null!;

    public int? Quantity { get; set; }

    public string Supplier { get; set; } = null!;

    public DateTime CreationDate { get; set; }

    public int? Cumulative { get; set; }
}
