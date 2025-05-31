using ASPWebAPI.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASPWebAPI.Domain.Interfaces
{
    public interface IVolunteerRepository
    {
        IEnumerable<Volunteer> GetAll();
        Volunteer GetById(int id);
        Volunteer Add(Volunteer volunteer);
        Volunteer Update(int id, Volunteer updatedVolunteer);
        Volunteer DeleteById(int id);
    }
}
