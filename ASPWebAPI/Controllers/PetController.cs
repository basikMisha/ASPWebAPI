using ASPWebAPI.BLL.Interfaces;
using ASPWebAPI.Domain.Entities;
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

        public PetController(IPetService petService)
        {
            _petService = petService;
        }

        /// <summary>
        /// Get all pets
        /// </summary>
        /// <returns>pets</returns>
        [HttpGet]
        public ActionResult<IEnumerable<Pet>> GetAll()
        {
            var pets = _petService.GetAll();
            return Ok(pets);
        }

        /// <summary>
        /// Get single pet by id
        /// </summary>
        /// <param name="id">pet id</param>
        /// <returns>pet</returns>
        [HttpGet("{id}")]
        public ActionResult<Pet> GetById(int id)
        {
            var pet = _petService.GetById(id);
            return pet is not null ? Ok(pet) : NotFound();
        }

        /// <summary>
        /// Create a new pet
        /// </summary>
        /// <param name="pet">Pet object with name, species, age and isAdopted</param>
        /// <returns>new pet</returns>
        [HttpPost]
        public ActionResult<Pet> Create(Pet pet)
        {
            var created = _petService.Add(pet);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
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
            var updated = _petService.Update(id, updatedPet);
            return updated is not null ? Ok(updated) : NotFound();
        }

        /// <summary>
        /// Delete pet by id
        /// </summary>
        /// <param name="id">pet id</param>
        /// <returns>deleted pet</returns>
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var deleted = _petService.DeleteById(id);
            return deleted is not null ? Ok(deleted) : NotFound();
        }
    }
}
