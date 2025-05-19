using ASPWebAPI.Models;
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
        private static List<AdoptionRequest> AdoptionRequests = new List<AdoptionRequest>
        {
           new AdoptionRequest {Id = 1, AdopterId = 1, PetId = 1, RequestDate = new DateTime(2012, 12, 25, 10, 30, 50), Status = "accepted"}
        };

        /// <summary>
        /// Get all adoption requests
        /// </summary>
        /// <returns>adoption requests</returns>
        [HttpGet]
        public ActionResult<IEnumerable<AdoptionRequest>> GetAll()
        {
            return Ok(AdoptionRequests);
        }

        /// <summary>
        /// Get single adoption request by id
        /// </summary>
        /// <param name="id">adoption request id</param>
        /// <returns>adoption request</returns>
        [HttpGet("{id}")]
        public ActionResult<AdoptionRequest> GetById(int id) =>
            AdoptionRequests.FirstOrDefault(ad => ad.Id == id) is { } adoptionRequest ? Ok(adoptionRequest) : NotFound();

        /// <summary>
        /// Create a new adoption request
        /// </summary>
        /// <param name="adoptionRequest">Adoption request object with petId, adopterId, requestDate and status</param>
        /// <returns>new adoption request</returns>
        [HttpPost]
        public ActionResult<AdoptionRequest> Create(AdoptionRequest adoptionRequest)
        {
            adoptionRequest.Id = AdoptionRequests.Max(ad => ad.Id) + 1;
            AdoptionRequests.Add(adoptionRequest);
            return CreatedAtAction(nameof(GetById), new { Id = adoptionRequest.Id }, adoptionRequest);
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
            var adoptionRequest = AdoptionRequests.FirstOrDefault(ad => ad.Id == id);
            if (adoptionRequest is null) return NotFound();
            adoptionRequest.PetId = updatedAdoptionRequest.PetId;
            adoptionRequest.Status = updatedAdoptionRequest.Status;
            adoptionRequest.RequestDate = updatedAdoptionRequest.RequestDate;
            adoptionRequest.AdopterId = updatedAdoptionRequest.AdopterId;
            return Ok(adoptionRequest);
        }

        /// <summary>
        /// Delete adoption request by id
        /// </summary>
        /// <param name="id">adoption request id</param>
        /// <returns> deleted adoption request </returns>
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var adoptionRequest = AdoptionRequests.FirstOrDefault(ad => ad.Id == id);
            if (adoptionRequest is null) return NotFound();
            AdoptionRequests.Remove(adoptionRequest);
            return Ok(adoptionRequest);
        }
    }
}
