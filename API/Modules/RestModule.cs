using Microsoft.Extensions.DependencyInjection;
using REST.Mappers;

namespace API.Modules
{
    public class RestModule : IModule
    {
        public void Register(IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IMapper<Domain.Models.Team, TeamDto>, TeamMapper>();
            services.AddScoped<IMapper<CreateTeamDto, Domain.Models.Team>, TeamMapper>();
            services.AddScoped<IMapper<UpdateTeamDto, Domain.Models.Team>, TeamMapper>();
        }
    }
}