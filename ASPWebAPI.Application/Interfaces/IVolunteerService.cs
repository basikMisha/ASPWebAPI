using ASPWebAPI.Domain.Entities;

namespace ASPWebAPI.BLL.Interfaces
{
    public interface IVolunteerService
    {
        IEnumerable<Volunteer> GetAll();
        Volunteer GetById(int id);
        Volunteer Add(Volunteer volunteer);
        Volunteer Update(int id, Volunteer updatedVolunteer);
        Volunteer DeleteById(int id);
    }
}
