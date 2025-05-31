using ASPWebAPI.BLL.Interfaces;
using ASPWebAPI.Domain.Entities;
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

        public VolunteerController(IVolunteerService volunteerService) 
        { 
            _volunteerService = volunteerService;
        }

        /// <summary>
        /// Get all volunteers
        /// </summary>
        /// <returns>volunteers</returns>
        [HttpGet]
        public ActionResult<IEnumerable<Volunteer>> GetAll()
        {
            var volunteers = _volunteerService.GetAll();
            return Ok(volunteers);
        }

        /// <summary>
        /// Get single volunteer by id
        /// </summary>
        /// <param name="id">volunteer id</param>
        /// <returns>volunteer</returns>
        [HttpGet("{id}")]
        public ActionResult<Volunteer> GetById(int id)
        {
            var volunteer = _volunteerService.GetById(id);
            return volunteer is not null ? Ok(volunteer) : NotFound();
        }

        /// <summary>
        /// Create a new volunteer
        /// </summary>
        /// <param name="volunteer">Volunteer object with name, role and startDate</param>
        /// <returns>new volunteer</returns>
        [HttpPost]
        public ActionResult<Volunteer> Create(Volunteer volunteer)
        {
            var created = _volunteerService.Add(volunteer);
            return CreatedAtAction(nameof(GetById), new {id = created.Id }, created);
        }

        /// <summary>
        /// Update volunteer by id
        /// </summary>
        /// <param name="id">volunteer id</param>
        /// <param name="updatedVolunteer">Volunteer object with updated values</param>
        /// <returns>updated vounteer</returns>
        [HttpPut("{id}")]
        public IActionResult Update(int id, Volunteer updatedVolunteer)
        {
            var updated = _volunteerService.Update(id, updatedVolunteer);
            return updated is not null ? Ok(updated) : NotFound();
        }

        /// <summary>
        /// Delete volunteer by id
        /// </summary>
        /// <param name="id">volunteer id</param>
        /// <returns>deleted volunteer</returns>
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var deleted = _volunteerService.DeleteById(id);
            return deleted is not null ? Ok(deleted) : NotFound();
        }

    }
}
