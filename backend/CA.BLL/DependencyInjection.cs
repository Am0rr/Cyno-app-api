using CA.BLL.Interfaces;
using CA.BLL.Services;
using Microsoft.Extensions.DependencyInjection;

namespace CA.BLL;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<ILitterService, LitterService>();
        services.AddScoped<INotificationService, NotificationService>();

        return services;
    }
}