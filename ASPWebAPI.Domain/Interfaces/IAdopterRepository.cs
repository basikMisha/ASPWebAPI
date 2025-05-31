using ASPWebAPI.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASPWebAPI.Domain.Interfaces
{
    public interface IAdopterRepository
    {
        IEnumerable<Adopter> GetAll();
        Adopter GetById(int id);
        Adopter Update(int id, Adopter updatedAdopter);
        Adopter DeleteById(int id);
        Adopter Add(Adopter adopter);
    }
}
