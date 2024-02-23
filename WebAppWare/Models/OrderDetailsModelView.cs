﻿using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebAppWare.Models;

public class OrderDetailsModelView
{
    public List<OrderDetailsModel> OrderDetails { get; set; } = new List<OrderDetailsModel>()
    {
        new OrderDetailsModel(),
        new OrderDetailsModel(),
        new OrderDetailsModel(),
        new OrderDetailsModel(),
        new OrderDetailsModel()
    };
	public IEnumerable<SelectListItem> Products { get; set; }
	public IEnumerable<SelectListItem> Suppliers { get; set; }
    public IEnumerable<SelectListItem> StatusList { get; set; }
    public int OrderId { get; set; }
    public string Document { get; set; }
    public DateTime CreationDate { get; set; }
    public string SupplierName { get; set; }
    public int SupplierId { get; set; }
    public string Status { get; set; }
    public string Remarks { get; set; }
}
