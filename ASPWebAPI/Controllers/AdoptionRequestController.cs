using ASPWebAPI.BLL.Interfaces;
using ASPWebAPI.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace ASPWebAPI.Controllers
{
    /// <summary>
    /// Contoller for managing adoption requests
    /// Supports CRUD operations
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class AdoptionRequestController : Controller
    {
        private readonly IAdoptionRequestService _adoptionRequestService;

        public AdoptionRequestController(IAdoptionRequestService adoptionRequestService)
        {
            _adoptionRequestService = adoptionRequestService;
        }

        /// <summary>
        /// Get all adoption requests
        /// </summary>
        /// <returns>adoption requests</returns>
        [HttpGet]
        public ActionResult<IEnumerable<AdoptionRequest>> GetAll()
        {
            var adoptionRequests = _adoptionRequestService.GetAll();
            return Ok(adoptionRequests);
        }

        /// <summary>
        /// Get single adoption request by id
        /// </summary>
        /// <param name="id">adoption request id</param>
        /// <returns>adoption request</returns>
        [HttpGet("{id}")]
        public ActionResult<AdoptionRequest> GetById(int id)
        {
            var adoptionRequest = _adoptionRequestService.GetById(id);
            return adoptionRequest is not null ? Ok(adoptionRequest) : NotFound();
        }

        /// <summary>
        /// Create a new adoption request
        /// </summary>
        /// <param name="adoptionRequest">Adoption request object with petId, adopterId, requestDate and status</param>
        /// <returns>new adoption request</returns>
        [HttpPost]
        public ActionResult<AdoptionRequest> Create(AdoptionRequest adoptionRequest)
        {
            var created = _adoptionRequestService.Add(adoptionRequest);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        /// <summary>
        /// Update adoption request by id
        /// </summary>
        /// <param name="id">adoption request id</param>
        /// <param name="updatedAdoptionRequest">Adoption request object with updated values</param>
        /// <returns> updated adoption request </returns>
        [HttpPut("{id}")]
        public IActionResult Update(int id, AdoptionRequest updatedAdoptionRequest)
        {
            var updated = _adoptionRequestService.Update(id, updatedAdoptionRequest);
            return updated is not null ? Ok(updated) : NotFound();
        }

        /// <summary>
        /// Delete adoption request by id
        /// </summary>
        /// <param name="id">adoption request id</param>
        /// <returns> deleted adoption request </returns>
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var deleted = _adoptionRequestService.DeleteById(id);
            return deleted is not null ? Ok(deleted) : NotFound();
        }
    }
}
