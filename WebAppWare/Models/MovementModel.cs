using System.ComponentModel.DataAnnotations;
using WebAppWare.Database.Entities;

namespace WebAppWare.Models;

public class MovementModel
{
    public int Id { get; set; }

    //[Required(ErrorMessage = "dfgdgd")]
    public string Document { get; set; }
    public MovementType MovementType { get; set; }
    public DateTime CreationDate { get; set; } = DateTime.Now;
}
