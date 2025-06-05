using ASPWebAPI.Domain.Entities;
using ASPWebAPI.Domain.Interfaces;

namespace ASPWebAPI.DAL.EF.Repositories
{
    public class VolunteerRepository : EFRepository<Volunteer>, IVolunteerRepository
    {
        public VolunteerRepository(PetCenterDbContext context) : base(context) { }
    }
}
