using ASPWebAPI.BLL.Interfaces;
using ASPWebAPI.Domain.Entities;
using ASPWebAPI.DTOs.Pet;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace ASPWebAPI.Controllers
{
    /// <summary>
    /// Contoller for managing pets
    /// Supports CRUD operations
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class PetController : Controller
    {
        private readonly IPetService _petService;
        private readonly IMapper _mapper;

        public PetController(IPetService petService, IMapper mapper)
        {
            _petService = petService;
            _mapper = mapper;
        }

        /// <summary>
        /// Get all pets
        /// </summary>
        /// <returns>pets</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PetDto>>> GetAll()
        {
            var pets = await _petService.GetAllAsync();
            return Ok(_mapper.Map<IEnumerable<PetDto>>(pets));
        }

        /// <summary>
        /// Get single pet by id
        /// </summary>
        /// <param name="id">pet id</param>
        /// <returns>pet</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<PetDto>> GetById(int id)
        {
            var pet = await _petService.GetByIdAsync(id);
            return pet is not null
                ? Ok(_mapper.Map<PetDto>(pet))
                : NotFound();
        }

        /// <summary>
        /// Create a new pet
        /// </summary>
        /// <param name="createDto">Pet object with name, species, age and isAdopted</param>
        /// <returns>new pet</returns>
        [HttpPost]
        public async Task<ActionResult<PetDto>> Create([FromBody] CreatePetDto createDto)
        {
            var pet = _mapper.Map<Pet>(createDto);
            var created = await _petService.AddAsync(pet);
            return CreatedAtAction(nameof(GetById), new {id = created.Id},
                _mapper.Map<PetDto>(created));
        }

        /// <summary>
        /// Update pet by id
        /// </summary>
        /// <param name="id">pet id</param>
        /// <param name="updateDto">Pet object with updated values</param>
        /// <returns>updated pet</returns>
        [HttpPut("{id}")]
        public async Task<ActionResult<PetDto>> Update(int id, [FromBody] UpdatePetDto updateDto)
        {
            var pet = _mapper.Map<Pet>(updateDto);
            var updated = await _petService.UpdateAsync(id, pet);
            return updated is not null
                ? Ok(_mapper.Map<PetDto>(updated))
                : NotFound();
        }

        /// <summary>
        /// Delete pet by id
        /// </summary>
        /// <param name="id">pet id</param>
        /// <returns>deleted pet</returns>
        [HttpDelete("{id}")]
        public async Task<ActionResult<PetDto>> Delete(int id)
        {
            var deleted = await _petService.DeleteByIdAsync(id);
            return deleted is not null
                ? Ok(_mapper.Map<PetDto>(deleted))
                : NotFound();
        }
    }
}
