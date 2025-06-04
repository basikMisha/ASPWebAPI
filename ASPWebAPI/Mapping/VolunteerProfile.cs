using ASPWebAPI.Domain.Entities;
using ASPWebAPI.DTOs.Volunteer;
using AutoMapper;

namespace ASPWebAPI.Api.Mapping
{
    public class VolunteerProfile : Profile
    {
        public VolunteerProfile()
        {
            CreateMap<CreateVolunteerDto, Volunteer>();
            CreateMap<UpdateVolunteerDto, Volunteer>();
            CreateMap<Volunteer, VolunteerDto>();
        }
    }
}
