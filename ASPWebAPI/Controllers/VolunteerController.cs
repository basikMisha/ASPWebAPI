using ASPWebAPI.Models;
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
        private static List<Volunteer> Volunteers = new List<Volunteer>
        {
            new Volunteer {Id = 1, Name = "Grisha", Role = "nurse", StartDate = new DateTime(2012, 12, 25, 10, 30, 50) }
        };

        /// <summary>
        /// Get all volunteers
        /// </summary>
        /// <returns>volunteers</returns>
        [HttpGet]
        public ActionResult<IEnumerable<Volunteer>> GetAll()
        {
            return Ok(Volunteers);
        }

        /// <summary>
        /// Get single volunteer by id
        /// </summary>
        /// <param name="id">volunteer id</param>
        /// <returns>volunteer</returns>
        [HttpGet("{id}")]
        public ActionResult<Volunteer> GetById(int id) =>
            Volunteers.FirstOrDefault(v => v.Id == id) is { } volunteer ? Ok(volunteer) : NotFound();

        /// <summary>
        /// Create a new volunteer
        /// </summary>
        /// <param name="volunteer">Volunteer object with name, role and startDate</param>
        /// <returns>new volunteer</returns>
        [HttpPost]
        public ActionResult<Volunteer> Create(Volunteer volunteer)
        {
            volunteer.Id = Volunteers.Max(v => v.Id) + 1;
            Volunteers.Add(volunteer);
            return CreatedAtAction(nameof(GetById), new { Id = volunteer.Id }, volunteer);
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
            var volunteer = Volunteers.FirstOrDefault(v => v.Id == id);
            if (volunteer is null) return NotFound();
            volunteer.Name = updatedVolunteer.Name;
            volunteer.StartDate = updatedVolunteer.StartDate;
            volunteer.Role = updatedVolunteer.Role;
            return Ok(volunteer);
        }

        /// <summary>
        /// Delete volunteer by id
        /// </summary>
        /// <param name="id">volunteer id</param>
        /// <returns>deleted volunteer</returns>
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var volunteer = Volunteers.FirstOrDefault(v => v.Id == id);
            if (volunteer is null) return NotFound();
            Volunteers.Remove(volunteer);
            return Ok(volunteer);
        }

    }
}
