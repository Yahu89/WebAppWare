using System.Collections;

namespace WebAppWare.Models;

public class Form
{
    //public int Id { get; set; }
    public string Document { get; set; }
    public int WarehouseId { get; set; }
    public PzArrayItem[] ProductFlowModels { get; set; } = new PzArrayItem[3];
}
