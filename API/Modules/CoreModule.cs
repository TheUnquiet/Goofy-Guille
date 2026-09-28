using Core.Context;
using Core.Repositories.Implementations;
using Core.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace API.Modules
{
    public class CoreModule : IModule
    {
        public void Register(IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<BasketBallClubDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
            services.AddScoped<IUserRepo, UserRepo>();
            services.AddScoped<IClubRepo, ClubRepo>();
            services.AddScoped<IEventRepo, EventRepo>();
            services.AddScoped<IEventResponseRepo, EventResponseRepo>();
            services.AddScoped<ILocationRepo, LocationRepo>();
        }
    }
}
