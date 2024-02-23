namespace WebAppWare.Models;

public class OrderDetailsModel
{
    public int Id { get; set; }
    public int OrderId { get; set; }
    public string Document { get; set; }
    public string SupplierName { get; set; }
    public int SupplierId { get; set; }
    public string SupplierEmail { get; set; }
    public DateTime CreationDate { get; set; }
    public int ProductId { get; set; }
    public string ProductItemCode { get; set; }
    public string Status { get; set; }
    public string Remarks { get; set; }
    public int Quantity { get; set; }
}
