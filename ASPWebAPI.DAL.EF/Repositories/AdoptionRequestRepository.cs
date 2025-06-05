using ASPWebAPI.Domain.Entities;
using ASPWebAPI.Domain.Interfaces;

namespace ASPWebAPI.DAL.EF.Repositories
{
    public class AdoptionRequestRepository : EFRepository<AdoptionRequest>, IAdoptionRequestRepository
    {
        public AdoptionRequestRepository(PetCenterDbContext context) : base(context) { }
    }
}
