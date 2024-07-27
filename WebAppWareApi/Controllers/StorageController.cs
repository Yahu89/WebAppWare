using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAppWare.Database;
using WebAppWare.Models;
using WebAppWare.Repositories.Interfaces;
using WebAppWareApi.Dto;

namespace WebAppWareApi.Controllers;

[Route("api/[controller]")]
public class StorageController : Controller
{
	private readonly WarehouseBaseContext _dbContext;
	private readonly IWarehouseRepo _warehouseRepo;
	public StorageController(WarehouseBaseContext dbContext, 
							IWarehouseRepo warehouseRepo)
    {
		_dbContext = dbContext;
		_warehouseRepo = warehouseRepo;
	}

	[HttpGet]
    public async Task<ActionResult<IEnumerable<ProductFlowModel>>> GetAllItems()
	{
		var items = await _warehouseRepo.GetProductsAmount();
		var results = items.Select(x => new TotalAmountDto()
		{
			ItemCode = x.ProductItemCode,
			Warehouse = x.WarehouseName,
			Cumulative = x.Cumulative
		})
			.Where(x => x.Cumulative != 0);

		return Ok(results);
	}

	[HttpPost]
	public async Task<ActionResult> Create([FromBody]WarehouseModel model)
	{
		if (ModelState.IsValid)
		{
			await _warehouseRepo.Add(model);
			return Created("Utworzono pomyślnie", null);
		}
		
		return BadRequest(model);
	}

	[HttpDelete("{id}")]
	[Authorize(Roles = "warehouse")]
	public async Task<ActionResult> Delete([FromRoute]int id)
	{
		var warehouseToDelete = await _warehouseRepo.GetById(id);

		if (warehouseToDelete is null)
		{
			return BadRequest();
		}

		await _warehouseRepo.Delete(warehouseToDelete);
		return NoContent();	
	}
}
