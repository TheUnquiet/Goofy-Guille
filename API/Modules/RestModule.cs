using Microsoft.Extensions.DependencyInjection;
using REST.Mappers;
using Domain.Models;
using REST.DTOs.Teams;
using REST.DTOs.Clubs;

namespace API.Modules
{
    public class RestModule : IModule
    {
        public void Register(IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IMapper<Domain.Models.Team, TeamDto>, TeamMapper>();
            services.AddScoped<IMapper<CreateTeamDto, Domain.Models.Team>, TeamMapper>();
            services.AddScoped<IMapper<UpdateTeamDto, Domain.Models.Team>, TeamMapper>();
            services.AddScoped<IMapper<Team, TeamDto>, TeamMapper>();
            services.AddScoped<IMapper<CreateTeamDto, Team>, TeamMapper>();
            services.AddScoped<IMapper<UpdateTeamDto, Team>, TeamMapper>();
            services.AddScoped<IMapper<Club, ClubDto>, ClubMapper>();
            services.AddScoped<IMapper<CreateClubDto, Club>, ClubMapper>();
            services.AddScoped<IMapper<UpdateClubDto, Club>, ClubMapper>();
        }
    }
}