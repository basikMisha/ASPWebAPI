using ASPWebAPI.Domain.Entities;
using ASPWebAPI.DTOs.AdoptionRequest;
using AutoMapper;

namespace ASPWebAPI.Api.Mapping
{
    public class AdoptionRequestProfile : Profile
    {
        public AdoptionRequestProfile()
        {
            CreateMap<CreateAdoptionRequestDto, AdoptionRequest>();
            CreateMap<UpdateAdoptionRequestDto, AdoptionRequest>();
            CreateMap<AdoptionRequest, AdoptionRequestDto>();
        }
    }
}
