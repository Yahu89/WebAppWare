using System;
using System.Collections.Generic;

namespace WebAppWare.Database.Entities;

public partial class ProductsAmountListView
{
    public int ProductId { get; set; }

    public int WarehouseId { get; set; }

    public int? TotalAmount { get; set; }
}
