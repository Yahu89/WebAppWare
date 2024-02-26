using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using WepAppWare.Database.Entities;
using WepAppWare.Database.Entities.Base;

namespace WebAppWare.Database.Entities;

public partial class Product : BaseEntity
{
    //[Required(ErrorMessage = "Pole Indeks jest wymagane")]
    //[MinLength(3)]
    //[MaxLength(15)]
    public string ItemCode { get; set; } = string.Empty;

    //[Required(ErrorMessage = "Pole Opis jest wymagane")]
    //[MaxLength(200)]
    public string Description { get; set; } = string.Empty;

    public int? ImageId { get; set; }
    public Image? Image { get; set; }

    public virtual ICollection<ProductsFlow> ProductsFlows { get; set; } = new List<ProductsFlow>();
}
