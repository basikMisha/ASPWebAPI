using ASPWebAPI.Domain.Entities;
using ASPWebAPI.DTOs.Pet;
using AutoMapper;

namespace ASPWebAPI.Api.Mapping
{
    public class PetProfile : Profile
    {
        public PetProfile()
        {
            CreateMap<CreatePetDto, Pet>();
            CreateMap<UpdatePetDto, Pet>();
            CreateMap<Pet, PetDto>();
        }
    }
}
