

namespace WebAppWare.Database.Entities;

public partial class ProductSummaryModel
{
    public string ItemCode { get; set; } = null!;

    public string Warehouse { get; set; } = null!;

    public int? TotalAmount { get; set; }
}
