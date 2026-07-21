using CA.DAL.Interfaces;
using CA.DAL.Persistence;
using CA.DAL.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CA.DAL;

public static class DependencyInjection
{
    public static IServiceCollection AddDataAccess(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<AppDbContext>(options =>
            options.UseSqlite(configuration.GetConnectionString("DefaultConnection")));

        services.AddScoped<ILitterRepository, LitterRepository>();
        services.AddScoped<IBenefitRepository, BenefitRepository>();

        return services;
    }
}