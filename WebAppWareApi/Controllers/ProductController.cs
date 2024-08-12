using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebAppWare.Api.Dto;
using WebAppWare.Api.Middleware;
using WebAppWare.Api.Repositories;
using WebAppWare.Models;
using WebAppWare.Repositories.Interfaces;

namespace WebAppWareApi.Controllers;

[Route("api/[controller]")]
public class ProductController : Controller
{
    private readonly IProductRepo _productRepo;
    private readonly IMapper _mapper;
    private readonly IImageRepository _imageRepository;
    private readonly IProductRepository _productRepository;

    public ProductController(IProductRepo productRepo,
                            IMapper mapper,
                            IImageRepository imageRepository,
                            IProductRepository productRepository)
    {
        _productRepo = productRepo;
        _mapper = mapper;
        _imageRepository = imageRepository;
        _productRepository = productRepository;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var entities = await _productRepo.GetAll();
        var results = _mapper.Map<List<ProductModel>>(entities);
        return Ok(results); 
    }

    [HttpPost] // to fix
    [Authorize(Roles = "admin")]
    public async Task<IActionResult> Create([FromBody]ProductCreateDto model, [FromBody]IFormFile? file)
    {
        int? imageId = null;

        if (ModelState.IsValid)
        {
            if (model.ImageFile != null)
            {
                imageId = await _productRepository.CreateImage(model.ImageFile);
            }

            await _productRepository.Create(model);

            return Ok(model);
        }

        return BadRequest();
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = "admin")]
    public async Task<IActionResult> Delete([FromRoute]int id)
    {
        var itemToDelete = await _productRepository.GetById(id);

        if (itemToDelete is null)
        {
            throw new NoContentException();       
        }

        await _productRepository.Delete(itemToDelete);
        return NoContent();
    }

}
