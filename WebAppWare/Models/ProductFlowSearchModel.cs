﻿namespace WebAppWare.Models;

public class ProductFlowSearchModel
{
    public IEnumerable<ProductFlowModel> ProductsFlow { get; set; }
    public string Warehouse { get; set; }
    public string ItemCode { get; set; }
    public string Supplier { get; set; }
}
