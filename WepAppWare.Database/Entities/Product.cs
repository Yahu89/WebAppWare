using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WebAppWare.Database.Entities;

public partial class Product
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Pole Indeks jest wymagane")]
    [MinLength(3)]
    [MaxLength(15)]
    public string ItemCode { get; set; } = string.Empty;

    [Required(ErrorMessage = "Pole Opis jest wymagane")]
    [MaxLength(200)]
    public string Description { get; set; } = string.Empty;

    public string ImgUrl { get; set; } = string.Empty;

    public virtual ICollection<ProductsFlow> ProductsFlows { get; set; } = new List<ProductsFlow>();
}
