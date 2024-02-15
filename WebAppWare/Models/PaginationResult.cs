namespace WebAppWare.Models;

public class PaginationResult
{
    public int ResultsPerPage { get; set; }
    public int PagesQuantity { get; set; }
    public int CurrentPageNumber { get; set; }
    public List<ProductFlowModel> ProductFlows { get; set; }
}
