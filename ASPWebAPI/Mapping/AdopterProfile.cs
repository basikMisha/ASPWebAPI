using ASPWebAPI.Domain.Entities;
using ASPWebAPI.DTOs.Adopters;
using AutoMapper;

namespace ASPWebAPI.Api.Mapping
{
    public class AdopterProfile : Profile
    {
        public AdopterProfile()
        {
            // Create -> Entity
            CreateMap<CreateAdopterDto, Adopter>();
            // Update -> Entity
            CreateMap<UpdateAdopterDto, Adopter>();
            // Entity -> Response
            CreateMap<Adopter, AdopterDto>();
        }
    }
}
