using System.ComponentModel.DataAnnotations;

namespace WebAppWare.Models;

public class WarehouseModel
{
    public int Id { get; set; }

    //[Required(ErrorMessage = "Nazwa magazynu jest wymagana")]
    public string Name { get; set; }
	public bool IsActive { get; set; } = true;
}
