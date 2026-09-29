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
            services.AddScoped<IClubService, ClubService>();
            services.AddScoped<IMapper<Core.Entities.Club, Domain.Models.Club>, ClubMapper>();
            services.AddScoped<IMapper<Domain.Models.Club, Core.Entities.Club>, ClubMapper>();
            services.AddScoped<ILocationService, LocationService>();
            services.AddScoped<IMapper<Core.Entities.Location, Domain.Models.Location>, LocationMapper>();
            services.AddScoped<IMapper<Domain.Models.Location, Core.Entities.Location>, LocationMapper>();
            services.AddScoped<IEventService, EventService>();
            services.AddScoped<IMapper<Core.Entities.Event, Models.Event>, EventMapper>();
            services.AddScoped<IMapper<Models.Event, Core.Entities.Event>, EventMapper>();
            services.AddScoped<IEventResponseService, EventResponseService>();
            services.AddScoped<IMapper<Core.Entities.EventResponse, Models.EventResponse>, EventResponseMapper>();
            services.AddScoped<IMapper<Models.EventResponse, Core.Entities.EventResponse>, EventResponseMapper>();
            services.AddScoped<ITeamService, TeamService>();
            services.AddScoped<IMapper<Core.Entities.Team, Models.Team>, TeamMapper>();
            services.AddScoped<IMapper<Models.Team, Core.Entities.Team>, TeamMapper>();
            services.AddScoped(typeof(IGenericService<,>), typeof(GenericService<,>));
        }
    }
}
