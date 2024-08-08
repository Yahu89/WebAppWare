using AutoMapper;
using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using WebAppWare.Api.Dto;
using WebAppWare.Api.Middleware;
using WebAppWare.Database;
using WebAppWare.Database.Entities;

namespace WebAppWare.Api.Repositories;

public class OrderRepository : IOrderRepository
{
    private readonly WarehouseBaseContext _dbContext;
    private readonly IMapper _mapper;
    public OrderRepository(WarehouseBaseContext dbContext,
                            IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public async Task<List<OrderDto>> GetAll()
    {
        var ordersEntitiy = await _dbContext.Orders.Include(x => x.Supplier)
                                                    .ToListAsync();

        if (!ordersEntitiy.Any())
        {
            throw new NoContentException();
        }

        var orders = _mapper.Map<List<OrderDto>>(ordersEntitiy);
        var orderItemsEntities = await _dbContext.OrderItems.Include(x => x.Product)
                                                            .ToListAsync();

        var orderItems = _mapper.Map<List<OrderDetailsDto>>(orderItemsEntities);

        foreach (var ord in orders)
        {
            foreach (var ordItem in orderItems)
            {
                if (ord.Id == ordItem.OrderId)
                {
                    ord.OrderDetails.Add(ordItem);
                }
            }
        }                  

        return orders;
    }

    public async Task<Order> GetById(int id)
    {
        var result = await _dbContext.Orders.FirstOrDefaultAsync(x => x.Id == id);

        if (result is null)
        {
            throw new NoContentException();
        }

        return result;
    }

    public async Task Create(OrderCreateDto orderCreateDto)
    {
        if (orderCreateDto is null)
        {
            throw new Middleware.InvalidDataException();
        }

        var orderEntity = _mapper.Map<Order>(orderCreateDto);
        var orderItemsEntity = _mapper.Map<List<OrderItem>>(orderCreateDto.OrderItemCreateDtos);

        using (var transaction = _dbContext.Database.BeginTransaction())
        {
            try
            {
                _dbContext.Orders.Add(orderEntity);
                await _dbContext.SaveChangesAsync();

                var orderId = orderEntity.Id;
             
                orderItemsEntity.ForEach(x => x.OrderId = orderId);
                _dbContext.OrderItems.AddRange(orderItemsEntity);
                await _dbContext.SaveChangesAsync();

                await transaction.CommitAsync();
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                throw new SqlTransactionFailedException();
            }
            
        }          
    }

    public async Task Delete(Order order)
    {
        if (order is null)
        {
            throw new NoContentException();
        }

        _dbContext.Orders.Remove(order);
        await _dbContext.SaveChangesAsync();
    }

    //private int GetSupplierIdByName(string supplierName)
    //{
    //    if (string.IsNullOrWhiteSpace(supplierName))
    //    {
    //        return 0;
    //    }

    //    var supplier = _dbContext.Suppliers.FirstOrDefault(x => x.Equals(supplierName));

    //    if (supplier is null)
    //    {
    //        return 0;
    //    }

    //    return supplier.Id;
    //}


}
