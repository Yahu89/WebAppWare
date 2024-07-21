using AutoMapper;
using WebAppWare.Database;
using WebAppWare.Database.Entities;

namespace WebAppWare.Models.MappingProfiles;

public class ProductFlowMappingProfile : Profile
{
    public ProductFlowMappingProfile()
    {
        CreateMap<ProductsFlow, ProductFlowModel>()
                .ForMember(x => x.Movement, opt => opt.MapFrom(src => new WarehouseMovement()
                {
                    Id = src.WarehouseMovement.Id,
                    CreationDate = src.WarehouseMovement.CreationDate,
                    MovementType = src.WarehouseMovement.MovementType,
                    Document = src.WarehouseMovement.Document
                }))
                .ForMember(x => x.Product, opt => opt.MapFrom(src => new Product()
                {
                    Id = src.Product.Id,
                    ItemCode = src.Product.ItemCode
                }))
                .ForMember(x => x.Supplier, opt => opt.MapFrom(src => new Supplier()
                {
                    Id = src.Supplier.Id,
                    Name = src.Supplier.Name,                
                }))
                .ForMember(x => x.Warehouse, opt => opt.MapFrom(src => new Warehouse()
                {
                    Id = src.Warehouse.Id,
                    Name = src.Warehouse.Name
                }));
    }
}
