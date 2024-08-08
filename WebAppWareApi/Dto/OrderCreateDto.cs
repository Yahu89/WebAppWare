namespace WebAppWare.Api.Dto;

public class OrderCreateDto
{
    public int Id { get; set; }
    public string Document { get; set; }
    public DateTime CreationDate { get; set; }
    public int SupplierId { get; set; }
    public int Status { get; set; }
    public string? Remarks { get; set; }
    public List<OrderItemCreateDto> OrderItemCreateDtos { get; set; } = new List<OrderItemCreateDto>();
}
