using ASPWebAPI.Domain.Entities;
using ASPWebAPI.Domain.Interfaces;

namespace ASPWebAPI.DAL.Repositories
{
    public class AdopterRepository : IAdopterRepository
    {
        private List<Adopter> _adopters = new()
        {
            new Adopter {Id = 1, Name = "Misha", Email = "test@gmail.com", AdoptionDate = DateTime.Now, }
        };

        public IEnumerable<Adopter> GetAll() => _adopters;

        public Adopter GetById(int id) => _adopters.FirstOrDefault(a => a.Id == id);

        public Adopter Add(Adopter adopter)
        {
            adopter.Id = _adopters.Max(a => a.Id) + 1;
            _adopters.Add(adopter);
            return adopter;
        }

        public Adopter Update(int id, Adopter updatedAdopter)
        {
            var adopter = _adopters.FirstOrDefault(a => a.Id == id);
            if (adopter == null) return null;
            adopter.Name = updatedAdopter.Name;
            adopter.Email = updatedAdopter.Email;
            adopter.AdoptionDate = updatedAdopter.AdoptionDate;
            adopter.Phone = updatedAdopter?.Phone;

            if (updatedAdopter.AdoptionRequests is not null)
            {
                adopter.AdoptionRequests = updatedAdopter.AdoptionRequests;
            }

            return adopter;
        }

        public Adopter DeleteById(int id)
        {
            var adopter = _adopters.FirstOrDefault(a => a.Id == id);
            if(adopter is not null)
            {
                _adopters.Remove(adopter);
            }
            return adopter;
        }

    }
}
