using ASPWebAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace ASPWebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AdopterController : Controller
    {
        private static List<Adopter> Adopters = new List<Adopter>
        {
            new Adopter {Id = 1, Name = "Misha", Email = "misha@gmail.com", AdoptionDate = new DateTime(2012, 12, 25, 10, 30, 50) }
        };

        /// <summary>
        /// GET ALL ADOPTERS
        /// </summary>
        /// <returns> ADOPTERS </returns>
        [HttpGet]
        public ActionResult<IEnumerable<Adopter>> GetAll()
        {
            return Ok(Adopters);
        }

        /// <summary>
        /// GET SINGLE ADOPTER BY ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns> ADOPTER </returns>
        [HttpGet("{id}")]
        public ActionResult<Adopter> GetById(int id) =>
            Adopters.FirstOrDefault(a => a.Id == id) is { } adopter ? Ok(adopter) : NotFound();


        /// <summary>
        /// CREATE A NEW ADOPTER
        /// </summary>
        /// <param name="adopter"></param>
        /// <returns>ADOPTER ID</returns>
        [HttpPost]
        public ActionResult<Adopter> Create(Adopter adopter)
        {
            adopter.Id = Adopters.Max(a => a.Id) + 1;
            Adopters.Add(adopter);
            return CreatedAtAction(nameof(GetById), new { Id = adopter.Id }, adopter);
        }

        /// <summary>
        /// UPDATE ADOPTER BY ID
        /// </summary>
        /// <param name="id"></param>
        /// <param name="updatedAdopter"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public IActionResult Update(int id, Adopter updatedAdopter)
        {
            var adopter = Adopters.FirstOrDefault(a => a.Id == id);
            if (adopter is null) return NotFound();
            adopter.Name = updatedAdopter.Name;
            adopter.Email = updatedAdopter.Email;
            adopter.AdoptionDate = updatedAdopter.AdoptionDate;
            return NoContent();
        }


        /// <summary>
        /// DELETE ADOPTER BY ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var adopter = Adopters.FirstOrDefault(a => a.Id == id);
            if (adopter is null) return NotFound();
            Adopters.Remove(adopter);
            return NoContent();
        }
    }
}
