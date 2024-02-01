using System.ComponentModel.DataAnnotations;

namespace WebAppWare.Models;

public class SupplierModel
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Nazwa jest wymagana")]
    public string Name { get; set; }

    [Required(ErrorMessage = "Adres e-mail jest wymagany")]
    public string Email { get; set; }
}
