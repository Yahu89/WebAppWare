namespace WebAppWare.Api.Dto;

public class ProductDto
{
    public int Id { get; set; }
    public string ItemCode { get; set; }
    public string Description { get; set; }
    public int? ImageId { get; set; }
    public string? ImageName { get; set; }
    public string? ImagePath { get; set; }
    public string? ImageAbsolutePath { get; set; }
}
