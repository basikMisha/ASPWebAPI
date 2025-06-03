using Microsoft.Extensions.DependencyInjection;
using ASPWebAPI.BLL.Interfaces;
using ASPWebAPI.BLL.Services;
namespace ASPWebAPI.BLL
{
    public static class ServiceCollectionExtensions
    {
        public static void AddBLLServices(this IServiceCollection services)
        {

            services.AddScoped<IAdopterService, AdopterService>();

            services.AddScoped<IVolunteerService, VolunteerService>();

            services.AddScoped<IPetService, PetService>();

            services.AddScoped<IAdoptionRequestService, AdoptionRequestService>();
        }
    }
}
