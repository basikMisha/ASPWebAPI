using ASPWebAPI.BLL.Interfaces;
using ASPWebAPI.Domain.Entities;
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
        private readonly IAdopterService _adopterService;

        public AdopterController(IAdopterService adopterService)
        {
            _adopterService = adopterService;
        }

        /// <summary>
        /// Get all adopters
        /// </summary>
        /// <returns>adopters</returns>
        [HttpGet]
        public ActionResult<IEnumerable<Adopter>> GetAll()
        {
            var adopters = _adopterService.GetAll();
            return Ok(adopters);
        }

        /// <summary>
        /// Get single adopter by id
        /// </summary>
        /// <param name="id">adopter id</param>
        /// <returns>adopter</returns>
        [HttpGet("{id}")]
        public ActionResult<Adopter> GetById(int id)
        {
            var adopter = _adopterService.GetById(id);
            return adopter is not null ? Ok(adopter) : NotFound();
        }

        /// <summary>
        /// Create a new adopter
        /// </summary>
        /// <param name="adopter">Adopter object with name, email and adoptionDate</param>
        /// <returns>new adopter</returns>
        [HttpPost]
        public ActionResult<Adopter> Create(Adopter adopter)
        {
            var created = _adopterService.Add(adopter);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        /// <summary>
        /// Update adopter by id
        /// </summary>
        /// <param name="id">adopter id</param>
        /// <param name="updatedAdopter">Adopter object with updated values</param>
        /// <returns> updated adopter </returns>
        [HttpPut("{id}")]
        public ActionResult<Adopter> Update(int id, Adopter updatedAdopter)
        {
            var updated = _adopterService.Update(id, updatedAdopter);
            return updated is not null ? Ok(updated) : NotFound();
        }

        /// <summary>
        /// Delete adopter by id
        /// </summary>
        /// <param name="id">adopter id</param>
        /// <returns>deleted adopter</returns>
        [HttpDelete("{id}")]
        public ActionResult<Adopter> Delete(int id)
        {
            var deleted = _adopterService.DeleteById(id);
            return deleted is not null ? Ok(deleted) : NotFound();
        }
    }
}