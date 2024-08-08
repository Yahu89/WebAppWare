namespace WebAppWare.Api.Dto;

public class ProductCreateDto
{
    public string ItemCode { get; set; }
    public string Description { get; set; }
    public IFormFile? ImageFile { get; set; }
}
