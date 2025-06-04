using ASPWebAPI.BLL.Interfaces;
using ASPWebAPI.Domain.Entities;
using ASPWebAPI.DTOs.Volunteer;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace ASPWebAPI.Controllers
{
    /// <summary>
    /// Contoller for managing volunteers
    /// Supports CRUD operations
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class VolunteerController : Controller
    {
        private readonly IVolunteerService _volunteerService;
        private readonly IMapper _mapper;

        public VolunteerController(IVolunteerService volunteerService, IMapper mapper) 
        { 
            _volunteerService = volunteerService;
            _mapper = mapper;
        }

        /// <summary>
        /// Get all volunteers
        /// </summary>
        /// <returns>volunteers</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<VolunteerDto>>> GetAll()
        {
            var volunteers = await _volunteerService.GetAllAsync();
            return Ok(_mapper.Map<IEnumerable<VolunteerDto>>(volunteers));
        }

        /// <summary>
        /// Get single volunteer by id
        /// </summary>
        /// <param name="id">volunteer id</param>
        /// <returns>volunteer</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<VolunteerDto>> GetById(int id)
        {
            var volunteer = await _volunteerService.GetByIdAsync(id);
            return volunteer is not null 
                ? Ok(_mapper.Map<VolunteerDto>(volunteer)) 
                : NotFound();
        }

        /// <summary>
        /// Create a new volunteer
        /// </summary>
        /// <param name="createDto">Volunteer object with name, role, email and startDate</param>
        /// <returns>new volunteer</returns>
        [HttpPost]
        public async Task<ActionResult<VolunteerDto>> Create([FromBody] CreateVolunteerDto createDto)
        {
            var volunteer = _mapper.Map<Volunteer>(createDto);
            var created = await _volunteerService.AddAsync(volunteer);
            return CreatedAtAction(nameof(GetById), new { id = created.Id },
                _mapper.Map<VolunteerDto>(created));
        }

        /// <summary>
        /// Update volunteer by id
        /// </summary>
        /// <param name="id">volunteer id</param>
        /// <param name="updateDto">Volunteer object with updated values</param>
        /// <returns>updated vounteer</returns>
        [HttpPut("{id}")]
        public async Task<ActionResult<VolunteerDto>> Update(int id, [FromBody] UpdateVolunteerDto updateDto)
        {
            var volunteer = _mapper.Map<Volunteer>(updateDto);
            var updated = await _volunteerService.UpdateAsync(id, volunteer);
            return updated is not null
                ? Ok(_mapper.Map<VolunteerDto>(updated))
                : NotFound();
        }

        /// <summary>
        /// Delete volunteer by id
        /// </summary>
        /// <param name="id">volunteer id</param>
        /// <returns>deleted volunteer</returns>
        [HttpDelete("{id}")]
        public async Task<ActionResult<VolunteerDto>> Delete(int id)
        {
            var deleted = await _volunteerService.DeleteByIdAsync(id);
            return deleted is not null
                ? Ok(_mapper.Map<VolunteerDto>(deleted))
                : NotFound();
        }

    }
}
