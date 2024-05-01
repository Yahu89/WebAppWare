using System;
using System.Collections.Generic;
using WepAppWare.Database.Entities.Base;

namespace WebAppWare.Database.Entities;

public partial class Order : BaseEntity
{
    //public int Id { get; set; }

    public string Document { get; set; } = null!;

    public int SupplierId { get; set; }

    public DateTime CreationDate { get; set; }

    public int Status { get; set; }

    public string? Remarks { get; set; }

    public virtual ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();

    public virtual Supplier Supplier { get; set; } = null!;
}
