﻿using System.ComponentModel.DataAnnotations;
using WepAppWare.Database.Entities;

namespace WebAppWare.Models;

public class OrderModel
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Nazwa dokumentu jest wymagana")]
    public string Document { get; set; }
    public int SupplierId { get; set; }
    public string SupplierName { get; set; }
    public string SupplierEmail { get; set; }
    public DateTime CreationDate { get; set; } = DateTime.Now;

	[Required(ErrorMessage = "Ustawienie statusu jest wymagane")]
	public int StatusId { get; set; }
    public string Status { get; set; }
    public string Remarks { get; set; }
}
