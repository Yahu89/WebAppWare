

namespace WebAppWare.Api.Dto;

public class OrderDto
{
    public int Id { get; set; }
    public string Document { get; set; }
    public DateTime CreationDate { get; set; }
    public string Status { get; set; }
    public string? Remarks { get; set; }
    public string SupplierName { get; set; }
    public List<OrderDetailsDto> OrderDetails { get; set; } = new List<OrderDetailsDto>();
}
