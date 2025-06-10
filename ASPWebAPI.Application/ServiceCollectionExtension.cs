using Microsoft.Extensions.DependencyInjection;
using ASPWebAPI.BLL.Interfaces;
using ASPWebAPI.BLL.Services;
using Microsoft.Extensions.Configuration;
using ASPWebAPI.DAL.EF;
using ASPWebAPI.DAL;

namespace ASPWebAPI.BLL
{
    public static class ServiceCollectionExtensions
    {
        public static void AddBLLServices(this IServiceCollection services, IConfiguration configuration)
        {
            bool useEfDal = configuration.GetValue<bool>("UseEfDal");

            if (useEfDal)
            {
                services.AddEfDalServices(configuration);
            }
            else
            {
                services.AddDapperDalServices();
            }
            services.AddDapperDalServices();
            services.AddScoped<IAdopterService, AdopterService>();

            services.AddScoped<IVolunteerService, VolunteerService>();

            services.AddScoped<IPetService, PetService>();

            services.AddScoped<IAdoptionRequestService, AdoptionRequestService>();

            services.AddScoped<JwtService>();
            services.AddScoped<IAuthService, AuthService>();
        }
    }
}
