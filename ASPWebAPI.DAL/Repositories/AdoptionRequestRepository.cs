using ASPWebAPI.Domain.Entities;
using ASPWebAPI.Domain.Interfaces;


namespace ASPWebAPI.DAL.Repositories
{
    public class AdoptionRequestRepository : IAdoptionRequestRepository
    {
        private List<AdoptionRequest> _requests = new()
        {
            new AdoptionRequest {Id = 1, AdopterId = 1, PetId = 1, RequestDate = DateTime.Now, Status = "approved"}
        };

        public IEnumerable<AdoptionRequest> GetAll() => _requests;

        public AdoptionRequest GetById(int id) => _requests.FirstOrDefault(ar => ar.Id == id);

        public AdoptionRequest Add(AdoptionRequest adoptionRequest)
        {
            adoptionRequest.Id = _requests.Max(ar => ar.Id) + 1;
            _requests.Add(adoptionRequest);
            return adoptionRequest;
        }

        public AdoptionRequest Update(int id, AdoptionRequest updatedAdoptionRequest) 
        {
            var adoptionRequest = _requests.FirstOrDefault(ar => ar.Id == id);
            if (adoptionRequest == null) return null;

            adoptionRequest.AdopterId = updatedAdoptionRequest.AdopterId;
            adoptionRequest.PetId = updatedAdoptionRequest.PetId;
            adoptionRequest.RequestDate = updatedAdoptionRequest.RequestDate;
            adoptionRequest.Status = updatedAdoptionRequest.Status;

            if(updatedAdoptionRequest.Adopter is not null)
            {
                adoptionRequest.Adopter = updatedAdoptionRequest.Adopter;
            }
            if(updatedAdoptionRequest.Pet is not null)
            {
                adoptionRequest.Pet = updatedAdoptionRequest.Pet;
            }

            return adoptionRequest;
        }

        public AdoptionRequest DeleteById(int id)
        {
            var adoptionRequest = _requests.FirstOrDefault(ar => ar.Id == id);
            if(adoptionRequest is not null)
            {
                _requests.Remove(adoptionRequest);
            }
            return adoptionRequest;
        }
    }
}
