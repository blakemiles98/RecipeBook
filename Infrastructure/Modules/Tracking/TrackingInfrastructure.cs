using Application.Modules.Tracking;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Modules.Tracking;

public static class TrackingInfrastructure
{
    public static IServiceCollection AddTrackingStorage(this IServiceCollection services)
    {
        services.AddScoped<IMealLogService, EfMealLogService>();
        services.AddScoped<ITrackingSummary, EfTrackingSummary>();
        services.AddScoped<IGoalSettingsService, EfGoalSettingsService>();

        return services;
    }
}
