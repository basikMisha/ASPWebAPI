using ASPWebAPI.DAL.Repositories;
using ASPWebAPI.Domain.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace ASPWebAPI.DAL
{
    public static class ServiceCollectionExtensions
    {
        public static void AddDapperDalServices(this IServiceCollection services) 
        { 
            services.AddScoped<IAdopterRepository, AdopterRepository>();
            services.AddScoped<IVolunteerRepository, VolunteerRepository>();
            services.AddScoped<IPetRepository, PetRepository>();
            services.AddScoped<IAdoptionRequestRepository, AdoptionRequestRepository>();
        }
    }
}
