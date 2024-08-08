using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebAppWare.Api.Dto;
using WebAppWare.Api.Repositories;

namespace WebAppWare.Api.Controllers;

[Route("api/[controller]")]
public class WarehouseController : Controller
{
    private readonly IWarehouseRepository _warehouseRepository;
    private readonly IMapper _mapper;

    public WarehouseController(IWarehouseRepository warehouseRepository,
                                IMapper mapper)
    {
        _warehouseRepository = warehouseRepository;
        _mapper = mapper;
    }

    [HttpPost("create")]
    public async Task<IActionResult> Create([FromBody]WarehouseDto dto)
    {
        if (ModelState.IsValid)
        {
            await _warehouseRepository.Create(dto);
            return Ok();
        }

        return BadRequest(ModelState);
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var entities = await _warehouseRepository.GetAll();
        var results = _mapper.Map<IEnumerable<WarehouseDto>>(entities);
        return Ok(results);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById([FromRoute]int id)
    {
        var entity = await _warehouseRepository.GetById(id);
        var result = _mapper.Map<WarehouseDto>(entity);
        return Ok(result);
    }

    [HttpPut]
    [Route("update/{id}")]
    [Authorize(Roles = "admin")]
    public async Task<IActionResult> Update([FromRoute]int id, [FromBody]WarehouseDto dto)
    {
        if (ModelState.IsValid)
        {
            await _warehouseRepository.Edit(id, dto);
            return Ok();
        }
        
        return BadRequest(ModelState);
    }

    [HttpDelete]
    [Route("delete/{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _warehouseRepository.Delete(id);
        return Ok();
    }
}
