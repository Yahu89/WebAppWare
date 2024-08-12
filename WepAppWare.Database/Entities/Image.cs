
using WepAppWare.Database.Entities.Base;

namespace WebAppWare.Database.Entities;

public partial class Image : BaseEntity
{
    public string Name { get; set; } = null!;
    public string Path { get; set; } = null!;
    public string AbsolutePath { get; set; } = null!;
    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}
