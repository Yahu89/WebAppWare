using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WebAppWare.Database.Entities;

public partial class Supplier
{
    public int Id { get; set; }

    //[Required(ErrorMessage = "Pole Nazwa jest wymagane")]
    //[MaxLength(150)]
    public string Name { get; set; } = string.Empty;

    //[Required(ErrorMessage = "Pole e-mail jest wymagane")]
    //[MaxLength(150)]
    public string Email { get; set; } = string.Empty;

    public virtual ICollection<ProductsFlow> ProductsFlows { get; set; } = new List<ProductsFlow>();
}
