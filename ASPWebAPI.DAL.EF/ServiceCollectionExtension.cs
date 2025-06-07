using ASPWebAPI.DAL.EF.Repositories;
using ASPWebAPI.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ASPWebAPI.DAL.EF
{
    public static class ServiceCollectionExtension 
    {
        public static void AddEfDalServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<PetCenterDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            services.AddScoped<IAdopterRepository, AdopterRepository>();
            services.AddScoped<IAdoptionRequestRepository, AdoptionRequestRepository>();
            services.AddScoped<IPetRepository, PetRepository>();
            services.AddScoped<IVolunteerRepository, VolunteerRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
        }

    }
}
