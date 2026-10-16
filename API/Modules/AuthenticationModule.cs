using API.Authentication;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace API.Modules
{
    public class AuthenticationModule : IModule
    {
        public void Register(IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<GoogleAuthenticationOptions>(configuration.GetSection("Authentication:Google"));
            services.AddScoped<GoogleTokenValidator>();
            services.AddScoped<IExternalTokenValidator, ExternalTokenValidator>();
            services.Configure<JwtAuthenticationOptions>(configuration.GetSection("Authentication:Jwt"));
            services.AddScoped<JwtTokenService>();
            services.AddScoped<AuthenticationService>();

            var jwtSection = configuration.GetSection("Authentication:Jwt");
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidIssuer = jwtSection["Issuer"],
                    ValidateAudience = true,
                    ValidAudience = jwtSection["Audience"],
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Authentication:Jwt:SecretKey"] ?? throw new InvalidOperationException("JWT signing key is not configured."))),
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.FromSeconds(30)
                };
            });
            services.AddAuthorization();
        }
    }
}
