using AutoMapper;
using WebAppWare.Database.Entities;

namespace WebAppWare.Models.MappingProfiles;

public class WarehouseMappingProfile : Profile
{
    public WarehouseMappingProfile()
    {
        CreateMap<Warehouse, WarehouseModel>();

        CreateMap<WarehouseModel, Warehouse>();
    }
}
