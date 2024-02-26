using System;
using System.Collections.Generic;
using WepAppWare.Database.Entities.Base;

namespace WebAppWare.Database.Entities;

public partial class MovementsListWithCumulativeSumView : BaseEntity
{
    public int MoveId { get; set; }

    public string Warehouse { get; set; } = null!;

    public string ItemCode { get; set; } = null!;

    public int? Quantity { get; set; }

    public string Supplier { get; set; } = null!;

    public DateTime CreationDate { get; set; }

    public int? Cumulative { get; set; }
}
