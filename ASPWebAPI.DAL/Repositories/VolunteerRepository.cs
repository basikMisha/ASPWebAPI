using ASPWebAPI.Domain.Entities;
using ASPWebAPI.Domain.Interfaces;

namespace ASPWebAPI.DAL.Repositories
{
    public class VolunteerRepository : IVolunteerRepository
    {
        private List<Volunteer> _volunteers = new()
        {
            new Volunteer {Id = 1, Name = "Misha", Email = "test@gmail.com", Role = "nurse", StartDate = DateTime.Now, Pets = {} }
        };

        public IEnumerable<Volunteer> GetAll() => _volunteers;

        public Volunteer GetById(int id) => _volunteers.FirstOrDefault(v => v.Id == id);

        public Volunteer Add(Volunteer volunteer)
        {
            volunteer.Id = _volunteers.Max(v => v.Id) + 1;
            _volunteers.Add(volunteer);
            return volunteer;
        }

        public Volunteer Update(int id, Volunteer updatedVolunteer)
        {
            var volunteer = _volunteers.FirstOrDefault(v => v.Id == id);
            if (volunteer == null) return null;
            volunteer.Name = updatedVolunteer.Name;
            volunteer.Email = updatedVolunteer.Email;
            volunteer.Role = updatedVolunteer.Role;
            volunteer.StartDate = updatedVolunteer.StartDate;

            if(updatedVolunteer.Pets is not null)
            {
                volunteer.Pets = updatedVolunteer.Pets;
            }
            return volunteer;
        }

        public Volunteer DeleteById(int id)
        {
            var volunteer = _volunteers.FirstOrDefault(v => v.Id==id);
            if (volunteer is not null)
            {
                _volunteers.Remove(volunteer);
            }
            return volunteer;
        }
    }
}
