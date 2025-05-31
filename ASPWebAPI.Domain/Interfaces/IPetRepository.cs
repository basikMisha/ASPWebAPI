using ASPWebAPI.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASPWebAPI.Domain.Interfaces
{
    public interface IPetRepository
    {
        IEnumerable<Pet> GetAll();
        Pet GetById(int id);
        Pet Add(Pet pet);
        Pet Update(int id, Pet updatedPet);
        Pet DeleteById(int id);
    }
}
