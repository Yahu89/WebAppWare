using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WebAppWare.Api.Dto;
using WebAppWare.Api.Repositories;

namespace WebAppWare.Api.Controllers;

[Route("api/[controller]")]
public class OrderController : Controller
{
    private readonly IOrderRepository _orderRepository;
    private readonly IMapper _mapper;

    public OrderController(IOrderRepository orderRepository,
                            IMapper mapper)
    {
        _orderRepository = orderRepository;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var items = await _orderRepository.GetAll();
        return Ok(items);
    }

    [HttpPost]
    [Route("create")]
    public async Task<IActionResult> Create([FromBody]OrderCreateDto dto)
    {
        if (ModelState.IsValid)
        {
            await _orderRepository.Create(dto);
            return Ok();
        }

        return BadRequest(ModelState);
    }

    [HttpDelete]
    [Route("delete/{id}")]
    public async Task<IActionResult> Delete([FromRoute]int id)
    {
        var order = await _orderRepository.GetById(id);
        await _orderRepository.Delete(order);
        return Ok();
    }
}
