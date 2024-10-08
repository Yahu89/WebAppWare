﻿using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebAppWare.Models;


public class ProductFlowMovementModel
{
    public IEnumerable<SelectListItem> Products { get; set; }
    public IEnumerable<SelectListItem> Suppliers { get; set; }
    public IEnumerable<SelectListItem> Warehouses { get; set; }
    public List<ProductFlowModel> ProductFlowModels { get; set; } = new List<ProductFlowModel>();
    public string Warehouse { get; set; }
    public int WarehouseId { get; set; }
    public string WarehouseTo { get; set; }
    public int WarehouseToId { get; set; }
    public string Document { get; set; }
    public string SupplierName { get; set; }
}
