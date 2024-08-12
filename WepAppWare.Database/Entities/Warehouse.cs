
using WepAppWare.Database.Entities.Base;

namespace WebAppWare.Database.Entities;

public partial class Warehouse : BaseEntity
{
    public string Name { get; set; } = null!;
    public bool IsActive { get; set; }
	public virtual ICollection<ProductsFlow> ProductsFlows { get; set; } = new List<ProductsFlow>();
}
