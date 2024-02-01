using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace WebAppWare.Database.Entities;

public partial class Warehouse
{
    public int Id { get; set; }

    //[DisplayName("Nazwa")]
    //[Required(ErrorMessage = "Pole jest wymagane")]
    //[MaxLength(100)]
    
    public string Name { get; set; }

    //[DisplayName("Czy aktywny")]
    //[Required]
    public bool IsActive { get; set; } = true;

    public virtual ICollection<ProductsFlow> ProductsFlows { get; set; } = new List<ProductsFlow>();
}
