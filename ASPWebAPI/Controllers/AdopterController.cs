using ASPWebAPI.BLL.Interfaces;
using ASPWebAPI.Domain.Entities;
using ASPWebAPI.DTOs.Adopters;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace ASPWebAPI.Controllers
{
    /// <summary>
    /// Contoller for managing adopters
    /// Supports CRUD operations
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class AdopterController : Controller
    {
        private readonly IAdopterService _adopterService;
        private readonly IMapper _mapper;

        public AdopterController(IAdopterService adopterService, IMapper mapper)
        {
            _adopterService = adopterService;
            _mapper = mapper;
        }

        /// <summary>
        /// Get all adopters
        /// </summary>
        /// <returns>adopters</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AdopterDto>>> GetAll()
        {
            var adopters = await _adopterService.GetAllAsync();
            return Ok(_mapper.Map<IEnumerable<AdopterDto>>(adopters));
        }

        /// <summary>
        /// Get single adopter by id
        /// </summary>
        /// <param name="id">adopter id</param>
        /// <returns>adopter</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<AdopterDto>> GetById(int id)
        {
            var adopter = await _adopterService.GetByIdAsync(id);
            return adopter is not null
                ? Ok(_mapper.Map<AdopterDto>(adopter)) 
                : NotFound();
        }

        /// <summary>
        /// Create a new adopter
        /// </summary>
        /// <param name="createDto">Adopter object with name, email and phone</param>
        /// <returns>new adopter</returns>
        [HttpPost]
        public async Task<ActionResult<AdopterDto>> Create([FromBody] CreateAdopterDto createDto)
        {
            var adopter = _mapper.Map<Adopter>(createDto);
            var created = await _adopterService.AddAsync(adopter);
            return CreatedAtAction(nameof(GetById), new { id = created.Id },
                _mapper.Map<AdopterDto>(created));
        }

        /// <summary>
        /// Update adopter by id
        /// </summary>
        /// <param name="id">adopter id</param>
        /// <param name="updateDto">Adopter object with updated values</param>
        /// <returns> updated adopter </returns>
        [HttpPut("{id}")]
        public async Task<ActionResult<AdopterDto>> Update(int id, [FromBody]  UpdateAdopterDto updateDto)
        {
            var adopter = _mapper.Map<Adopter>(updateDto);
            var updated = await _adopterService.UpdateAsync(id, adopter);
            return updated is not null 
                ? Ok(_mapper.Map<AdopterDto>(updated))
                : NotFound();
        }

        /// <summary>
        /// Delete adopter by id
        /// </summary>
        /// <param name="id">adopter id</param>
        /// <returns>deleted adopter</returns>
        [HttpDelete("{id}")]
        public async Task<ActionResult<AdopterDto>> Delete(int id)
        {
            var deleted = await _adopterService.DeleteByIdAsync(id);
            return deleted is not null
                ? Ok(_mapper.Map<AdopterDto>(deleted))
                : NotFound();
        }
    }
}