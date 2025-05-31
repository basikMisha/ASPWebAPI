using ASPWebAPI.BLL.Interfaces;
using ASPWebAPI.Domain.Entities;
using ASPWebAPI.Domain.Interfaces;

namespace ASPWebAPI.BLL.Services
{
    public class VolunteerService : IVolunteerService
    {
        private readonly IVolunteerRepository _repo;

        public VolunteerService(IVolunteerRepository repo) 
        { 
            _repo = repo;
        }

        public IEnumerable<Volunteer> GetAll() => _repo.GetAll();

        public Volunteer GetById(int id) => _repo.GetById(id);

        public Volunteer Add(Volunteer volunteer)
        {
            _repo.Add(volunteer);
            return volunteer;
        }

        public Volunteer Update(int id, Volunteer updatedVolunteer)
        {
            var volunteer = _repo.GetById(id);
            if (volunteer == null) return null;
            volunteer.Id = id;
            return _repo.Update(id, updatedVolunteer);
        }

        public Volunteer DeleteById(int id) => _repo.DeleteById(id);
    }
}
