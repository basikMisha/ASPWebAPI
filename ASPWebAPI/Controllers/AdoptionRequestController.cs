using ASPWebAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace ASPWebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AdoptionRequestController : Controller
    {
        private static List<AdoptionRequest> AdoptionRequests = new List<AdoptionRequest>
        {
           new AdoptionRequest {Id = 1, AdopterId = 1, PetId = 1, RequestDate = new DateTime(2012, 12, 25, 10, 30, 50), Status = "accepted"}
        };


        /// <summary>
        /// GET ALL ADOPTION REQUESTS
        /// </summary>
        /// <returns> ADOPTIONS REQUESTS </returns>
        [HttpGet]
        public ActionResult<IEnumerable<AdoptionRequest>> GetAll()
        {
            return Ok(AdoptionRequests);
        }


        /// <summary>
        /// GET SINGLE ADOPTION REQUEST BY ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns> ADOPTION REQUEST </returns>
        [HttpGet("{id}")]
        public ActionResult<AdoptionRequest> GetById(int id) =>
            AdoptionRequests.FirstOrDefault(ad => ad.Id == id) is { } adoptionRequest ? Ok(adoptionRequest) : NotFound();


        /// <summary>
        /// CREATE A NEW ADOPTION REQUEST
        /// </summary>
        /// <param name="adoptionRequest"></param>
        /// <returns>ADOPTION REQUEST ID</returns>
        [HttpPost]
        public ActionResult<AdoptionRequest> Create(AdoptionRequest adoptionRequest)
        {
            adoptionRequest.Id = AdoptionRequests.Max(ad => ad.Id) + 1;
            AdoptionRequests.Add(adoptionRequest);
            return CreatedAtAction(nameof(GetById), new { Id = adoptionRequest.Id }, adoptionRequest);
        }


        /// <summary>
        /// UPDATE ADOPTION REQUEST BY ID
        /// </summary>
        /// <param name="id"></param>
        /// <param name="updatedAdoptionRequest"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public IActionResult Update(int id, AdoptionRequest updatedAdoptionRequest)
        {
            var adoptionRequest = AdoptionRequests.FirstOrDefault(ad => ad.Id == id);
            if (adoptionRequest is null) return NotFound();
            adoptionRequest.PetId = updatedAdoptionRequest.PetId;
            adoptionRequest.Status = updatedAdoptionRequest.Status;
            adoptionRequest.RequestDate = updatedAdoptionRequest.RequestDate;
            adoptionRequest.AdopterId = updatedAdoptionRequest.AdopterId;
            return NoContent();
        }

        /// <summary>
        /// DELETE ADOPTION REQUEST BY ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var adoptionRequest = AdoptionRequests.FirstOrDefault(ad => ad.Id == id);
            if (adoptionRequest is null) return NotFound();
            AdoptionRequests.Remove(adoptionRequest);
            return NoContent();
        }
    }
}
