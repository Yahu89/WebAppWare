using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using WepAppWare.Database.Entities.Base;

namespace WebAppWare.Database.Entities;

public partial class Warehouse : BaseEntity
{
    //[DisplayName("Nazwa")]
    //[Required(ErrorMessage = "Pole jest wymagane")]
    //[MaxLength(100)]
    
    public string Name { get; set; }

    //[DisplayName("Czy aktywny")]
    //[Required]
    public bool IsActive { get; set; } = true;

    public virtual ICollection<ProductsFlow> ProductsFlows { get; set; } = new List<ProductsFlow>();
}
