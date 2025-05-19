using ASPWebAPI.Models;
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
        private static List<Adopter> Adopters = new List<Adopter>
        {
            new Adopter {Id = 1, Name = "Misha", Email = "misha@gmail.com", AdoptionDate = new DateTime(2012, 12, 25, 10, 30, 50) }
        };

        /// <summary>
        /// Get all adopters
        /// </summary>
        /// <returns>adopters</returns>
        [HttpGet]
        public ActionResult<IEnumerable<Adopter>> GetAll()
        {
            return Ok(Adopters);
        }

        /// <summary>
        /// Get single adopter by id
        /// </summary>
        /// <param name="id">adopter id</param>
        /// <returns>adopter</returns>
        [HttpGet("{id}")]
        public ActionResult<Adopter> GetById(int id) =>
            Adopters.FirstOrDefault(a => a.Id == id) is { } adopter ? Ok(adopter) : NotFound();

        /// <summary>
        /// Create a new adopter
        /// </summary>
        /// <param name="adopter">Adopter object with name, email and adoptionDate</param>
        /// <returns>new adopter</returns>
        [HttpPost]
        public ActionResult<Adopter> Create(Adopter adopter)
        {
            adopter.Id = Adopters.Max(a => a.Id) + 1;
            Adopters.Add(adopter);
            return CreatedAtAction(nameof(GetById), new { Id = adopter.Id }, adopter);
        }

        /// <summary>
        /// Update adopter by id
        /// </summary>
        /// <param name="id">adopter id</param>
        /// <param name="updatedAdopter">Adopter object with updated values</param>
        /// <returns> updated adopter </returns>
        [HttpPut("{id}")]
        public IActionResult Update(int id, Adopter updatedAdopter)
        {
            var adopter = Adopters.FirstOrDefault(a => a.Id == id);
            if (adopter is null) return NotFound();
            adopter.Name = updatedAdopter.Name;
            adopter.Email = updatedAdopter.Email;
            adopter.AdoptionDate = updatedAdopter.AdoptionDate;
            return Ok(adopter);
        }

        /// <summary>
        /// Delete adopter by id
        /// </summary>
        /// <param name="id">adopter id</param>
        /// <returns>deleted adopter</returns>
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var adopter = Adopters.FirstOrDefault(a => a.Id == id);
            if (adopter is null) return NotFound();
            Adopters.Remove(adopter);
            return Ok(adopter);
        }
    }
}
