using API.Modules;
using Microsoft.OpenApi.Models;
using REST.Middleware;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddModule<CoreModule>(builder.Configuration);
builder.Services.AddModule<DomainModule>(builder.Configuration);
builder.Services.AddModule<RestModule>(builder.Configuration);
builder.Services.AddModule<AuthenticationModule>(builder.Configuration);
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors(options =>
{
    options.AddPolicy("ReactTestApp", policy =>
    {
        policy.WithOrigins("http://localhost:5173").AllowAnyHeader().AllowAnyMethod();
    });
});

builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Enter your JWT access token."
    });

    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
});

var app = builder.Build();
app.UseSwagger();
app.UseSwaggerUI();
app.UseMiddleware<ExceptionHandlingMiddleware>();
app.UseCors("ReactTestApp");
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();