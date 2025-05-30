using ASPWebAPI.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASPWebAPI.Domain.Interfaces
{
    public interface IAdoptionRequestRepository
    {
        IEnumerable<AdoptionRequest> GetAll();
        AdoptionRequest GetById(int id);
        AdoptionRequest Add(AdoptionRequest adoptionRequest);
        AdoptionRequest Update(int id, AdoptionRequest updatedAdoptionRequest);
        AdoptionRequest DeleteById(int id);
    }
}
