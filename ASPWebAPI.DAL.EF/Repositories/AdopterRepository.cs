using ASPWebAPI.Domain.Entities;
using ASPWebAPI.Domain.Interfaces;

namespace ASPWebAPI.DAL.EF.Repositories
{
    public class AdopterRepository : EFRepository<Adopter>, IAdopterRepository
    {
        public AdopterRepository(PetCenterDbContext context) : base(context) { }
    }
}
