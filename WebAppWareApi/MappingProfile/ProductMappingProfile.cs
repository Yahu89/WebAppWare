using AutoMapper;
using WebAppWare.Api.Dto;
using WebAppWare.Database.Entities;
using WebAppWare.Models;

namespace WebAppWare.Api.MappingProfile;

public class ProductMappingProfile : Profile
{
    public ProductMappingProfile()
    {
        CreateMap<Product, ProductModel>();

        CreateMap<ProductModel, Product>();

        CreateMap<ProductCreateDto, Product>()
                    .ForMember(x => x.ItemCode, y => y.MapFrom(src => new ProductCreateDto()
                    {
                        ItemCode = src.ItemCode
                    }))
                    .ForMember(x => x.Description, y => y.MapFrom(src => new ProductCreateDto()
                    {
                        Description = src.Description
                    }));

        CreateMap<Product, ProductDto>()
                    .ForMember(x => x.Id, y => y.MapFrom(src => new ProductDto()
                    {
                        Id = src.Id
                    }))
                    .ForMember(x => x.ItemCode, y => y.MapFrom(src => new ProductDto()
                    {
                        ItemCode = src.ItemCode
                    }))
                    .ForMember(x => x.Description, y => y.MapFrom(src => new ProductDto()
                    {
                        Description = src.Description
                    }))
                    .ForMember(x => x.ImageId, y => y.MapFrom(src => new ProductDto()
                    {
                        ImageId = src.ImageId
                    }))
                    .ForMember(x => x.ImageName, y => y.MapFrom(src => new Image()
                    {
                        Name = src.Image.Name
                    }))
                    .ForMember(x => x.ImagePath, y => y.MapFrom(src => new Image()
                    {
                        Path = src.Image.Path
                    }))
                    .ForMember(x => x.ImageAbsolutePath, y => y.MapFrom(src => new Image()
                    {
                        AbsolutePath = src.Image.AbsolutePath
                    }));

        CreateMap<Warehouse, WarehouseDto>();

        CreateMap<WarehouseDto, Warehouse>();

        CreateMap<Supplier, SupplierDto>();

        CreateMap<SupplierDto, Supplier>();

        CreateMap<Order, OrderDto>()
                    .ForMember(x => x.Id, y => y.MapFrom(src => src.Id))
                    .ForMember(x => x.CreationDate, y => y.MapFrom(src => src.CreationDate))
                    .ForMember(x => x.Document, y => y.MapFrom(src => src.Document))
                    .ForMember(x => x.Status, y => y.MapFrom(src => src.Status))
                    .ForMember(x => x.Remarks, y => y.MapFrom(src => src.Remarks))
                    .ForMember(x => x.SupplierName, y => y.MapFrom(src => src.Supplier.Name));

        CreateMap<OrderItem, OrderDetailsDto>()
                    .ForMember(x => x.OrderId, y => y.MapFrom(src => src.OrderId))
                    .ForMember(x => x.ItemCode, y => y.MapFrom(src => src.Product.ItemCode))
                    .ForMember(x => x.Quantity, y => y.MapFrom(src => src.Quantity));

        CreateMap<OrderDto, Order>()
                    .ForMember(x => x.Id, y => y.MapFrom(src => src.Id))
                    .ForMember(x => x.Document, y => y.MapFrom(src => src.Document))
                    .ForMember(x => x.CreationDate, y => y.MapFrom(src => src.CreationDate))
                    .ForMember(x => x.Remarks, y => y.MapFrom(src => src.Remarks))
                    .ForMember(x => x.Status, y => y.MapFrom(src => src.Status));

        CreateMap<OrderCreateDto, Order>()
            .ForMember(x => x.Id, y => y.MapFrom(src => src.Id))
            .ForMember(x => x.Document, y => y.MapFrom(src => src.Document))
            .ForMember(x => x.CreationDate, y => y.MapFrom(src => src.CreationDate))
            .ForMember(x => x.Remarks, y => y.MapFrom(src => src.Remarks))
            .ForMember(x => x.Status, y => y.MapFrom(src => src.Status))
            .ForMember(x => x.SupplierId, y => y.MapFrom(src => src.SupplierId));

        CreateMap<OrderItemCreateDto, OrderItem>()
             .ForMember(x => x.ProductId, y => y.MapFrom(src => src.ProductId))
             .ForMember(x => x.OrderId, y => y.MapFrom(src => src.OrderId))
             .ForMember(x => x.Quantity, y => y.MapFrom(src => src.Quantity));




    }
}
