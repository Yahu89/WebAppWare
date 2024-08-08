using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WebAppWare.Api.Dto;
using WebAppWare.Api.Repositories;
using WebAppWare.Database.Entities;

namespace WebAppWare.Api.Controllers
{
    [Route("api/[controller]")]
    public class SupplierController : Controller
    {
        private readonly ISupplierRepository _supplierRepository;
        private readonly IMapper _mapper;

        public SupplierController(ISupplierRepository supplierRepository,
                                    IMapper mapper)
        {
            _supplierRepository = supplierRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var entities = await _supplierRepository.GetAll();
            var results = _mapper.Map<IEnumerable<SupplierDto>>(entities);
            return Ok(results);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var entity = await _supplierRepository.GetById(id);
            var result = _mapper.Map<SupplierDto>(entity);
            return Ok(result);
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody]SupplierDto dto)
        {
            if (ModelState.IsValid)
            {
                var entity = _mapper.Map<Supplier>(dto);
                await _supplierRepository.Create(entity);
                return Ok();
            }

            return BadRequest(ModelState);
        }

        [HttpPut("update/{id}")]
        public async Task<IActionResult> Update([FromRoute]int id, [FromBody]SupplierDto dto)
        {
            var entity = await _supplierRepository.GetById(id);

            if (ModelState.IsValid)
            {
                entity.Name = dto.Name;
                entity.Email = dto.Email;

                await _supplierRepository.Edit(entity);
                return Ok();
            }

            return BadRequest();
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> Delete([FromRoute]int id)
        {
            await _supplierRepository.Delete(id);
            return Ok();
        }
    }
}
