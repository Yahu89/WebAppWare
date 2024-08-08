using AutoMapper.Configuration.Annotations;
using System.Text.Json.Serialization;
using WebAppWare.Database.Entities;

namespace WebAppWare.Api.Dto;

public class OrderDetailsDto
{
    [Ignore]
    [JsonIgnore]
    public Product Product { get; set; }

    [JsonIgnore]
    public int ProductId { get; set; }
    public string ItemCode { get; set; }
    public int Quantity { get; set; }

    [JsonIgnore]
    public int OrderId { get; set; }
}
