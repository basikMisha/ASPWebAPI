using ASPWebAPI.Domain.Entities;

namespace ASPWebAPI.BLL.Interfaces
{
    public interface IAdoptionRequestService
    {
        IEnumerable<AdoptionRequest> GetAll();
        AdoptionRequest GetById(int id);
        AdoptionRequest Add(AdoptionRequest adoptionRequest);
        AdoptionRequest Update(int id, AdoptionRequest updatedAdoptionRequest);
        AdoptionRequest DeleteById(int id);
    }
}
