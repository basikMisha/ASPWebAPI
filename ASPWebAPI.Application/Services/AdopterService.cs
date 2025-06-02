using ASPWebAPI.BLL.Interfaces;
using ASPWebAPI.Domain.Entities;
using ASPWebAPI.Domain.Interfaces;

namespace ASPWebAPI.BLL.Services
{
    public class AdopterService : IAdopterService
    {
        private IAdopterRepository _repo;

        public AdopterService(IAdopterRepository repo)
        {
            _repo = repo;
        }

        public IEnumerable<Adopter> GetAll() => _repo.GetAll();

        public Adopter GetById(int id) => _repo.GetById(id);

        public Adopter Add(Adopter adopter)
        {
            _repo.Add(adopter);
            return adopter;
        }

        public Adopter Update(int id, Adopter updatedAdopter)
        {
            var adopter = _repo.GetById(id);
            if (adopter == null) return null;
            adopter.Id = id;
            return _repo.Update(id, updatedAdopter);
        }

        public Adopter DeleteById(int id) => _repo.DeleteById(id);
    }
}
