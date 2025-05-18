using ASPWebAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace ASPWebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PetController : Controller
    {
        private static List<Pet> Pets = new List<Pet>
        {
            new Pet { Id = 1, Name = "Charlie", Species = "Dog", Age = 3, isAdopted = false }
        };

        /// <summary>
        /// GET ALL PETS
        /// </summary>
        /// <returns> PET </returns>
        [HttpGet]
        public ActionResult<IEnumerable<Pet>> GetAll()
        {
            return Ok(Pets);
        }


        /// <summary>
        /// GET SINGLE PET BY ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns> PET </returns>
        [HttpGet("{id}")]
        public ActionResult<Pet> GetById(int id) => 
            Pets.FirstOrDefault(p => p.Id == id) is { } pet ? Ok(pet) : NotFound();


        /// <summary>
        /// CREATE A NEW PET
        /// </summary>
        /// <param name="pet"></param>
        /// <returns>PET ID</returns>
        [HttpPost]
        public ActionResult<Pet> Create(Pet pet)
        {
            pet.Id = Pets.Max(p => p.Id) + 1;
            Pets.Add(pet);
            return CreatedAtAction(nameof(GetById), new { Id = pet.Id }, pet);
        }

        /// <summary>
        /// UPDATE PET BY ID
        /// </summary>
        /// <param name="id"></param>
        /// <param name="updatedPet"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public IActionResult Update(int id, Pet updatedPet)
        {
            var pet = Pets.FirstOrDefault(p => p.Id == id);
            if (pet is null) return NotFound();
            pet.Name = updatedPet.Name;
            pet.Species = updatedPet.Species;
            pet.Age = updatedPet.Age;
            pet.isAdopted = updatedPet.isAdopted;
            return NoContent();
        }

        /// <summary>
        /// DELETE PET BY ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var pet = Pets.FirstOrDefault(p=>p.Id == id);
            if (pet is null) return NotFound();
            Pets.Remove(pet);
            return NoContent(); 
        }
    }
}
