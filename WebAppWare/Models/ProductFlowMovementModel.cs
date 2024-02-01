using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using WebAppWare.Database.Entities;

namespace WebAppWare.Models;


public class ProductFlowMovementModel
{
    public IEnumerable<SelectListItem> Products { get; set; }
    public IEnumerable<SelectListItem> Suppliers { get; set; }
    public IEnumerable<SelectListItem> Warehouses { get; set; }
    public ProductFlowModel[] ProductFlowModels { get; set; } = new ProductFlowModel[]
    {
        new ProductFlowModel(),
        new ProductFlowModel(),
        new ProductFlowModel(),
        new ProductFlowModel(),
        new ProductFlowModel()
    };

    //[Required(ErrorMessage = "Pole Nazwa magazynu jest wymagane")]
    public string Warehouse { get; set; }

    //[Required(ErrorMessage = "Pole Nr dokumentu jest wymagane")]
    public string Document { get; set; }
}
