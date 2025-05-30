using ASPWebAPI.BLL.Interfaces;
using ASPWebAPI.Domain.Entities;
using ASPWebAPI.Domain.Interfaces;

namespace ASPWebAPI.BLL.Services
{
    public class AdoptionRequestService : IAdoptionRequestService
    {
        private IAdoptionRequestRepository _repo;

        public AdoptionRequestService(IAdoptionRequestRepository repo)
        {
            _repo = repo;
        }   

        public IEnumerable<AdoptionRequest> GetAll() => _repo.GetAll();

        public AdoptionRequest GetById(int id) => _repo.GetById(id);

        public AdoptionRequest Add(AdoptionRequest adoptionRequest)
        {
            _repo.Add(adoptionRequest);
            return adoptionRequest;
        }

        public AdoptionRequest Update(int id, AdoptionRequest updatedAdoptionRequest)
        {
            var adoptionRequest = _repo.GetById(id);
            if (adoptionRequest == null) return null;
            adoptionRequest.Id = id;
            return _repo.Update(id, updatedAdoptionRequest);
        }

        public AdoptionRequest DeleteById(int id) => _repo.DeleteById(id);
    }
}
