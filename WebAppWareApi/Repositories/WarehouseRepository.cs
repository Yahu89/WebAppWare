using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAppWare.Api.Dto;
using WebAppWare.Api.Middleware;
using WebAppWare.Database;
using WebAppWare.Database.Entities;

namespace WebAppWare.Api.Repositories;

public class WarehouseRepository : IWarehouseRepository
{
    private readonly WarehouseBaseContext _dbContex;
    private readonly IMapper _mapper;

    public WarehouseRepository(WarehouseBaseContext dbContex,
                                IMapper mapper)
    {
        _dbContex = dbContex;
        _mapper = mapper;
    }

    public async Task Create(WarehouseDto dto)
    {
        var entity = _mapper.Map<Warehouse>(dto);
        _dbContex.Warehouses.Add(entity);
        await _dbContex.SaveChangesAsync();
    }

    public async Task<IEnumerable<Warehouse>> GetAll()
    {
        var entities = await _dbContex.Warehouses.ToListAsync();

        if (!entities.Any())
        {
            throw new NoContentException();
        }

        //var results = _mapper.Map<IEnumerable<WarehouseDto>>(entities);

        return entities;
    }

    public async Task<Warehouse> GetById(int id)
    {
        var entity = await _dbContex.Warehouses.Where(x => x.Id == id).FirstOrDefaultAsync();

        if (entity is null)
        {
            throw new NoContentException();
        }

        //var result = _mapper.Map<WarehouseDto>(entity);
        return entity;
    }

    public async Task Edit(int id, WarehouseDto dto)
    {
        var entity = await GetById(id);

        if (entity is null)
        {
            throw new NoContentException();
        }

        entity.Name = dto.Name;
        entity.IsActive = dto.IsActive;

        _dbContex.Warehouses.Update(entity);
        await _dbContex.SaveChangesAsync();
    }

    public async Task Delete(int id)
    {
        var entity = await _dbContex.Warehouses.FirstOrDefaultAsync(x => x.Id == id);

        if (entity is null)
        {
            throw new NoContentException();
        }

        _dbContex.Warehouses.Remove(entity);
        await _dbContex.SaveChangesAsync();
    }
}
