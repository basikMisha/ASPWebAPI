using ASPWebAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace ASPWebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class VolunteerController : Controller
    {
        private static List<Volunteer> Volunteers = new List<Volunteer>
        {
            new Volunteer {Id = 1, Name = "Grisha", Role = "nurse", StartDate = new DateTime(2012, 12, 25, 10, 30, 50) }
        };


        /// <summary>
        /// GET ALL VOLUNTEERS
        /// </summary>
        /// <returns> VOLUNTEERS </returns>
        [HttpGet]
        public ActionResult<IEnumerable<Volunteer>> GetAll()
        {
            return Ok(Volunteers);
        }


        /// <summary>
        /// GET SINGLE VOLUNTEER BY ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns> VOLUNTEER </returns>
        [HttpGet("{id}")]
        public ActionResult<Volunteer> GetById(int id) =>
            Volunteers.FirstOrDefault(v => v.Id == id) is { } volunteer ? Ok(volunteer) : NotFound();


        /// <summary>
        /// CREATE A NEW VOLUNTEER
        /// </summary>
        /// <param name="volunteer"></param>
        /// <returns>VOLUNTEER ID</returns>
        [HttpPost]
        public ActionResult<Volunteer> Create(Volunteer volunteer)
        {
            volunteer.Id = Volunteers.Max(v => v.Id) + 1;
            Volunteers.Add(volunteer);
            return CreatedAtAction(nameof(GetById), new { Id = volunteer.Id }, volunteer);
        }


        /// <summary>
        /// UPDATE VOLUNTEER BY ID
        /// </summary>
        /// <param name="id"></param>
        /// <param name="updatedVolunteer"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public IActionResult Update(int id, Volunteer updatedVolunteer)
        {
            var volunteer = Volunteers.FirstOrDefault(v => v.Id == id);
            if (volunteer is null) return NotFound();
            volunteer.Name = updatedVolunteer.Name;
            volunteer.StartDate = updatedVolunteer.StartDate;
            volunteer.Role = updatedVolunteer.Role;
            return NoContent();
        }


        /// <summary>
        /// DELETE VOLUNTEER BY ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var volunteer = Volunteers.FirstOrDefault(v => v.Id == id);
            if (volunteer is null) return NotFound();
            Volunteers.Remove(volunteer);
            return NoContent();
        }

    }
}
