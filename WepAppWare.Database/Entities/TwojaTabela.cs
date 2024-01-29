using System;
using System.Collections.Generic;

namespace WebAppWare.Database.Entities;

public partial class TwojaTabela
{
    public int Id { get; set; }

    public string? NazwaProduktu { get; set; }

    public int? Ilosc { get; set; }
}
