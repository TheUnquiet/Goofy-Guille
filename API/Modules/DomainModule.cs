using Domain.Mappers;
using Domain.Services.Implementations;
using Domain.Services.Interfaces;

namespace API.Modules
{
    public class DomainModule : IModule
    {
        public void Register(IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IMapper<Core.Entities.User, Domain.Models.User>, UserMapper>();
            services.AddScoped<IMapper<Domain.Models.User, Core.Entities.User>, UserMapper>();
        }
    }
}
