using Microsoft.Extensions.DependencyInjection;
using ASPWebAPI.BLL.Interfaces;
using ASPWebAPI.BLL.Services;
using ASPWebAPI.DAL.Repositories;
using ASPWebAPI.Domain.Interfaces;
namespace ASPWebAPI.BLL
{
    public static class ServiceCollectionExtensions
    {
        public static void AddBLLServices(this IServiceCollection services)
        {

            services.AddScoped<IAdopterService, AdopterService>();
            services.AddScoped<IAdopterRepository, AdopterRepository>();

            services.AddScoped<IVolunteerService, VolunteerService>();
            services.AddScoped<IVolunteerRepository, VolunteerRepository>();

            services.AddScoped<IPetService, PetService>();
            services.AddScoped<IPetRepository, PetRepository>();

            services.AddScoped<IAdoptionRequestService, AdoptionRequestService>();
            services.AddScoped<IAdoptionRequestRepository, AdoptionRequestRepository>();
        }
    }
}
