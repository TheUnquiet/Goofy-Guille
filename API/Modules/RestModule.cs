using Domain.Mappers;
using Domain.Models;
using REST.DTOs.Clubs;
using REST.DTOs.Locations;
using REST.DTOs.Teams;

namespace API.Modules
{
    public class RestModule : IModule
    {
        public void Register(IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IMapper<Domain.Models.Team, TeamDto>, REST.Mappers.TeamMapper>();
            services.AddScoped<IMapper<CreateTeamDto, Domain.Models.Team>, REST.Mappers.TeamMapper>();
            services.AddScoped<IMapper<UpdateTeamDto, Domain.Models.Team>, REST.Mappers.TeamMapper>();
            services.AddScoped<IMapper<Team, TeamDto>, REST.Mappers.TeamMapper>();
            services.AddScoped<IMapper<CreateTeamDto, Team>, REST.Mappers.TeamMapper>();
            services.AddScoped<IMapper<UpdateTeamDto, Team>, REST.Mappers.TeamMapper>();
            services.AddScoped<IMapper<Club, ClubDto>, REST.Mappers.ClubMapper>();
            services.AddScoped<IMapper<CreateClubDto, Club>, REST.Mappers.ClubMapper>();
            services.AddScoped<IMapper<UpdateClubDto, Club>, REST.Mappers.ClubMapper>();
            services.AddScoped<IMapper<Location, LocationDto>, REST.Mappers.LocationMapper>();
            services.AddScoped<IMapper<CreateLocationDto, Location>, REST.Mappers.LocationMapper>();
            services.AddScoped<IMapper<UpdateLocationDto, Location>, REST.Mappers.LocationMapper>();
        }
    }
}