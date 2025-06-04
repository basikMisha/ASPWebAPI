using ASPWebAPI.Domain.Entities;
using ASPWebAPI.Domain.Interfaces;

namespace ASPWebAPI.DAL.EF.Repositories
{
    public class PetRepository : EFRepository<Pet>, IPetRepository
    {
        public PetRepository(PetCenterDbContext context) : base(context) { }
    }
}
