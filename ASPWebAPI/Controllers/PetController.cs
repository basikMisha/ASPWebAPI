using ASPWebAPI.Models;
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
        private static List<Pet> Pets = new List<Pet>
        {
            new Pet { Id = 1, Name = "Charlie", Species = "Dog", Age = 3, isAdopted = false }
        };

        /// <summary>
        /// Get all pets
        /// </summary>
        /// <returns>pets</returns>
        [HttpGet]
        public ActionResult<IEnumerable<Pet>> GetAll()
        {
            return Ok(Pets);
        }

        /// <summary>
        /// Get single pet by id
        /// </summary>
        /// <param name="id">pet id</param>
        /// <returns>pet</returns>
        [HttpGet("{id}")]
        public ActionResult<Pet> GetById(int id) => 
            Pets.FirstOrDefault(p => p.Id == id) is { } pet ? Ok(pet) : NotFound();

        /// <summary>
        /// Create a new pet
        /// </summary>
        /// <param name="pet">Pet object with name, species, age and isAdopted</param>
        /// <returns>new pet</returns>
        [HttpPost]
        public ActionResult<Pet> Create(Pet pet)
        {
            pet.Id = Pets.Max(p => p.Id) + 1;
            Pets.Add(pet);
            return CreatedAtAction(nameof(GetById), new { Id = pet.Id }, pet);
        }

        /// <summary>
        /// Update pet by id
        /// </summary>
        /// <param name="id">pet id</param>
        /// <param name="updatedPet">Pet object with updated values</param>
        /// <returns>updated pet</returns>
        [HttpPut("{id}")]
        public IActionResult Update(int id, Pet updatedPet)
        {
            var pet = Pets.FirstOrDefault(p => p.Id == id);
            if (pet is null) return NotFound();
            pet.Name = updatedPet.Name;
            pet.Species = updatedPet.Species;
            pet.Age = updatedPet.Age;
            pet.isAdopted = updatedPet.isAdopted;
            return Ok(pet);
        }

        /// <summary>
        /// Delete pet by id
        /// </summary>
        /// <param name="id">pet id</param>
        /// <returns>deleted pet</returns>
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var pet = Pets.FirstOrDefault(p=>p.Id == id);
            if (pet is null) return NotFound();
            Pets.Remove(pet);
            return Ok(pet); 
        }
    }
}
