using ASPWebAPI.BLL.Interfaces;
using ASPWebAPI.Domain.Entities;
using ASPWebAPI.DTOs.AdoptionRequest;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace ASPWebAPI.Controllers
{
    /// <summary>
    /// Contoller for managing adoption requests
    /// Supports CRUD operations
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class AdoptionRequestController : Controller
    {
        private readonly IAdoptionRequestService _adoptionRequestService;
        private readonly IMapper _mapper;

        public AdoptionRequestController(IAdoptionRequestService adoptionRequestService, IMapper mapper)
        {
            _adoptionRequestService = adoptionRequestService;
            _mapper = mapper;
        }

        /// <summary>
        /// Get all adoption requests
        /// </summary>
        /// <returns>adoption requests</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AdoptionRequestDto>>> GetAll()
        {
            var adoptionRequests = await _adoptionRequestService.GetAllAsync();
            return Ok(_mapper.Map<IEnumerable<AdoptionRequestDto>>(adoptionRequests));
        }

        /// <summary>
        /// Get single adoption request by id
        /// </summary>
        /// <param name="id">adoption request id</param>
        /// <returns>adoption request</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<AdoptionRequestDto>> GetById(int id)
        {
            var adoptionRequest = await _adoptionRequestService.GetByIdAsync(id);
            return adoptionRequest is not null
                ? Ok(_mapper.Map<AdoptionRequestDto>(adoptionRequest))
                : NotFound();
        }

        /// <summary>
        /// Create a new adoption request
        /// </summary>
        /// <param name="createDto">Adoption request object with petId, adopterId, requestDate and status</param>
        /// <returns>new adoption request</returns>
        [HttpPost]
        public async Task<ActionResult<AdoptionRequestDto>> Create([FromBody] CreateAdoptionRequestDto createDto)
        {
            try
            {
                var adoptionRequest = _mapper.Map<AdoptionRequest>(createDto);
                var created = await _adoptionRequestService.AddAsync(adoptionRequest);
                return CreatedAtAction(nameof(GetById), new { id = created.Id }, _mapper.Map<AdoptionRequestDto>(created));
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        /// <summary>
        /// Update adoption request by id
        /// </summary>
        /// <param name="id">adoption request id</param>
        /// <param name="updateDto">Adoption request object with updated values</param>
        /// <returns> updated adoption request </returns>
        [HttpPut("{id}")]
        public async Task<ActionResult<AdoptionRequestDto>> Update(int id, [FromBody] UpdateAdoptionRequestDto updateDto)
        {
            try
            {
                var adoptionRequest = _mapper.Map<AdoptionRequest>(updateDto);
                var updated = await _adoptionRequestService.UpdateAsync(id, adoptionRequest);
                return updated is not null
                    ? Ok(_mapper.Map<AdoptionRequestDto>(updated))
                    : NotFound();
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        /// <summary>
        /// Delete adoption request by id
        /// </summary>
        /// <param name="id">adoption request id</param>
        /// <returns> deleted adoption request </returns>
        [HttpDelete("{id}")]
        public async Task<ActionResult<AdoptionRequestDto>> Delete(int id)
        {
            var deleted = await _adoptionRequestService.DeleteByIdAsync(id);
            return deleted is not null
                ? Ok(_mapper.Map<AdoptionRequestDto>(deleted))
                : NotFound();
        }
    }
}
