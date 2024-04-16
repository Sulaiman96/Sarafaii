using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Sarafaii.Data;
using Sarafaii.Mapping;
using Sarafaii.Services;
using Sarafaii.Services.Implementations;

namespace Sarafaii.Extensions;

public static class ApplicationServiceExtension
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<ApplicationDbContext>(options =>
        {
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
        });

        var mapperConfig = new MapperConfiguration(mc =>
        {
            mc.AddProfile(new MappingProfile());
        });
        
        services.AddSingleton(mapperConfig.CreateMapper());
        services.AddScoped<ITokenService, TokenService>();
        
        return services;
    }
}